﻿using System;
using System.Diagnostics;
using System.Timers;
using HealthCheck.Interceptors;
using HealthCheck.Models;
using HealthCheck.Services;
using Ninject.Extensions.Interception;

public class CircuitBreaker : SimpleInterceptor
{
    private readonly object monitor = new object();
    private CircuitBreakerState state;
    private int failures;
    private int threshold;
    private TimeSpan timeout;
    private HealthCheckStatus status;
    private readonly IHealthCheckRepository _healthCheckRepository;

    public CircuitBreaker(int threshold, int timeoutSeconds, string systemName, IHealthCheckRepository healthCheckRepository)
    {
        this.threshold = threshold;
        _healthCheckRepository = healthCheckRepository;
        this.timeout = TimeSpan.FromSeconds(timeoutSeconds);
        status = new HealthCheckStatus(systemName, threshold, timeout.TotalSeconds.ToString());
        MoveToClosedState();
    }

    protected override void BeforeInvoke(IInvocation invocation)
    {
        using (TimedLock.Lock(monitor))
        {
            state.ProtectedCodeIsAboutToBeCalled();
        }

        try
        {
            invocation.Proceed();
        }
        catch (Exception e)
        {
            status.Request = invocation.Request;
            status.Exception = e;
            using (TimedLock.Lock(monitor))
            {
                failures++;
                state.ActUponException(e);
            }
            throw;
        }

        using (TimedLock.Lock(monitor))
        {
            state.ProtectedCodeHasBeenCalled();
        }
    }

    private void MoveToClosedState()
    {
        state = new ClosedState(this);
        SetState();        
    }

    private void MoveToOpenState()
    {
        state = new OpenState(this);
        SetState();
    }

    private void MoveToHalfOpenState()
    {
        state = new HalfOpenState(this);
        SetState();
    }

    private void SetState()
    {
        status.State = state.StateName();
        _healthCheckRepository.AddOrUpdate(status);
    }

    private void ResetFailureCount()
    {
        failures = 0;
    }
 
    private bool ThresholdReached()
    {
        return failures >= threshold;
    }
 
    private abstract class CircuitBreakerState
    {
        protected readonly CircuitBreaker circuitBreaker;
 
        protected CircuitBreakerState(CircuitBreaker circuitBreaker)
        {
            this.circuitBreaker = circuitBreaker;
        }
 
        public virtual void ProtectedCodeIsAboutToBeCalled() { }
        public virtual void ProtectedCodeHasBeenCalled() { }
        public virtual void ActUponException(Exception e) { }
        public abstract HealthState StateName();
    }
 
    private class ClosedState : CircuitBreakerState
    {
        public ClosedState(CircuitBreaker circuitBreaker)
            : base(circuitBreaker)
        {
            circuitBreaker.ResetFailureCount();
        }
 
        public override void ActUponException(Exception e)
        {
            if (circuitBreaker.ThresholdReached()) circuitBreaker.MoveToOpenState();
        }

        public override HealthState StateName()
        {
            return HealthState.OK;
        }
    }
 
    private class OpenState : CircuitBreakerState
    {
        private readonly Timer timer;
 
        public OpenState(CircuitBreaker circuitBreaker)
            : base(circuitBreaker)
        {
            timer = new Timer(circuitBreaker.timeout.TotalMilliseconds);
            timer.Elapsed += TimeoutHasBeenReached;
            timer.AutoReset = false;
            timer.Start();
        }
 
        private void TimeoutHasBeenReached(object sender, ElapsedEventArgs e)
        {
            circuitBreaker.MoveToHalfOpenState();
        }
 
        public override void ProtectedCodeIsAboutToBeCalled()
        {
            throw new OpenCircuitException();
        }

        public override HealthState StateName()
        {
            return HealthState.Critical;
        }
    }
 
    private class HalfOpenState : CircuitBreakerState
    {
        public HalfOpenState(CircuitBreaker circuitBreaker) : base(circuitBreaker) { }
 
        public override void ActUponException(Exception e)
        {
            circuitBreaker.MoveToOpenState();
        }

        public override HealthState StateName()
        {
            return HealthState.TryingToRestablish;
        }

        public override void ProtectedCodeHasBeenCalled()
        {
            circuitBreaker.MoveToClosedState();
        }
    }
}

    public class OpenCircuitException : Exception
    {
    }