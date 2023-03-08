using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DeliveryApp.Data
{
    public static class Constants
    {
        public const string LocalhostUri = "http://localhost:";
        public const string AzureUri = "http://fsd05rnv1.eastus.cloudapp.azure.com:";
        
        public const string AWSUri = "http://ec2-18-214-61-45.compute-1.amazonaws.com:";
        

        public const string baseUri = LocalhostUri;

        public const string IdentityUri = "http://ec2-18-214-61-45.compute-1.amazonaws.com:5191/";
        public const string DelyUri = "http://fsd05rnv1.eastus.cloudapp.azure.com:5175/";
        public const string RestUri = "http://fsd05rnv1.eastus.cloudapp.azure.com:5064/";
        public const string OrderUri = "http://fsd05rnv1.eastus.cloudapp.azure.com:5275/";

        public const string imgLocal ="img/delivery/";
        public const string imgAzureBlob = "https://pxtoday.blob.core.windows.net/pxtoday/";

        public const string imgUrl = imgAzureBlob;



    }
}
