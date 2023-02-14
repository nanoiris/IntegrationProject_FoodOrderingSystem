using RestaurantDao;

namespace RestaurantDaoTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BlogSetupTest1()
        {
            string blobConnString = AppDbContext.GetBlobConnectionString();
            if(blobConnString == null)
            {
                Assert.Fail();
            }
            string blobContainerName = AppDbContext.GetBlobContainerName();
            if (blobConnString == null)
            {
                Assert.Fail();
            }
            Assert.Pass();
            string blobItemUrlPrefix = AppDbContext.GetBlobItemUrlPrefix();
            if (blobItemUrlPrefix == null)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
    }
}