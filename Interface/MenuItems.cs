using Labb3Databaser.Models;

namespace Labb3Databaser.Interface
{
    internal class MenuItems
    {
        public static void RunProgram()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Vad vill du göra?\n" +
                    "[1] Lista alla elever på skolan\n" +
                    "[2] Lista alla elever i valfri klass\n" +
                    "[3] Lägg till personal i databasen\n" +
                    "[4] Visa lärare per avdelning\n" +
                    "[5] Visa detaljerad info om alla elever\n" +
                    "[6] Visa aktiva kurser\n" +
                    "[7] Avsluta\n" +
                    "Välj siffra + enter: ");
                int userChoice = InterfaceMethods.CheckInput(7);
                switch (userChoice)
                {
                    case 1:
                        ListAllStudents();
                        break;
                    case 2:
                        ListStudentClass();
                        break;
                    case 3:
                        AddStaff();
                        break;
                    case 4:
                        ListTeachers();
                        break;
                    case 5:
                        ListStudentsDetailed();
                        break;
                    case 6:
                        ViewActiveCourses();
                        break;
                    case 7:
                        run = false;
                        break;
                }
            }
        }

        public static void ListAllStudents()
        {
            var context = new SkolanDbContext();
            bool name = InterfaceMethods.FirstName();
            bool asc = InterfaceMethods.Ascending();
            if (name && asc)
            {
                var students = context.Students
                    .Where(s => s.StudentId > 0)
                    .OrderBy(s => s.FName);
                InterfaceMethods.PrintStudents(students);
            }
            else if (name && !asc)
            {
                var students = context.Students
                    .Where(s => s.StudentId > 0)
                    .OrderByDescending(s => s.FName);
                InterfaceMethods.PrintStudents(students);
            }
            else if (!name && asc)
            {
                var students = context.Students
                    .Where(s => s.StudentId > 0)
                    .OrderBy(s => s.LName);
                InterfaceMethods.PrintStudents(students);
            }
            else
            {
                var students = context.Students
                    .Where(s => s.StudentId > 0)
                    .OrderByDescending(s => s.LName);
                InterfaceMethods.PrintStudents(students);
            }
        }

        public static void ListStudentClass()
        {
            var context = new SkolanDbContext();

            // Get all classes
            var classes = context.Classes
                .Where(c => c.ClassId > 0)
                .OrderBy(c => c.ClassId);

            InterfaceMethods.PrintClasses(classes);
            Console.WriteLine("Välj vilken klass du vill visa: ");
            int classIdChoice = InterfaceMethods.CheckInput(classes.Count());

            bool name = InterfaceMethods.FirstName();
            bool asc = InterfaceMethods.Ascending();

            // Get the chosen class
            var chosenClass = context.Classes.Single
                (c => c.ClassId == classIdChoice);

            Console.WriteLine($"\nI klass " +
                $"{chosenClass.ClassName} går följande elever:");

            if (name && asc)
            {
                var students = context.Students
                    .Where(s => s.ClassId == classIdChoice)
                    .OrderBy(s => s.FName);
                InterfaceMethods.PrintStudents(students);
            }
            else if (name && !asc)
            {
                var students = context.Students
                    .Where(s => s.ClassId == classIdChoice)
                    .OrderByDescending(s => s.FName);
                InterfaceMethods.PrintStudents(students);
            }
            else if (!name && asc)
            {
                var students = context.Students
                    .Where(s => s.ClassId == classIdChoice)
                    .OrderBy(s => s.LName);
                InterfaceMethods.PrintStudents(students);
            }
            else
            {
                var students = context.Students
                    .Where(s => s.ClassId == classIdChoice)
                    .OrderByDescending(s => s.LName);
                InterfaceMethods.PrintStudents(students);
            }
        }

        public static void AddStaff()
        {
            var context = new SkolanDbContext();

            AddressBook address = new AddressBook();

            Console.WriteLine("Ange gatuadress:");
            address.Street = InterfaceMethods.UserStringInput();

            Console.WriteLine("Ange postkod:");
            address.PostalCode = InterfaceMethods.UserIntInput();

            Console.WriteLine("Ange stad:");
            address.City = InterfaceMethods.UserStringInput();

            context.AddressBooks.Add(address);
            context.SaveChanges();


            Staff newEmployee = new Staff();
            newEmployee.AddressId = context.AddressBooks
                .OrderByDescending(a => a.AddressId).FirstOrDefault().AddressId;

            Console.WriteLine("Ange personnummer:");
            newEmployee.SocialNr = InterfaceMethods.UserStringInput();

            Console.WriteLine("Ange förnamn:");
            newEmployee.FName = InterfaceMethods.UserStringInput();

            Console.WriteLine("Ange efternamn:");
            newEmployee.LName = InterfaceMethods.UserStringInput();

            Console.WriteLine("Ange position:");
            newEmployee.Position = InterfaceMethods.UserStringInput();

            context.Staff.Add(newEmployee);
            context.SaveChanges();


            // If the new employee is a teacher, add it to the teacher table
            if (newEmployee.Position == "Lärare")
            {
                Teacher teacher = new Teacher();
                teacher.StaffId = context.Staff.OrderByDescending
                    (s => s.StaffId).FirstOrDefault().StaffId;
                context.Teachers.Add(teacher);
            }
            context.SaveChanges();

            Console.WriteLine($"{newEmployee.FName} har nu blivit tillagd i" +
                $" systemet.");
            InterfaceMethods.PressToCont();
        }

        public static void ListTeachers()
        {
            var context = new SkolanDbContext();

            var teachers = from s in context.Staff
                           join d in context.Departments on s.DepartmentId equals d.DepartmentId
                           orderby d.DepartmentId ascending
                           where s.Position == "Lärare"
                           select new
                           {
                               Name = s.FName + " " + s.LName,
                               Dep = d.DepName,
                               DepID = d.DepartmentId
                           };

            int depId = 0;
            foreach (var item in teachers)
            {
                if (depId < item.DepID)
                {
                    Console.WriteLine("\n" + item.Dep + ":");
                    depId = item.DepID;
                }
                Console.WriteLine(item.Name);
            }

            InterfaceMethods.PressToCont();
        }

        public static void ListStudentsDetailed()
        {
            var context = new SkolanDbContext();

            var students = from stud in context.Students
                           join add in context.AddressBooks on stud.AddressId equals add.AddressId
                           join cla in context.Classes on stud.ClassId equals cla.ClassId
                           orderby stud.LName ascending
                           select new
                           {
                               _name = stud.FName + " " + stud.LName,
                               _class = cla.ClassName,
                               _address = add.Street + ", " + add.PostalCode + " " + add.City,

                           };

            foreach (var item in students)
            {
                Console.WriteLine($"{item._name} : {item._class} : {item._address}");
            }

            InterfaceMethods.PressToCont();
        }

        public static void ViewActiveCourses()
        {
            var context = new SkolanDbContext();

            var activeCourses = context.Courses
                                .Where(c => c.Active == true);

            Console.WriteLine("Dessa kurser pågår just nu:");
            foreach (Course item in activeCourses)
            {
                Console.WriteLine(item.CourseName);
            }

            InterfaceMethods.PressToCont();
        }
    }
}
