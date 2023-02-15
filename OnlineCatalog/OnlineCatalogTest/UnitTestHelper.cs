using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCatalogTests
{
    public class UnitTestHelper
    {
        public static DbContextOptions<ProjectDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ProjectDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }
        private static void SeedData(ProjectDbContext context)
        {
            context.Persons.AddRange(
                new Person { Id = 1, Name = "Name1", Surname = "Surname1"},
                new Person { Id = 2, Name = "Name2", Surname = "Surname2"},
                new Person { Id = 3, Name = "Name3", Surname = "Surname3"});
            context.Workers.AddRange(
                new Worker { Id = 1, PersonId = 1, Position = "worker1", Salary = 100000m, HireDate = DateTime.Parse("2014-11-05")},
                new Worker { Id = 2, PersonId = 2, Position = "worker2", Salary = 10030m, HireDate = DateTime.Parse("2012-01-05")},
                new Worker { Id = 3, PersonId = 3, Position = "worker3", Salary = 27000m, HireDate = DateTime.Parse("2002-07-09") });
            context.Leaders.AddRange(
                new Leader { Id = 1, WorkerId = 1 },
                new Leader { Id = 2, WorkerId = 2 });
            context.Subordinates.AddRange(
                new Subordinate { Id = 1, WorkerId = 2, LeaderId = 1},
                new Subordinate { Id = 2, WorkerId = 3, LeaderId = 2});
            context.SaveChanges();
        }
    }
}
