using DddAndEfCore;
using DddAndEfCore.Common;
using DddAndEfCore.School;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DDD_EfCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //InitServices(optionsBuilder);
            //InitServices2();

            string result5 = Execute(x => x.EditPersonalInformation(Guid.Parse("B5127DFA-FDC7-4ECC-4DCA-08DA367B7560"), "Onur", "Özgüzel 3", "ozguzel.onur2@gmail.com", Guid.Parse("1B302E6F-81D6-4ADD-B8EF-424A4F685DD1"), Guid.Parse("314687c5-576c-48c2-bd7b-db56f7b6a552")));
            //string result4 = Execute(x => x.RegisterStudent("Onur", "ozguzel.onur@gmail.com", Guid.Parse("1B302E6F-81D6-4ADD-B8EF-424A4F685DD1"), Grade.B));
            //string result3 = Execute(x => x.DisenrollStudent(Guid.Parse("ee1ab909-08dc-4ea1-a45d-a48dbb7f1d4c"), Guid.Parse("1B302E6F-81D6-4ADD-B8EF-424A4F685DD1")));
            //string result2 = Execute(x => x.EnrollStudent(Guid.Parse("ee1ab909-08dc-4ea1-a45d-a48dbb7f1d4c"), Guid.Parse("1B302E6F-81D6-4ADD-B8EF-424A4F685DD1"), Grade.A));
            //string result = Execute(x => x.CheckStudentFavoriteCourse(Guid.Parse("ee1ab909-08dc-4ea1-a45d-a48dbb7f1d4c"), Guid.Parse("1B302E6F-81D6-4ADD-B8EF-424A4F685DD1")));
        }

        private static string Execute(Func<StudentController, string> func)
        {
            using (var context = new SchoolContext(GetConnectionString(), true, new EventDispatcher(new MessageBus(new Bus()))))
            {
                var controller = new StudentController(context);
                return func(controller);
            }
        }


        private static string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration["ConnectionStrings:DefaultConnection"];
        }

        //private static void InitServices(DbContextOptionsBuilder<SchoolContext> optionsBuilder)
        //{
        //    var services = new ServiceCollection();
        //    services.AddDbContext<SchoolContext>(options => options = optionsBuilder);
        //    var serviceProvider = services.BuildServiceProvider();
        //}

        //private static void InitServices2()
        //{
        //    var services = new ServiceCollection();
        //    services.AddScoped(_ => new SchoolContext(GetConnectionString(), true));
        //    var serviceProvider = services.BuildServiceProvider();
        //}
    }
}
