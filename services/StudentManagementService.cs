using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace ConsoleApp
{
    class SchoolService
    {

        public static List<School> schoolList = new List<School>();
        public static List<float> marks = new List<float>();
        public static int subjectCode;
        static string[,] s = new string[,]
        {
            {"Kannada","English","Hindi","Maths","Science","Social"},
            {"Telugu","English","Hindi","Maths","Science","Social"},
            {"Tamil","English","Hindi","Maths","Science","Social" },
            {"Bengali","English","Hindi","Maths","Science","Social" }
        };

       
        public void AddSchool(string schoolName, int id)
        {
            schoolList.Add(new School(schoolName, id));
        }


        public bool CheckStringValidity(string name)
        {
            bool valid = true;
            if (string.IsNullOrEmpty(name))
                valid = false;
            else
            {
                valid = Regex.IsMatch(name, @"^[a-zA-Z]+$");
                foreach (char c in name)
                {
                    if (!Char.IsLetter(c))
                        valid = false;
                }
            }
            return valid;
        }

        
        public bool AddStudent(string name, int rollNumber, int schoolID)
        {
            try
            {
                School school = schoolList.Find(_ => _.ID == schoolID);
                school.studentList.Add(new Student(name, rollNumber, schoolID));
                bool studentAdded = school.studentList.Any(_ => _.RollNumber == rollNumber);
                if (studentAdded)
                {
                    return true;
                }
                return false;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public bool checkValidity(int marks)
        {
            if (marks < 0 || marks > 100)
            {
                //Console.WriteLine("enter valid marks");
                return false;
            }
            return true;
        }

        public void AddMark(int rollNumber, float totalMarks, float percentage, int schoolID)
        {
            try
            {
                School school = schoolList.Find(_ => _.ID == schoolID);
                for (int i = 0; i < school.studentList.Count; i++)
                {
                    if (school.studentList[i].RollNumber == rollNumber)
                    {
                        school.studentList[i].Marks = marks;
                        school.studentList[i].TotalMarks = totalMarks;
                        school.studentList[i].Percentage = percentage;
                        //school.studentList[i].SubjectCode = subjectCode;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error occured");
            }
            
        }

        public void ExitApplication()
        {
            Console.WriteLine("Exiting the application...");
            Environment.Exit(0);

        }


    }
}
