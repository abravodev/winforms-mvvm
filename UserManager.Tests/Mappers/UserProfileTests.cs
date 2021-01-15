using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserManager.BusinessLogic.Model;
using UserManager.DTOs;
using UserManager.Mappers;
using UserManager.Tests.TestUtils;

namespace UserManager.Tests.Mappers
{
    [TestClass]
    public class UserProfileTests
    {
        [TestMethod]
        public void TestProfile() => TestMapperProfile.Test<UserProfile>();

        [TestMethod]
        public void Map_UserEmailIsNull_ReturnEmpty()
        {
            // Arrange
            var user = new User { Email = null };
            var mapper = TestMapperProfile.GetMapper<UserProfile>();

            // Act
            var userDto = mapper.Map<UserListItemDto>(user);

            // Assert
            userDto.Email.Should().BeEmpty();
        }
    }
}
