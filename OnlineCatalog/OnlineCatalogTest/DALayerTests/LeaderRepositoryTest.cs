using DAL.Data;
using DAL.Entities;
using DAL.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCatalogTests.DALayerTests
{
    internal class LeaderRepositoryTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task LeaderRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var LeaderRepository = new LeaderRepository(context);
            //Act
            var Leader = await LeaderRepository.GetByIdAsync(id);
            var expectedLeader = ExpectedLeaders.FirstOrDefault(x => x.Id == id);
            //Assert
            Assert.That(expectedLeader, Is.EqualTo(Leader).Using(new LeaderEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task LeaderRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var LeaderRepository = new LeaderRepository(context);
            //Act
            var Leaders = await LeaderRepository.GetAllAsync();
            //Assert
            Assert.That(ExpectedLeaders, Is.EqualTo(Leaders).Using(new LeaderEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task LeaderRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var LeaderRepository = new LeaderRepository(context);
            //Act
            var Leader = new Leader { Id = 3 };
            await LeaderRepository.AddAsync(Leader);
            await context.SaveChangesAsync();
            //Arrange
            Assert.That(context.Leaders.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task LeaderRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var LeaderRepository = new LeaderRepository(context);
            //Act
            await LeaderRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();
            //Assert
            Assert.That(context.Leaders.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task LeaderRepository_Update_UpdatesEntity()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var LeaderRepository = new LeaderRepository(context);
            //Act
            var Leader = new Leader
            {
                Id = 1,
                WorkerId = 2

            };

            LeaderRepository.Update(Leader);
            await context.SaveChangesAsync();
            var NewLeader = await LeaderRepository.GetByIdAsync(1);
            //Assert
            Assert.That(NewLeader, Is.EqualTo(new Leader
            {
                Id = 1,
                WorkerId = 2

            }).Using(new LeaderEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task LeaderRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var LeaderRepository = new LeaderRepository(context);
            //Act
            var Leader = await LeaderRepository.GetByIdWithDetailsAsync(1);
            var expectedLeader = ExpectedLeaders.FirstOrDefault(x => x.Id == 1);
            //Assert
            Assert.That(Leader,
                Is.EqualTo(expectedLeader).Using(new LeaderEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(Leader.Worker,
                Is.EqualTo(ExpectedWorkers.FirstOrDefault(x => x.Id == expectedLeader.WorkerId)).Using(new WorkerEqualityComparer()),
                message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        [Test]
        public async Task LeaderRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var LeaderRepository = new LeaderRepository(context);
            //Act
            var Leaders = await LeaderRepository.GetAllWithDetailsAsync();

            Assert.That(Leaders,
                Is.EqualTo(ExpectedLeaders).Using(new LeaderEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(Leaders.Select(i => i.Worker).OrderBy(i => i.Id),
                Is.EqualTo(ExpectedWorkers.Take(2)).Using(new WorkerEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        private static IEnumerable<Person> ExpectedPersons =>
           new[]
           {
                new Person { Id = 1, Name = "Name1", Surname = "Surname1"},
                new Person { Id = 2, Name = "Name2", Surname = "Surname2"},
                new Person { Id = 3, Name = "Name3", Surname = "Surname3"}
           };

        private static IEnumerable<Worker> ExpectedWorkers =>
            new[]
            {
                new Worker { Id = 1, PersonId = 1, Position = "worker1", Salary = 100000m, HireDate = DateTime.Parse("2014-11-05")},
                new Worker { Id = 2, PersonId = 2, Position = "worker2", Salary = 10030m, HireDate = DateTime.Parse("2012-01-05")},
                new Worker { Id = 3, PersonId = 3, Position = "worker3", Salary = 27000m, HireDate = DateTime.Parse("2002-07-09")}
            };
        private static IEnumerable<Leader> ExpectedLeaders =>
            new[]
            {
                new Leader { Id = 1, WorkerId = 1 },
                new Leader { Id = 2, WorkerId = 2 }
            };
        private static IEnumerable<Subordinate> ExpectedSubordinates =>
            new[]
            {
                new Subordinate { Id = 1, LeaderId = 2, WorkerId = 1},
                new Subordinate { Id = 2, LeaderId = 3, WorkerId = 2}
            };
    }
}
