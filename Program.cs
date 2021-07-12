using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
using System.Linq;


namespace ConsoleApp
{
    class MainClass
    {
        public static SchoolService studentService = new SchoolService();
        public static int schoolID;
        public static int rollNumber;
        public static string selectedSchool;
        public static int id;
        public static List<School> schoolJsonList;
        public static string fileName;
        public enum StudentMenuOptions
        {
            AddStudent=1, AddMarks=2, Progress=3, SchoolMenu=4
        }
        public enum SchoolMenuOptions
        {
            Add=1, Manage=2, Exit=3
        }
        static string[,] s = new string[,]
        {
            {"Kannada","English","Hindi","Maths","Science","Social"},
            {"Telugu","English","Hindi","Maths","Science","Social"},
            {"Tamil","English","Hindi","Maths","Science","Social" },
            {"Bengali","English","Hindi","Maths","Science","Social" }
        };

        public static void Main(string[] args)
        {
            
            SchoolMenu();
        }


        public static void SchoolMenu()
        {
            fileName = @"C:\Users\Admin\source\repos\student-info-portal-task-2-refactored\bin\Debug\netcoreapp3.1\SchoolDetails.json";
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            string json = File.ReadAllText(fileName);
            schoolJsonList = JsonConvert.DeserializeObject<List<School>>(json);

            SchoolService.schoolList = schoolJsonList;
            
            Console.WriteLine("Enter valid option");
            Console.WriteLine("1. Add school\n");
            Console.WriteLine("2. Manage School \n");
            Console.WriteLine("3. Exit \n");

            if (Enum.TryParse<SchoolMenuOptions>(Console.ReadLine(), ignoreCase: true, out var schoolOption))
            {
                switch (schoolOption)
                {
                    case SchoolMenuOptions.Add:
                        {
                            Console.WriteLine("Add School");
                            SchoolDetails();
                            break;
                        }
                    case SchoolMenuOptions.Manage:
                        {
                            Console.WriteLine("Manage School");
                            ChooseSchool();
                            break;
                        }
                    case SchoolMenuOptions.Exit:
                        {
                            Console.WriteLine("Exit");
                            studentService.ExitApplication();
                            break;
                        }
                    default:
                        break;

                }
            }
            else
            {
                Console.WriteLine("enter valid options");
                SchoolMenu();
            }
        }


        public static void SchoolDetails()
        {

            for (int i = 0; i < schoolJsonList.Count; i++)
            {
                Console.WriteLine("School Name : " + schoolJsonList[i].Name + " || School ID : " + schoolJsonList[i].ID + "\n");
            }
            Console.WriteLine("Enter School Name:");
            string schoolName = Console.ReadLine();
            
            if (!studentService.CheckStringValidity(schoolName) || schoolName.Length < 3)
            {
                Console.WriteLine("Enter valid school name");
                SchoolDetails();
            }
            if (new FileInfo(fileName).Length==0)
            {
                id += 1;
                studentService.AddSchool(schoolName, id);
            }
            bool schoolExists = schoolJsonList.Any(_ => _.Name == schoolName);
            if (schoolExists)
            {
                Console.WriteLine("School with that name already exists.");
                SchoolDetails();
            }
            id += 1;
            bool schoolIDExists = schoolJsonList.Any(_ => _.ID ==id);
            while (schoolIDExists)
            {
                id += 1;
                schoolIDExists = schoolJsonList.Any(_ => _.ID == id);
            }
            //School school=schoolJsonList.
            studentService.AddSchool(schoolName, id);

            Console.WriteLine("Do you want to add another school? y/n");
            char c = Console.ReadLine()[0];
            if (c == 'y')
            {
                SchoolDetails();
            }
            SchoolMenu();
        }

        public static void ChooseSchool()
        {
            
            if (SchoolService.schoolList.Count == 0)
            {
                Console.WriteLine("Add a school first");
                SchoolDetails();
            }
            for(int i = 0; i < schoolJsonList.Count; i++)
            {
                Console.WriteLine("School Name : " + schoolJsonList[i].Name + " || School ID : " + schoolJsonList[i].ID + "\n");
            }
            Console.WriteLine("Enter school ID");
            schoolID = int.Parse(Console.ReadLine());
            School school = schoolJsonList.Find(_ => _.ID == schoolID);
            selectedSchool = school.Name;
            Console.WriteLine("Main Menu\n------------------");
            Console.WriteLine("Welcome to " + selectedSchool + " Student Information Portal ");
            MainMenu();
        }

        public static void MainMenu()
        {
            
            Console.WriteLine("1. Add student \n");
            Console.WriteLine("2. Add marks for student \n");
            Console.WriteLine("3. Show student progress card \n");
            Console.WriteLine("4. Go back to school menu \n");
            Console.WriteLine("Please provide valid input from menu options :");
            
            try
            {
                if (Enum.TryParse<StudentMenuOptions>(Console.ReadLine(), ignoreCase: true, out var option))
                    switch (option)
                    {
                        case StudentMenuOptions.AddStudent:
                            {
                                DisplayAddStudent();
                                break;
                            }
                            
                        case StudentMenuOptions.AddMarks:
                            {
                                DisplayAddMark();
                                break;
                            }
                            
                        case StudentMenuOptions.Progress:
                            {
                                DisplayProgressCard(rollNumber);
                                break;
                            }
                            
                        case StudentMenuOptions.SchoolMenu:
                            {
                                SchoolMenu();
                                break;
                            }
                            
                        default:
                            break;
                    }
                    
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Enter valid data");
                MainMenu();
            }

        }

        public static void DisplayAddStudent()
        {
            
            Console.WriteLine("1. Add Student\n ----------------------------------");
            try
            {
                Console.WriteLine("Enter Student Roll Number :");
                int rollNumber;
                bool checkInt = int.TryParse(Console.ReadLine(), out rollNumber);
                //roll number validation
                if (!checkInt || rollNumber <= 0)
                {
                    Console.WriteLine("Enter valid roll number");
                    DisplayAddStudent();
                }
                if (checkInt)
                {
                    School school = schoolJsonList.Find(_ => _.ID == schoolID);
                    bool studentExists = school.studentList.Any(_ => _.RollNumber == rollNumber);
                    if (studentExists)
                    {
                        Console.WriteLine("Student with that roll number already exists. Enter valid roll number.");
                        DisplayAddStudent();
                    }
                    else
                    {
                        Console.WriteLine("Enter Student Name :");
                        string name = Console.ReadLine();
                        while (!studentService.CheckStringValidity(name) || name.Length < 3)
                        {
                            Console.WriteLine("Enter valid name");
                            name = Console.ReadLine();
                        }
                        if(studentService.AddStudent(name, rollNumber, schoolID))
                        {
                            Console.WriteLine("Student details added successfully");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Enter valid roll number");
                    DisplayAddStudent();
                }
                MainMenu();
            }
            catch (Exception)
            {
                Console.WriteLine("Enter valid data.");
                MainMenu();
            }

        }

        public static void DisplayAddMark()
        {
            float[] array = new float[6];//array of size 6 for 6 subjects.

            School school = schoolJsonList.Find(_ => _.ID == schoolID);
            if (school.studentList.Count == 0)
            {
                Console.WriteLine("Add a student first");
                DisplayAddStudent();
            }

         
            Console.WriteLine("2. Add Marks\n ---------------------");
            Console.WriteLine("Enter Student Roll Number :");

            int rollNumber;
            bool checkInteger = int.TryParse(Console.ReadLine(), out rollNumber);
            while (!checkInteger || rollNumber <= 0)
            {
                Console.WriteLine("Enter valid roll number");
                checkInteger = int.TryParse(Console.ReadLine(), out rollNumber);
            }

            bool studentExists = school.studentList.Any(_ => _.RollNumber == rollNumber);

            if (!studentExists)
            {
                Console.WriteLine("Student does not exist, add student first\n");
                Console.WriteLine("Do you want to add a new student? y/n");
                char c = Console.ReadLine()[0];
                if (c == 'y')
                {
                    DisplayAddStudent();
                }
                if (c == 'n')
                {
                    DisplayAddMark();
                }
                if (c != 'y' || c != 'n')
                {
                    Console.WriteLine("Enter y or n");
                    DisplayAddMark();
                }
            }

            Student student = school.studentList.Find(_ => _.RollNumber == rollNumber);
            if (student.Marks.Count != 0)
            {
                Console.WriteLine("Are you sure you want to change the marks? y/n");
                char c = Console.ReadLine()[0];
                if (c == 'n')
                {
                    DisplayAddMark();
                }
                if (c != 'y')
                {
                    Console.WriteLine("Enter y or n");
                    MainMenu();
                }
            }
            
            

            try
            {
                for (int i = 0; i < s.GetLength(0); i++)
                {
                    Console.WriteLine("Subject Code : " + (i + 1));
                    Console.WriteLine("-------------------------------");
                    for (int j = 0; j < s.GetLength(1); j++)
                    {
                        Console.WriteLine(s[i, j]);
                    }
                    Console.WriteLine("-------------------------------");
                }

                Console.WriteLine("Enter subject code");

                bool checkSubjectCode = int.TryParse(Console.ReadLine(), out SchoolService.subjectCode);

                while (!checkSubjectCode || SchoolService.subjectCode > s.GetLength(0))
                {
                    Console.WriteLine("Enter valid subject code");
                    checkSubjectCode = int.TryParse(Console.ReadLine(), out SchoolService.subjectCode);
                }
                
                float totalMarks = 0;
                float percentage = 0;
                for (int i = 0; i < s.GetLength(1); i++)
                {
                    int k = SchoolService.subjectCode - 1;
                    Console.WriteLine("Enter " + s[k, i] + " marks");
                    float mark;
                    checkInteger = float.TryParse(Console.ReadLine(), out mark);
                    while (!checkInteger || !studentService.checkValidity(mark))
                    {
                        Console.WriteLine("Enter valid " + s[SchoolService.subjectCode - 1, i] + " marks");
                        checkInteger = float.TryParse(Console.ReadLine(), out mark);
                    }
                    if (checkInteger)
                    {
                        array[i] = mark;//if entered marks is valid, add to the array.

                        totalMarks += array[i];
                    }
                    else
                    {
                        if (checkInteger)
                        {
                            while (!studentService.checkValidity(mark))
                            {
                                Console.WriteLine("Enter valid" + s[SchoolService.subjectCode - 1, i] + " marks");
                                checkInteger = float.TryParse(Console.ReadLine(), out mark);
                            }
                        }
                        
                    }
                }

                percentage = ((float)(totalMarks * 100)) / 600;
                SchoolService.marks = array.ToList();  //convert marks array to list so array can be overwritten later on.
                studentService.AddMark(rollNumber, totalMarks, percentage, schoolID);
                

            }
            catch (Exception)
            {
                Console.WriteLine("Enter valid integers");
                MainMenu();
            }
            MainMenu();
        }

        public static void DisplayProgressCard(int rollNumber)
        {
            Console.WriteLine("3. Show student progress card\n --------------------------");
            School school = schoolJsonList.Find(_ => _.ID == schoolID);
            if (school.studentList.Count == 0)
            {
                Console.WriteLine("Add a student first");
                DisplayAddStudent();
            }
            Console.WriteLine("Enter student roll number :");
            int rollNum;
            bool checkInt = int.TryParse(Console.ReadLine(), out rollNum);

            if (checkInt)
            {
                //Console.WriteLine(schoolList.Count);
                bool studentExists = school.studentList.Any(_ => _.RollNumber == rollNum);
                if (!studentExists)
                {
                    Console.WriteLine("Student with that roll number does not exist.");
                    Console.WriteLine("Enter valid roll number or enter 0 to go back to main menu");
                    int option;
                    checkInt = int.TryParse(Console.ReadLine(), out option);
                    if (option == 0)
                    {
                        MainMenu();
                    }
                    else
                    {
                        rollNum = option;
                        while (!checkInt || rollNum < 0)
                        {
                            Console.WriteLine("Enter valid roll number");
                        }
                        DisplayProgressCard(rollNum);
                    }
                }
                try
                {

                    Student student = school.studentList.Find(_ => _.RollNumber == rollNum);
                    if (student.Marks.Count == 0)
                    {
                        Console.WriteLine("marks not added");
                        DisplayAddMark();
                    }
                    Console.WriteLine("Student Name : " + student.Name);
                    Console.WriteLine("Student roll number : " + student.RollNumber);
                    Console.WriteLine("-------------------------------");
                    int temp = SchoolService.subjectCode - 1;
                    for(int p = 0; p < student.Marks.Count; p++)
                    {
                        Console.WriteLine(s[temp, p] + " : " + student.Marks[p]);
                    }
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine("Total Marks : " + student.TotalMarks);
                    Console.WriteLine("percentage : " + student.Percentage);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
                
            }
            else
            {
                Console.WriteLine("enter valid roll number");
                DisplayProgressCard(rollNumber);
            }
            
            
            MainMenu();

        }

    }
}
