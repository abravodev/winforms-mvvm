using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserManager.Mappers;
using UserManager.Tests.TestUtils;

namespace UserManager.Tests.Mappers
{
    [TestClass]
    public class UserProfileTests
    {
        [TestMethod]
        public void TestProfile() => TestMapperProfile.Test<UserProfile>();
    }
}
