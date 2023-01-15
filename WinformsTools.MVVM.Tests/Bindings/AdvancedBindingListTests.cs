using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using WinformsTools.MVVM.Bindings;

namespace WinformsTools.MVVM.Tests.Bindings
{
    [TestClass]
    public class AdvancedBindingListTests
    {
        private class User
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        [TestMethod]
        public void Can_be_constructed_with_empty_list()
        {
            // Act
            var emptyList = new AdvancedBindingList<User>(new List<User>());
            var list = new AdvancedBindingList<User>();

            // Assert
            list.Should().BeEmpty();
            list.Should().BeEquivalentTo(emptyList);
        }

        [TestMethod]
        public void Is_in_sync_with_source_list()
        {
            var firstUser = new User { FirstName = "John", LastName = "Doe" };
            var secondUser = new User { FirstName = "Joe", LastName = "Average" };
            var source = new BindingList<User> { firstUser };

            var advancedList = new AdvancedBindingList<User>(source);
            advancedList.Should().HaveCount(1);
            advancedList[0].Should().Be(firstUser);

            source.Add(secondUser);
            advancedList.Should().HaveCount(2);
            advancedList[1].Should().Be(secondUser);

            source.Remove(secondUser);
            advancedList.Should().HaveCount(1);

            firstUser.LastName = "Smith";
            advancedList[0].LastName.Should().Be("Smith");
        }

        [TestMethod]
        public void Can_remove_filter()
        {
            // Arrange
            var user = new User { FirstName = "Jack" };
            var source = new BindingList<User> { user };
            var list = new AdvancedBindingList<User>(source);
            list.Should().Contain(user);
            list.Filter = "[FirstName] IN ('john')";
            list.Should().BeEmpty();

            // Act
            list.RemoveFilter();

            // Assert
            list.Should().Contain(user);
            list.Filter.Should().BeEmpty();
        }
    }
}
