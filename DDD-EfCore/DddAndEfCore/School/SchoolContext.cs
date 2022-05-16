using DddAndEfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.School
{
    public sealed class SchoolContext : DbContext
    {
        private static readonly Type[] EnumerationTypes = { typeof(Course), typeof(Suffix) };
        private readonly string _connectionString;
        private readonly bool _useConsoleLogger;
        private readonly EventDispatcher _eventDispatcher;

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public SchoolContext() : this("Server=.;Database=EFCoreDDD;Trusted_Connection=true", true, new EventDispatcher(new MessageBus(new Bus())))
        {

        }
        public SchoolContext(string connectionString, bool useConsoleLogger, EventDispatcher eventDispatcher)
        {
            _connectionString = connectionString;
            _useConsoleLogger = useConsoleLogger;
            _eventDispatcher = eventDispatcher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(x =>
            {
                x.ToTable("Student").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("StudentID");
                x.Property(p => p.Email)
                    .HasConversion(p => p.Value, p => Email.Create(p).Value);
                x.OwnsOne(p => p.Name, p =>
                {
                    p.Property(pp => pp.First).HasColumnName("FirstName");
                    p.Property(pp => pp.Last).HasColumnName("LastName");
                    p.Property<Guid?>("NameSuffixID").HasColumnName("NameSuffixID");
                    p.HasOne(pp => pp.Suffix).WithMany().HasForeignKey("NameSuffixID").IsRequired(false);
                });
                x.HasOne(p => p.FavoriteCourse).WithMany();
                x.HasMany(p => p.Enrollments).WithOne(p => p.Student)
                    .OnDelete(DeleteBehavior.Cascade);
                //.Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
                //x.Property(p => p.FavoriteCourseId);
            });

            modelBuilder.Entity<Course>(x =>
            {
                x.ToTable("Course").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CourseID");
                x.Property(p => p.Name);
                //.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            modelBuilder.Entity<Suffix>(x =>
            {
                x.ToTable("Suffix").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("SuffixID");
                x.Property(p => p.Name);
                //.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            modelBuilder.Entity<Enrollment>(x =>
            {
                x.ToTable("Enrollment").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("EnrollmentID");
                x.HasOne(p => p.Student).WithMany(p => p.Enrollments);
                x.HasOne(p => p.Course).WithMany();
                x.Property(p => p.Grade);
            });

            SeedData(modelBuilder);
        }

        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> enumerationEntries = ChangeTracker.Entries().Where(w => EnumerationTypes.Contains(w.Entity.GetType()));

            foreach (EntityEntry enumerationEntry in enumerationEntries)
            {
                enumerationEntry.State = EntityState.Unchanged;
            }

            List<AggregateRoot> entities = ChangeTracker
                .Entries()
                .Where(w => w.Entity is AggregateRoot)
                .Select(s => (AggregateRoot)s.Entity)
                .ToList();

            int result = base.SaveChanges();

            foreach (var entity in entities)
            {
                _eventDispatcher.Dispatch(entity.DomainEvents);
                entity.ClearDomainEvents();
            }

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });

            optionsBuilder.UseSqlServer(_connectionString)
                .UseLazyLoadingProxies();

            if (_useConsoleLogger)
            {
                optionsBuilder.UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            }
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course(Guid.Parse("1b302e6f-81d6-4add-b8ef-424a4f685dd1"), "Calculus"),
                new Course(Guid.Parse("5d77a202-5c7a-44a3-99f9-f3fe3d34a0d3"), "Chemistry")
            );

            modelBuilder.Entity<Suffix>().HasData(
                new Suffix(Guid.Parse("314687c5-576c-48c2-bd7b-db56f7b6a552"), "Jr"),
                new Suffix(Guid.Parse("bab15095-f4a3-4f36-a2b1-8e552e09407b"), "Sr")
            );
        }
    }
}
