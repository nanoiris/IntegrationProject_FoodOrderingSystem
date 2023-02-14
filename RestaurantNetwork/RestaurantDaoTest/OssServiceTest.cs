using Azure.Storage.Blobs;
using RestaurantDao;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RestaurantDao.Services;

namespace RestaurantDaoTest
{
    public class OssServiceTest
    {
        OssService service;

        [SetUp]
        public void Setup()
        {
            //service = new OssService();
        }
        /*
        [Test]
        public void AddRestCategoryTest()
        {
            string[] names = new string[] { "Pizza", "Burger", "Sandwich", "Coffee", "Steak", "Breakfast", "Fries", "ColaCan", "Salad" };
           
            foreach (string name in names)
            {
                RestCategory category = new RestCategory
                {
                    //Id = id,
                    Name = name,
                    Logo = name + ".png"
                };
                Stream logo = new FileStream(@"D:\tmp\img\icons\" + category.Logo, FileMode.Open);
                service.AddRestaurantCategory(category, logo);

            }
            //string id = Guid.NewGuid().ToString("N");

            Assert.Pass();
        }
        */
        /*
        [Test]
        public void AddRestCategoryTestFailedTest()
        {
            string id = Guid.NewGuid().ToString("N");
            RestCategory restaurant = new RestCategory
            {
                //Id = id,
                Logo = "axis.png",
            };
            Stream logo = new FileStream(@"D:\tmp\img\axis.png", FileMode.Open);
            
            Assert.Pass();
            try
            {
                service.AddRestaurantCategory(restaurant, logo);
            }
            finally
            {
                var containerClient = AppDbContext.GetBlobContainerClient();
                BlobClient blobClient = containerClient.GetBlobClient(restaurant.Logo);
                if (blobClient.Exists())
                    Assert.Fail();
                else Assert.Pass();
            }
        }
        */
        
        [Test]
        public void AddRestaurantTest()
        {
            Restaurant restaurant = new Restaurant
            {
                Name = "Pizza Kingdom - 2",
                Description = "Best pizza in world!",
                Street = "12 Saint Laurent",
                City = "Montreal",
                State = "QC",
                Country = "CAN",
                PhoneNo = "111111111",
                Zip = "H3H 3k5",
                Logo = "logo_web1.png",
                 
            };
            //RestCategory ca = service.FindCategory(4);
           // restaurant.Category = ca;
            //RestCategory ca = new RestCategory();
            //ca.Id = 4;
            //restaurant.CategoryId = 4;

            Stream logo = new FileStream(@"D:\tmp\img\logo_web1.png", FileMode.Open);
            service.AddRestaurant(restaurant, logo);
            
            Assert.Pass();
        }
        
        /*
        [Test]
        public void AddRestaurantFailedTest()
        {
            string id = Guid.NewGuid().ToString("N");
            Restaurant restaurant = new Restaurant
            {
                Name = "Restaurant Name",
                City = "Montreal",
                Country = "CAN",
                PhoneNo = "111111111",
                State = "QC",
                Street = "100 Cath",
                Logo = "trending7.png",
            };
            Stream logo = new FileStream(@"D:\tmp\img\trending7.png", FileMode.Open);
            Stream largeLogo = new FileStream(@"D:\tmp\img\trending8.png", FileMode.Open);
            try
            {
                service.AddRestaurant(restaurant, logo);
            }
            finally
            {
                var containerClient = AppDbContext.GetBlobContainerClient();
                BlobClient blobClient = containerClient.GetBlobClient(restaurant.Logo);
                if (blobClient.Exists())
                    Assert.Fail();
                else Assert.Pass();
            }
        }
        */
    }
}