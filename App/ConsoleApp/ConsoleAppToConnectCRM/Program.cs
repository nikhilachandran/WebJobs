using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using System.Configuration;
namespace ConsoleAppToConnectCRM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //get service
                IOrganizationService service = GetCRMConnect();

                //create contact sample record
                Entity account = new Entity();
                account.LogicalName = "account";
                account["name"] = "SampleAccount_"+DateTime.Now.ToString();
                service.Create(account);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public static IOrganizationService GetCRMConnect()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string connectionStr = ConfigurationManager.ConnectionStrings["CRMConnection"].ConnectionString;

                CrmServiceClient serviceClient = new CrmServiceClient(connectionStr);

                WhoAmIResponse whoamiRes = serviceClient.Execute(new WhoAmIRequest()) as WhoAmIResponse;

                if (serviceClient!=null) { return (IOrganizationService)serviceClient.OrganizationWebProxyClient;  }
                else { throw new Exception(serviceClient.LastCrmError); }


            }
            catch (Exception ex)           
            {
                throw ex;
            }

        }
    }
}
