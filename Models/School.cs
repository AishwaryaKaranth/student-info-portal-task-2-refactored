using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace ConsoleApp
{
    class School
    {
        public string Name;
        public int ID;
        public List<Student> studentList = new List<Student>();

        public School() { }
        public School(string schoolName, int id)
        {
            this.Name = schoolName;
            //id += 1;
            Console.WriteLine("id " + id);
            this.ID = id;
        }

    }

}