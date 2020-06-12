using Db_Context;
using Db_Context.ViewModels;
using IMNAT.School.Repositories.DAL;
using IMNAT.School.Repositories.DAL.Repository;
using IMNAT.School.Repositories.DAL.Repository.Implementations;
using IMNAT.School.Services.Services;
using IMNAT.School.Services.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IMNAT.School.CLI
{
    class Program
    {
        

        public class SchoolManagement
        {
            readonly SchoolDbContext _SchoolDbContext;

        /*-------------------------------------------------------------------------------------------------*/
            public SchoolManagement(SchoolDbContext SchoolDbContext) => _SchoolDbContext = SchoolDbContext;

            public IEnumerable<Course> GetAllCourses()
            {


                var ListCourses = _SchoolDbContext.Courses;

                return ListCourses;
            }
          /*------------------------------------------------------------------------------------------------------------------*/

            public string CreateStudent(string StudentName, string email)
            {
                string[] Names = StudentName.Split(' ');
                Student student = new Student();

                if (Names.Length > 2)
                {
                    for (int i = 0; i < (Names.Length-1); i++)
                    { student.FirstName += (Names[i]+ " "); }

                    student.LastName = Names[(Names.Length - 1)];
                    student.Email = email;
                }

                else
                {
                    student.FirstName = Names[0]; student.LastName = Names[1];
                    student.Email = email;
                }

                _SchoolDbContext.Students.Add(student);

                _SchoolDbContext.SaveChanges();
                return (student.LastName);
            }

            /*--------------------------------------------------------------------------------------------------*/
            public void LinkStudentToCourse(string StudentName, int CourseID) {

                StudentCourse Selection = new StudentCourse {SelectedCourseID = CourseID, Student =StudentName };

                try
                {
                    _SchoolDbContext.SelectedCourses.Add(Selection);
                    _SchoolDbContext.SaveChanges();
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }

            /*------------------------------------------------------------------------------------------------------*/

            public Dictionary<string, IEnumerable<string>> DisplaySelection() {

                Dictionary<string, IEnumerable<string>> AllSelections = new Dictionary<string, IEnumerable<string>>();
                
                var RegisteredStudents = from StudentNames in _SchoolDbContext.Students.AsEnumerable()
                                         select StudentNames.LastName;
                           
                foreach (string key in RegisteredStudents)
                {
                    var CourseNames = from c in _SchoolDbContext.Courses
                                      join s in _SchoolDbContext.SelectedCourses on c.Id equals s.SelectedCourseID
                                      where s.Student == key
                                      select c.Name;

                    AllSelections.Add(key, CourseNames);

                }

                return AllSelections;

            }

        /*--------------------------------------------------------------------------------------------------------*/
         
            
        }
        public class MySettings
        {
            public string Connection { get; set; }
            public string[] InitialCourses { get; set; }
        }

        private static IServiceProvider CreateServiceProvider()
        {
            // create service collection
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            return services.BuildServiceProvider();
        }

        public static class FileConfigRead {

            public static  string[] InitialContent { get; set; }
        }


        public static void ConfigureServices(IServiceCollection Services)
        {

            var config = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();

            var appConfig = config.GetSection("application").Get<MySettings>(); // get connection strings
            var InitialData = config.GetSection("Data").Get<MySettings>(); //to get initial data to seed database

            FileConfigRead.InitialContent = InitialData.InitialCourses;
            var InitialContent = appConfig.InitialCourses;

            Services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(appConfig.Connection, b => b.MigrationsAssembly("IMNAT.School.Models")));
            Services.AddSingleton<SchoolManagement>();
            Services.AddSingleton<ICourseManagement, CourseManagement>();
            Services.AddSingleton<ICoursesRepo, CoursesRepo>();
            Services.AddSingleton<IStudentrepo, StudentsRepo>();
            Services.AddSingleton<IStudentManagement, StudentManagement>();
        }

        static int Main(string[] args)
        {
            
            using (var scope = CreateServiceProvider().CreateScope())
            {
                
               var Courses= scope.ServiceProvider.GetService<SchoolManagement>().GetAllCourses();

                if (Courses != null)
                {
                    Console.WriteLine("----- Voici la liste des cours disponibles ------------ \n\n\n");

                    foreach (Course course in Courses)
                    {
                        Console.WriteLine("{0} ---------- {1}\n", course.Id, course.Name);
                    }

                }
                else
                {
                    Console.WriteLine("Je dois me soumettre a Giovanni");
                       return 0; 
                        };

                Console.WriteLine("--------------- Quel est vos Nom(s)?(dans cet ordre prenom(s)/nom):  ");

                  var Noms = Console.ReadLine();

                Console.WriteLine("---------------  Ecrivez votre adresse courriel SVP?:   ");

                var Courriel = Console.ReadLine();

                 var Nom = scope.ServiceProvider.GetService<SchoolManagement>().CreateStudent(Noms, Courriel);

                Console.WriteLine("------------ Entrer le numero du cours auquel vous souhaitez vous inscrire :  ");

                var courseNoString = Console.ReadLine();
                var courseNoInt = Int16.Parse(courseNoString);
              
                scope.ServiceProvider.GetService<SchoolManagement>().LinkStudentToCourse(Nom, courseNoInt);

                Console.WriteLine("------ Souhaitez vous en ajouter d'autres a votre liste de cours? (Y/N) ");

                var Option = Console.ReadLine();

                while (Option == "Y") 
                {
                    Console.WriteLine("------ Saisissez de nouveau l'option du cours. Tapez 'N' pour arreter et afficher vos selections ");
                    var Choix = Console.ReadLine();
                    var ChoixInt = Int16.Parse(Choix);

                    scope.ServiceProvider.GetService<SchoolManagement>().LinkStudentToCourse(Nom, ChoixInt);
                    Console.WriteLine("-------- Souhaitez vous en ajouter d'autres a votre liste de cours? (Y/N) ");

                     Option = Console.ReadLine();
                }

                var Inscriptions = scope.ServiceProvider.GetService<SchoolManagement>().DisplaySelection();

                Console.WriteLine("-------- Voici la liste de tous les inscriptions suivants les noms d'etudiants \n\n\n");

                foreach (string etudiant in Inscriptions.Keys)
                {
                    foreach (string cours in Inscriptions[etudiant])
                        Console.WriteLine("{0} --------> {1}\n\n", etudiant, cours);
                }
                return 0;

            }
        }

        private class Factory : IDesignTimeDbContextFactory<SchoolDbContext>
        {
            public SchoolDbContext CreateDbContext(string[] args)
                => CreateServiceProvider().CreateScope().ServiceProvider.GetService<SchoolDbContext>();
        }

    }


}
            
    
