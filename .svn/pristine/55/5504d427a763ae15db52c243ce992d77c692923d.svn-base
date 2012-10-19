using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthCheck.Services;

namespace HealthCheck.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IStringService _stringService;
        private readonly IMathService _mathService;

        public ValuesController(IStringService stringService, IMathService mathService)
        {
            _stringService = stringService;
            _mathService = mathService;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] {_stringService.GetValues("Hey")};
        }

        // GET api/values/5
        public string Get(string message, int a, int b)
        {
            var divided = _mathService.Divide(a, b);
            var msg = _stringService.GetValues(message);
            return string.Format("Message: {0}, result: {1}", _stringService.GetValues(message), divided);

        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}