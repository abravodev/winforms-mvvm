using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinformsTools.MVVM.Bindings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace WinformsTools.MVVM.Tests.Bindings
{
    [TestClass]
    public class ListObjectComparerTests
    {
        private class ListItem
        {
            public string FirstColumn { get; set; }

            public string SecondColumn { get; set; }
        }

        [TestMethod]
        public void CompareValuesByProperties_ItemWithNullProperties_ReturnedNullFirst()
        {
            // Arrange
            var itemWithNull = new ListItem { FirstColumn = null };
            var itemWithValue = new ListItem { FirstColumn = "Something" };
            var list = new List<ListItem> { itemWithValue, itemWithNull };
            var descriptor = typeof(ListItem).GetProperty(nameof(ListItem.FirstColumn));
            var sortCollection = new[]
            {
                new ListSortDescription(GetPropertyDescriptor(descriptor), ListSortDirection.Ascending)
            };
            var comparer = ListObjectComparer.FromSort<ListItem>(new ListSortDescriptionCollection(sortCollection));

            // Act
            list.Sort(comparer.CompareValuesByProperties);

            // Assert
            list.Should().BeEquivalentTo(new List<ListItem> { itemWithNull, itemWithValue });
        }

        [TestMethod]
        public void CompareValuesByProperties_ItemWithoutNullProperties_ReturnedSorted()
        {
            // Arrange
            var firstItem = new ListItem { FirstColumn = "A" };
            var secondItem = new ListItem { FirstColumn = "B" };
            var list = new List<ListItem> { secondItem, firstItem };
            var descriptor = typeof(ListItem).GetProperty(nameof(ListItem.FirstColumn));
            var sortCollection = new[]
            {
                new ListSortDescription(GetPropertyDescriptor(descriptor), ListSortDirection.Ascending)
            };
            var comparer = ListObjectComparer.FromSort<ListItem>(new ListSortDescriptionCollection(sortCollection));

            // Act
            list.Sort(comparer.CompareValuesByProperties);

            // Assert
            list.Should().BeEquivalentTo(new List<ListItem> { firstItem, secondItem });
        }

        private static PropertyDescriptor GetPropertyDescriptor(PropertyInfo PropertyInfo)
        {
            return TypeDescriptor
                .GetProperties(PropertyInfo.DeclaringType)
                .Cast<PropertyDescriptor>()
                .FirstOrDefault(x => x.Name == PropertyInfo.Name);
        }
    }
}
