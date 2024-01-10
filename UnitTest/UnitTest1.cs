using Komprese;
namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestConfigLoader();
        }

        [Test]
        public void TestConfigLoader()
        {
            try
            {
                ConfigurationLoader config = new ConfigurationLoader();

                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Test1()
        {
            try
            {

                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }
        [Test]
        public void Test2()
        {
            try
            {

                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}