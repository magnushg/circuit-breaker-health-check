using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Ninject.Extensions.Interception;

namespace HealthCheck.Interceptors
{
    public class TimingInterceptor : SimpleInterceptor
    {
        readonly Stopwatch _stopwatch = new Stopwatch();
        protected override void BeforeInvoke(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void AfterInvoke(IInvocation invocation)
        {
            _stopwatch.Stop();
            string message = string.Format("Execution of {0} took {1}.",
                                            invocation.Request.Method,
                                            _stopwatch.Elapsed);
            Trace.WriteLine(message);
            _stopwatch.Reset();
        }
    }
}