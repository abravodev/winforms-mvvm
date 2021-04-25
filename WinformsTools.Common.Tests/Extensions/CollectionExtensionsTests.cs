using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WinformsTools.Common.Extensions;

namespace WinformsTools.Common.Tests.Extensions
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void AddRange_Empty_DoNotChangeCollection()
        {
            // Arrange
            var collection = new Collection<int> { 0 };

            // Act
            collection.AddRange(Enumerable.Empty<int>());

            // Assert
            collection.Should().BeEquivalentTo(new[] { 0 });
        }

        [TestMethod]
        public void AddRange_OneElement_AddElementToCollection()
        {
            // Arrange
            var collection = new Collection<int> { 0 };

            // Act
            collection.AddRange(new[] { 1 });

            // Assert
            collection.Should().BeEquivalentTo(new[] { 0, 1 });
        }

        [TestMethod]
        public void Replace_NoItemsInSource_AddNewElements()
        {
            // Arrange
            var collection = new Collection<int>();

            // Act
            collection.Replace(new[] { 1 });

            // Assert
            collection.Should().BeEquivalentTo(new[] { 1 });
        }

        [TestMethod]
        public void Replace_SourceHasItems_ReplaceWithNewElements()
        {
            // Arrange
            var collection = new Collection<int> { 0 };

            // Act
            collection.Replace(new[] { 1 });

            // Assert
            collection.Should().BeEquivalentTo(new[] { 1 });
        }

        [TestMethod]
        public void Replace_NewCollectionIsEmpty_RemoveAllItems()
        {
            // Arrange
            var collection = new Collection<int> { 0 };

            // Act
            collection.Replace(new int[0]);

            // Assert
            collection.Should().BeEmpty();
        }

        [TestMethod]
        public void AddRange_TwoElement_AddElementsToCollection()
        {
            // Arrange
            var collection = new Collection<int> { 0 };

            // Act
            collection.AddRange(new[] { 1, 2 });

            // Assert
            collection.Should().BeEquivalentTo(new[] { 0, 1, 2 });
        }

        [TestMethod]
        public void IsEmpty_Empty_ReturnsTrue()
        {
            // Arrange
            var collection = new int[0];

            // Act
            var isEmpty = collection.IsEmpty();

            // Assert
            isEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_HasElements_ReturnsFalse()
        {
            // Arrange
            var collection = new int[] { 1 };

            // Act
            var isEmpty = collection.IsEmpty();

            // Assert
            isEmpty.Should().BeFalse();
        }

        [TestMethod]
        public void Split_NullString_ThrowException()
        {
            // Arrange
            string text = null;

            // Act
            Action action = () => text.Split("any");

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow("", "", new[] { "" })]
        [DataRow("", "-", new[] { "" })]
        [DataRow("abc", "", new[] { "abc" })]
        [DataRow("a-b-c", "-", new[] { "a", "b", "c" })]
        [DataRow("astopbstopc", "stop", new[] { "a", "b", "c" })]
        public void Split_AnyString_ReturnSplittedArray(string source, string separator, string[] expected)
        {
            // Act
            var result = source.Split(separator);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
