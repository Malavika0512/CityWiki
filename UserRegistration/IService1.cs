using System;
using System.ServiceModel;

namespace UserRegistration
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string addtoXML(string username, string password, string filename);

        [OperationContract]
        string search(string username, string password, string filename);

        // TODO: Add your service operations here
    }
}
