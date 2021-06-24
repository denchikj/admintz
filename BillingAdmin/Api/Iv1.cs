using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using static BillingAdmin.Api.v1;

namespace BillingAdmin.Api
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "Iv1" в коде и файле конфигурации.
    [ServiceContract]    
    public interface Iv1
    {
       
        //[WebInvoke(UriTemplate = "/DoWork",
        //  RequestFormat = WebMessageFormat.Json,
        //  ResponseFormat = WebMessageFormat.Json, Method = "POST",
        //  BodyStyle = WebMessageBodyStyle.Wrapped)]

        [OperationContract]
        [WebGet(UriTemplate = "DoWork?composite={composite}")]
        string DoWork(String composite);

        [OperationContract]
        [WebGet(UriTemplate = "CreateUser?userData={userData}")]
        System.Threading.Tasks.Task<string> CreateUserAsync(string userData);
    }

    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class UserLoginDataType
    {
        string userName = "";
        string password = "";
        string roleId = "";

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public string RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }
    }
}
