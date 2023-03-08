using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data
{
    public static class Constants
    {
        public const string LocalhostUri = "http://localhost:";
        public const string AzureUri = "http://fsd05rnv1.eastus.cloudapp.azure.com:";
        public const string AzureRatingUri = "http://fsd05rnv.eastus.cloudapp.azure.com:";
        public const string AWSUri = "http://ec2-18-214-61-45.compute-1.amazonaws.com:";

        public const string baseUri = AzureUri;

        public const string IdentityUri = AWSUri + "5191";

        public const string RestUri = baseUri + "5064";     
        public const string OrderUri = baseUri + "5275";
        public const string RatingUri = AzureRatingUri + "5048";
        public const string DeliveryUri = baseUri + "5175";

        public const string imgLocal = "img/rest/";
        public const string imgAzureBlob = "https://pxtoday.blob.core.windows.net/pxtoday/";

        public const string imgUrl = imgAzureBlob;

        public const string keyPublic = "pk_test_51MZ0uECfoDmFMZLdq9tu56tAiEdIS8khf5QNtFHmBZxd71zDLHMMP3FnGdP3EuWJGe75YqCvMOkq6YmooEfWVKBF00qzrJoc06";
        public const string keySecret = "sk_test_51MZ0uECfoDmFMZLdzmF7eLHHLAonoZe5hptwRkvNfg4imaHG8djSCo0Z8dNJF0l1yJdOOfz2ftDlI0O3JnqUJYWU00zdNk6KJr";

    }
}
