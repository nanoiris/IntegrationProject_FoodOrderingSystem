namespace RmsApp.Services
{
    public static class Constants
    {
        // my local Db below
        public const string RestaurantId = "dffa781cbd6b4fafbb4f5e5b1cd05386";

        // 1. Restaurant Server 
        public const string RestUri = "http://localhost:5064/";
        public const string AzureRestUri = "http://fsd05rnv.eastus.cloudapp.azure.com:5064/";
        public const string AWSRestUri = "http://ec2-18-214-61-45.compute-1.amazonaws.com:5064/";
        // 2. Identity Server 
        public const string IdentityUri = "http://localhost:5191/";
        public const string AzureIdentityUri = "http://fsd05rnv.eastus.cloudapp.azure.com:5191/";
        public const string AWSIdentityUri = "http://ec2-18-214-61-45.compute-1.amazonaws.com:5191/";


        // 3. Order Server 
        public const string OrderUri = "http://localhost:5275/";
        public const string AzureOrderUri = "http://fsd05rnv.eastus.cloudapp.azure.com:5275/";
        public const string AWSOrderUri = "http://ec2-18-214-61-45.compute-1.amazonaws.com:5275/";

        // 4. Delivery Server 
        public const string DeliveryUri = "http://localhost:5064/";
        public const string AzureDeliveryUri = "http://fsd05rnv.eastus.cloudapp.azure.com:5064/";
        public const string AWSDeliveryUri = "http://ec2-18-214-61-45.compute-1.amazonaws.com:5064/";

        // 5. Rating Server 
        public const string RatingUri = "http://localhost:5064/";
        public const string AzureRatingUri = "http://fsd05rnv.eastus.cloudapp.azure.com:5064/";
        public const string AWSRatingUri = "http://ec2-18-214-61-45.compute-1.amazonaws.com:5064/";
    }

}