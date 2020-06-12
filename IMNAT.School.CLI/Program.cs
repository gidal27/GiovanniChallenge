using Db_Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using IMNAT.School.Services.Services.Configurations;
using IMNAT.School.Services.Services.Implementations;
using ViewModel;

namespace IMNAT.School.CLI
{
    class Program
    {           
               static int Main(string[] args)
                {
                    using (var scope = StartUp.CreateServiceProvider().CreateScope())
                    {
                     //First we initialise with some data
                 scope.ServiceProvider.GetService<SchoolManagement>().SeedDatabase();

                var Courses = scope.ServiceProvider.GetService<SchoolManagement>().GetAllCourses();

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

                    

    }


}
            
    
