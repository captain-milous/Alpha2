using Komprese;
using Komprese.src.CompressHandling;
using Komprese.src.UI;

namespace UnitTesting
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCompression()
        {
            try
            {
                Compression compress = new Compression("Test");
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestDecompression()
        {
            try
            {
                Compression compress = new Compression("Tes", true);
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestConfig()
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
    }
}