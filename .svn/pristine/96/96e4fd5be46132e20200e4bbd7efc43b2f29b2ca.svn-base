using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthCheck.Models;

namespace HealthCheck.Services
{
    public interface IHealthCheckRepository
    {
        void AddOrUpdate(HealthCheckStatus status);
        IEnumerable<HealthCheckStatus> GetAll();
    }

    public class HealthCheckRepository : IHealthCheckRepository
    {
        private static List<HealthCheckStatus> statuses = new List<HealthCheckStatus>();
        
        public void AddOrUpdate(HealthCheckStatus status)
        {
            var existing = statuses.FirstOrDefault(x => x.SystemName == status.SystemName);
            if (existing != null)
            {
                existing = status;
            }
            else
            {
                statuses.Add(status);
            }
        }

        public IEnumerable<HealthCheckStatus> GetAll()
        {
            return statuses;
        }
    }
}