using System;

namespace HealthCheck.Services
{
    public interface IStringService
    {
        string GetValues(string s);
    }

    public class StringService : IStringService
    {
       public virtual string GetValues(string s)
        {
            if(s.Contains("Error"))
            {
                throw new ApplicationException("Typing in error is not allowed");
            }
            return s;
        }
    }


}