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
    public class WorkerRepositoryTest
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task WorkerRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var WorkerRepository = new WorkerRepository(context);
            //Act
            var Worker = await WorkerRepository.GetByIdAsync(id);
            var expectedWorker = ExpectedWorkers.FirstOrDefault(x => x.Id == id);
            //Assert
            Assert.That(expectedWorker, Is.EqualTo(Worker).Using(new WorkerEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task WorkerRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var WorkerRepository = new WorkerRepository(context);
            //Act
            var Workers = await WorkerRepository.GetAllAsync();
            //Assert
            Assert.That(ExpectedWorkers, Is.EqualTo(Workers).Using(new WorkerEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task WorkerRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var WorkerRepository = new WorkerRepository(context);
            //Act
            var Worker = new Worker { Id = 4 };
            await WorkerRepository.AddAsync(Worker);
            await context.SaveChangesAsync();
            //Arrange
            Assert.That(context.Workers.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task WorkerRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var WorkerRepository = new WorkerRepository(context);
            //Act
            await WorkerRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();
            //Assert
            Assert.That(context.Workers.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task WorkerRepository_Update_UpdatesEntity()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var WorkerRepository = new WorkerRepository(context);
            //Act
            var Worker = new Worker
            {
                Id = 1,
                PersonId = 1,
                Position = "newWorker3",
                Salary = 29000m,
                HireDate = DateTime.Parse("2002-07-09")

            };

            WorkerRepository.Update(Worker);
            await context.SaveChangesAsync();
            var NewWorker = await WorkerRepository.GetByIdAsync(1);
            //Assert
            Assert.That(NewWorker, Is.EqualTo(new Worker
            {
                Id = 1,
                PersonId = 1,
                Position = "newWorker3",
                Salary = 29000m,
                HireDate = DateTime.Parse("2002-07-09")

            }).Using(new WorkerEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task WorkerRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var WorkerRepository = new WorkerRepository(context);
            //Act
            var Worker = await WorkerRepository.GetByIdWithDetailsAsync(1);
            var expectedWorker = ExpectedWorkers.FirstOrDefault(x => x.Id == 1);
            //Assert
            Assert.That(Worker,
                Is.EqualTo(expectedWorker).Using(new WorkerEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(Worker.Person,
                Is.EqualTo(ExpectedPersons.FirstOrDefault(x => x.Id == expectedWorker.PersonId)).Using(new PersonEqualityComparer()), 
                message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        [Test]
        public async Task WorkerRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new ProjectDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var WorkerRepository = new WorkerRepository(context);
            //Act
            var Workers = await WorkerRepository.GetAllWithDetailsAsync();

            Assert.That(Workers,
                Is.EqualTo(ExpectedWorkers).Using(new WorkerEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(Workers.Select(i => i.Person).OrderBy(i => i.Id),
                Is.EqualTo(ExpectedPersons).Using(new PersonEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
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
                new Worker { Id = 3, PersonId = 3, Position = "worker3", Salary = 27000m, HireDate = DateTime.Parse("2002-07-09") }
            };
    }
}
