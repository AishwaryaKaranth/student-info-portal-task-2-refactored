using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Linq;

namespace ConsoleApp
{
    class SchoolService
    {

        public static List<School> schoolList = new List<School>();
        public static List<float> marks = new List<float>();
        public static int subjectCode;
        public static string jsonString;
        public static string fileName= @"C:\Users\Admin\source\repos\student-info-portal-task-2-refactored\SchoolDetails.json";
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
            jsonString = JsonConvert.SerializeObject(schoolList, Formatting.Indented);
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine("----------------------------------");
            }
            File.WriteAllText(fileName, jsonString);
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
                jsonString = JsonConvert.SerializeObject(schoolList, Formatting.Indented);
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine("----------------------------------");
                }
                File.WriteAllText(fileName, jsonString);
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

        public bool checkValidity(float marks)
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
                Student student = school.studentList.Find(_ => _.RollNumber == rollNumber);
                student.Marks = marks;
                student.TotalMarks = totalMarks;
                //Console.WriteLine(student.TotalMarks);
                student.Percentage = percentage;
                jsonString = JsonConvert.SerializeObject(schoolList, Formatting.Indented);
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine("----------------------------------");
                }
                File.WriteAllText(fileName, jsonString);
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
