using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HealthCheck.Models
{
    [DataContract]
    public class HealthCheckStatus
    {
        private Dictionary<HealthState, string> _messageMap;
 
        public HealthCheckStatus(string systemName, int threshold, string timeout)
        {
            SystemName = systemName;
            _messageMap = new Dictionary<HealthState, string>
                              {
                                  {HealthState.OK, "Service OK"},
                                  {HealthState.TryingToRestablish, "Failure threshold exceeded, trying to re-establish contact"},
                                  {HealthState.Critical, string.Format("Critical error, exceeded failure threshold of {0}, will try again after {1}s timeout", threshold, timeout)}

                              };
        }

        [DataMember]
        public string SystemName { get; set; }
        [DataMember]
        public string StateMessage { get { return _messageMap[State]; } set { StateMessage = value; } }
        [DataMember]
        public string Status { get { return State.ToString(); } set { Status = value; } }
        [DataMember]
        public string ExceptionMessage { get { return Exception != null ? Exception.Message : ""; } set { ExceptionMessage = value; } }
        [DataMember]
        public string ExceptionStackTrace { get { return Exception != null ? Exception.StackTrace : ""; } set { ExceptionStackTrace = value; } }
        [DataMember]
        public string MethodName { get { return Request != null ? Request.Method.ToString() : ""; } set { MethodName = value; } }

        public HealthState State { get; set; }
        public Ninject.Extensions.Interception.Request.IProxyRequest Request { get; set; }
        public Exception Exception { get; set; }
    }
}