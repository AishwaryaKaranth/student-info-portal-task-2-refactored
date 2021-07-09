using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace ConsoleApp
{
    class Student
    {
        public string Name;
        public int SubjectCode;
        public int RollNumber;
        public float TotalMarks;
        public float Percentage;
        public int sID;
        public List<float> Marks = new List<float>();
        
        public Student() { }
        public Student(string name, int rollNumber, int sID)
        {
            this.Name = name;
            this.RollNumber = rollNumber;
            this.sID = sID;
        }

    }

}
