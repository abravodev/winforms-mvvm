using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess;
using UserManager.BusinessLogic.DataAccess.Repositories;

namespace UserManager.BusinessLogic.Tests.DataAccess
{
    [TestClass]
    public class DatabaseServiceTests
    {
        private DatabaseContext _databaseContext;
        private IDatabaseRepository _databaseRepository;
        private DatabaseService sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _databaseContext = Substitute.For<DatabaseContext>("anyConnectionString");
            _databaseRepository = Substitute.For<IDatabaseRepository>();
            sut = new DatabaseService(_databaseContext, _databaseRepository);
        }

        [TestMethod]
        public void GetName_ReturnNameOfTheDatabase()
        {
            // Arrange
            var connectionInfo = new SqlConnectionStringBuilder { InitialCatalog = "anyDatabaseName" };
            _databaseContext.ConnectionInfo.Returns(connectionInfo);

            // Act
            var name = sut.GetName();

            // Assert
            name.Should().Be(connectionInfo.InitialCatalog);
        }

        [TestMethod]
        public void GetServer_ReturnNameOfTheDatabaseServer()
        {
            // Arrange
            var connectionInfo = new SqlConnectionStringBuilder { DataSource = "anyDatabaseServer" };
            _databaseContext.ConnectionInfo.Returns(connectionInfo);

            // Act
            var name = sut.GetServer();

            // Assert
            name.Should().Be(connectionInfo.DataSource);
        }

        [TestMethod]
        public async Task CanConnectToDatabase_FailedToConnect_ReturnFalse()
        {
            // Arrange
            _databaseRepository.GetDatabaseVersion().Throws(new Exception());

            // Act
            var connectionSucceeded = await sut.CanConnectToDatabase();

            // Assert
            connectionSucceeded.Should().BeFalse();
        }

        [TestMethod]
        public async Task CanConnectToDatabase_SucceedAtConnecting_ReturnTrue()
        {
            // Arrange
            _databaseRepository.GetDatabaseVersion().Returns("anyVersion");

            // Act
            var connectionSucceeded = await sut.CanConnectToDatabase();

            // Assert
            connectionSucceeded.Should().BeTrue();
        }
    }
}
