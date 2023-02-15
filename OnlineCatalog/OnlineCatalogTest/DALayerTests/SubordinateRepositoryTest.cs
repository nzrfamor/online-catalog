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
    [TestFixture]
    public class SubordinateRepositoryTest
    {

        [TestCase(1)]
        [TestCase(2)]
        public async Task SubordinateRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var SubordinateRepository = new SubordinateRepository(context);
            //Act
            var Subordinate = await SubordinateRepository.GetByIdAsync(id);
            var expectedSubordinate = ExpectedSubordinates.FirstOrDefault(x => x.Id == id);
            //Assert
            Assert.That(expectedSubordinate, Is.EqualTo(Subordinate).Using(new SubordinateEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task SubordinateRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var SubordinateRepository = new SubordinateRepository(context);
            //Act
            var Leaders = await SubordinateRepository.GetAllAsync();
            //Assert
            Assert.That(ExpectedSubordinates, Is.EqualTo(Leaders).Using(new SubordinateEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task SubordinateRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var SubordinateRepository = new SubordinateRepository(context);
            //Act
            var Subordinate = new Subordinate { Id = 3 };
            await SubordinateRepository.AddAsync(Subordinate);
            await context.SaveChangesAsync();
            //Arrange
            Assert.That(context.Subordinates.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task SubordinateRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var SubordinateRepository = new SubordinateRepository(context);
            //Act
            await SubordinateRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();
            //Assert
            Assert.That(context.Subordinates.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task SubordinateRepository_Update_UpdatesEntity()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var SubordinateRepository = new SubordinateRepository(context);
            //Act
            var Subordinate = new Subordinate
            {
                Id = 1,
                WorkerId = 2,
                LeaderId = 2
            };

            SubordinateRepository.Update(Subordinate);
            await context.SaveChangesAsync();
            var NewLeader = await SubordinateRepository.GetByIdAsync(1);
            //Assert
            Assert.That(NewLeader, Is.EqualTo(new Subordinate
            {
                Id = 1,
                WorkerId = 2,
                LeaderId = 2

            }).Using(new SubordinateEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task SubordinateRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var SubordinateRepository = new SubordinateRepository(context);
            //Act
            var Subordinate = await SubordinateRepository.GetByIdWithDetailsAsync(1);
            var expectedSubordinate = ExpectedSubordinates.FirstOrDefault(x => x.Id == 1);
            //Assert
            Assert.That(Subordinate,
                Is.EqualTo(expectedSubordinate).Using(new SubordinateEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(Subordinate.Leader,
                Is.EqualTo(ExpectedLeaders.FirstOrDefault(x => x.Id == expectedSubordinate.LeaderId)).Using(new LeaderEqualityComparer()),
                message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(Subordinate.Worker,
                Is.EqualTo(ExpectedWorkers.FirstOrDefault(x => x.Id == expectedSubordinate.WorkerId)).Using(new WorkerEqualityComparer()),
                message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        [Test]
        public async Task SubordinateRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var SubordinateRepository = new SubordinateRepository(context);
            //Act
            var Leaders = await SubordinateRepository.GetAllWithDetailsAsync();

            Assert.That(Leaders,
                Is.EqualTo(ExpectedSubordinates).Using(new SubordinateEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(Leaders.Select(i => i.Leader).OrderBy(i => i.Id),
                Is.EqualTo(ExpectedLeaders).Using(new LeaderEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(Leaders.Select(i => i.Worker).OrderBy(i => i.Id),
                Is.EqualTo(ExpectedWorkers.Skip(1)).Using(new WorkerEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
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
                new Subordinate { Id = 1, LeaderId = 1, WorkerId = 2},
                new Subordinate { Id = 2, LeaderId = 2, WorkerId = 3}
            };
    }
}

