using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.DtoExceptions
{
    [DataContract]
    public class ServiceFault
    {
        [DataMember]
        public string ErrorMessage { get; private set; }

        public ServiceFault(string exceptionErrorMessage)
        {
            ErrorMessage = exceptionErrorMessage;
        }
    }
}
