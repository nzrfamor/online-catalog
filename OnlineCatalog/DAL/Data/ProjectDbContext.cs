using Bogus;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Data
{
    public class ProjectDbContext : DbContext
    {
        /*public ProjectDbContext()
        {
        }*/
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Person> Persons { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Subordinate> Subordinates { get; set; }
        public DbSet<Leader> Leaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>()
                .HasOne(w => w.Person)
                .WithOne()
                .HasForeignKey<Worker>(p => p.PersonId);

            modelBuilder.Entity<Subordinate>()
                .HasOne(s => s.Worker)
                .WithOne(w => w.WorkerAsSubordinate)
                .HasForeignKey<Subordinate>(s => s.WorkerId).IsRequired();

            modelBuilder.Entity<Subordinate>()
                .HasOne(c => c.Leader)
                .WithMany(l => l.Subordinates)
                .HasForeignKey(s => s.LeaderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Leader>()
                .HasOne(l => l.Worker)
                .WithOne(w => w.WorkerAsLeader)
                .HasForeignKey<Leader>(l => l.WorkerId).IsRequired();

            modelBuilder.Entity<Leader>()
                .HasMany(l => l.Subordinates)
                .WithOne(s => s.Leader)
                .HasForeignKey(s => s.LeaderId);

            FakeData.Init();
            modelBuilder.Entity<Entities.Person>(p =>
            {
                p.HasData(FakeData.Persons);
            });
            modelBuilder.Entity<Leader>(l =>
            {
                l.HasData(FakeData.Leaders);
            });
            modelBuilder.Entity<Worker>(w =>
            {
                w.HasData(FakeData.Workers);
            });
            modelBuilder.Entity<Subordinate>(s =>
            {
                s.HasData(FakeData.Subordinates);
            });
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OnlineCatalog;Trusted_Connection=True;");
        }*/
        public static class FakeData
        {
            public static List<Entities.Person> Persons = new List<Entities.Person>();
            public static List<Worker> Workers = new List<Worker>();
            public static List<Leader> Leaders = new List<Leader>();
            public static List<Subordinate> Subordinates = new List<Subordinate>();

            public static void Init()
            {
                var firstPerson = new Entities.Person
                {
                    Id = 1,
                    Name = "First",
                    Surname = "First"
                };
                var firstWorker = new Worker
                {
                    Id = 1,
                    PersonId = firstPerson.Id,
                    Position = "Owner",
                    HireDate = DateTime.Now,
                    Salary = 1000000M
                };

                var firstLeader = new Leader
                {
                    Id = 1,
                    WorkerId = firstWorker.Id,
                    Subordinates = null
                };


                var persId = 2;
                var workerId = 2;
                int leadId = 2;
                int subId = 2;

                var PersonFaker = new Faker<Entities.Person>()
                    .RuleFor(p => p.Id, _ => persId++)
                    .RuleFor(p => p.Name, f => f.Name.FirstName()) 
                    .RuleFor(p => p.Surname, f => f.Name.LastName());

                var WorkerFaker = new Faker<Worker>()
                    .RuleFor(e => e.Id, _ => workerId++)
                    .RuleFor(e => e.PersonId, (_,w) => w.Id)
                    .RuleFor(e => e.HireDate, _ => DateTime.Now)
                    .RuleFor(e => e.Salary, _ => new Random().Next(10000,250000));


                for (int i = 0; i < 5; i++)
                {
                    var personI = PersonFaker.Generate();
                    var workerI = WorkerFaker.Generate();
                    workerI.Position = $"level 2 worker";
                    var leaderI = new Leader
                    {
                        Id = leadId++,
                        WorkerId = workerI.Id
                    };
                    var subordI = new Subordinate
                    {
                        Id = subId++,
                        WorkerId = workerI.Id,
                        LeaderId = firstLeader.Id
                    };
                    for (int j = 0; j < 6; j++)
                    {
                        var personJ = PersonFaker.Generate();
                        var workerJ = WorkerFaker.Generate();
                        workerJ.Position = $"level 3 worker";
                        var leaderJ = new Leader
                        {
                            Id = leadId++,
                            WorkerId = workerJ.Id
                        };
                        var subordJ = new Subordinate
                        {
                            Id = subId++,
                            WorkerId = workerJ.Id,
                            LeaderId = leaderI.Id
                        };
                        for (int k = 0; k < 7; k++)
                        {
                            var personK = PersonFaker.Generate();
                            var workerK = WorkerFaker.Generate();
                            workerK.Position = $"level 4 worker";
                            var leaderK = new Leader
                            {
                                Id = leadId++,
                                WorkerId = workerK.Id
                            };
                            var subordK = new Subordinate
                            {
                                Id = subId++,
                                WorkerId = workerK.Id,
                                LeaderId = leaderJ.Id
                            };
                            for (int z = 0; z < 8; z++)
                            {
                                var personZ = PersonFaker.Generate();
                                var workerZ = WorkerFaker.Generate();
                                workerZ.Position = $"level 5 worker";
                                var subordZ = new Subordinate
                                {
                                    Id = subId++,
                                    WorkerId = workerZ.Id,
                                    LeaderId = leaderK.Id
                                };
                                Persons.Add(personZ);
                                Workers.Add(workerZ);
                                Subordinates.Add(subordZ);
                            }
                            Persons.Add(personK);
                            Workers.Add(workerK);
                            Leaders.Add(leaderK);
                            Subordinates.Add(subordK);
                        }
                        Persons.Add(personJ);
                        Workers.Add(workerJ);
                        Leaders.Add(leaderJ);
                        Subordinates.Add(subordJ);
                    }
                    Persons.Add(personI);
                    Workers.Add(workerI);
                    Leaders.Add(leaderI);
                    Subordinates.Add(subordI);

                }


                Persons.Add(firstPerson);
                Workers.Add(firstWorker);
                Leaders.Add(firstLeader);

                
            }
        }
    }
}

