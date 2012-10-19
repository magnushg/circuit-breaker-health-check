using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthCheck.Models;
using HealthCheck.Services;

namespace HealthCheck.Controllers
{
    public class HealthCheckController : ApiController
    {
        private readonly IHealthCheckRepository _healthCheckRepository;

        public HealthCheckController(IHealthCheckRepository healthCheckRepository)
        {
            _healthCheckRepository = healthCheckRepository;
        }

        // GET api/healthcheck
        public IEnumerable<HealthCheckStatus> Get()
        {
            return _healthCheckRepository.GetAll();
        }

        // GET api/healthcheck/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/healthcheck
        public void Post([FromBody]string value)
        {
        }

        // PUT api/healthcheck/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/healthcheck/5
        public void Delete(int id)
        {
        }
    }
}
