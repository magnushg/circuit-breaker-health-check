using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Services
{
   public interface IAlwaysFails
   {
       void Fail();
   }

    public class AlwaysFails : IAlwaysFails
    {
        public void Fail()
        {
            throw new AccessViolationException();
        }
    }

}
