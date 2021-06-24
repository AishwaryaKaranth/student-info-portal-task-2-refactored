using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class School{
  private string _schoolName;
  public string setSchoolName(string _schoolName){
    this._schoolName=_schoolName;
    return _schoolName;
  }
}

class Student{

  //mainClass m =new mainClass();
  private string _name;
  private int _rollNumber;
  List<string>namesList=new List<string>();
  List<int>rollNumberList=new List<int>();
  List<int>marksList=new List<int>();

  string[] subjects={"Telugu","English","Hindi","Maths","Science","Social"};

  public List<string> GetNamesList(){
    return namesList;
  }
  
  public List<int>GetRollNumberList(){
    return rollNumberList;
  }

  public List<int>GetMarkslist(){
    return marksList;
  }

  public void AddStudentDetails(string name, int rollNumber){
    this._name=_name;
    this._rollNumber=_rollNumber;
    namesList.Add(name);
    rollNumberList.Add(rollNumber);
  }

  public void AddMarks(int mark){
    marksList.Add(mark);
  }
  
  public void ShowProgressCard(int rollNumber, List<int>marksList){
    if(namesList.Count==0){
      Console.WriteLine("Add a student first");
    }
    if(!rollNumberList.Contains(rollNumber)){
      Console.WriteLine("Student with that roll number does not exist");
    }
    var totalmarks=0;
    //var percentage=0;
    Console.WriteLine("Student roll number : "+rollNumber);
    Console.WriteLine("Student Name : "+namesList[rollNumber-1]);
    Console.WriteLine("Student Marks");
    Console.WriteLine("-------------------------------------");
   // Console.WriteLine(marksList.Count);
    for(int i=0;i<marksList.Count;i++){
      Console.WriteLine(subjects[i]+" : "+marksList[i]);
      //Console.WriteLine(totalmarks+"\n");
      totalmarks+=marksList[i];
    }
    //Console.WriteLine(percentage);
    float percentage=0;
    percentage=((float)(totalmarks * 100))/600;
    Console.WriteLine("---------------------------------------");
    Console.WriteLine("Total Marks : "+totalmarks);
    Console.WriteLine("\n");
    Console.WriteLine("Percentage : "+percentage);
    Console.WriteLine("----------------------------------------");

    Console.WriteLine("press any key to continue");
    Console.ReadKey();
    //m.SchoolDetails();
  }

}

class MainClass{
  static int rollNumber;
  static School sc=new School();
  static Student st=new Student();
  static List<int>marks=st.GetMarkslist();
  public static void Main(string[]args){
    SchoolDetails();
  }


  public static void SchoolDetails(){
    Console.WriteLine("Enter School Name:");
    string schoolName=Console.ReadLine();

    //School sc= new School();
    var t=sc.setSchoolName(schoolName);

    if(!CheckStringValidity(t) || t.Length<3){
      Console.WriteLine("Enter valid school name");
      SchoolDetails();
    }
    Console.WriteLine("Welcome to "+schoolName+" Student Information Portal");
    Console.WriteLine("--------------------------------");
    MainMenu();
  }

  public static bool CheckStringValidity(string name){
    bool valid=true;
    if(string.IsNullOrEmpty(name))
      valid=false;
    else{
      valid=Regex.IsMatch(name, @"^[a-zA-Z]+$");
      foreach(char c in name){
        if(!Char.IsLetter(c))
          valid=false;
      }
    }
    return valid;
  }

  public static bool CheckOptionValidity(int option){

    if(option!=1 && option!=2 && option!=3 && option!=4){
      Console.WriteLine("Enter valid option");
      option=int.Parse(Console.ReadLine());
      CheckOptionValidity(option);
    }
    return true;
  }


  public static void MainMenu(){
    //Student st=new Student();
    Console.WriteLine("1. Add student\n");
    Console.WriteLine("2. Add marks for student\n");
    Console.WriteLine("3. Show student progress card\n");
    Console.WriteLine("4. Exit the application\n");
    Console.WriteLine("Please provide valid input from menu options :");
    //int option=int.Parse(Console.ReadLine());
    int option;
    bool checkInteger = int.TryParse(Console.ReadLine(), out option);
    //Console.WriteLine(checkInteger);
    try{
      if(checkInteger){
        if(CheckOptionValidity(option)){
        //mainMenu();
          switch(option){
          case 1: AddStudent();
          break;
          case 2: AddMark();
          break;
          case 3: ProgressCard(rollNumber, marks);
          break;
          case 4: ExitApplication();
                  break;
          default:
                break;
        }
      }
    }else{
      Console.WriteLine("enter valid option");
      MainMenu();
    }
    }catch(Exception e){
      Console.WriteLine("Enter valid data");
      MainMenu();
    }
    
  }


  public static void MarksMenu(){
    //Student st=new Student();
    Console.WriteLine("1. Add student\n-----------------------");
    //Console.WriteLine("2. Add marks for student\n");
    Console.WriteLine("2. Show student progress card\n");
    Console.WriteLine("3. Exit the application\n");
    Console.WriteLine("Please provide valid input from menu options :");
    //int option=int.Parse(Console.ReadLine());
    int option;
    bool checkInteger = int.TryParse(Console.ReadLine(), out option);
    //Console.WriteLine(checkInteger);
    try{
      if(checkInteger){
        if(CheckOptionValidity(option)){
        //mainMenu();
          switch(option){
          case 1: AddStudent();
          break;
          case 2: ProgressCard(rollNumber, marks);
          break;
          case 3: ExitApplication();
                  break;
          default:
                break;
        }
      }
    }else{
      Console.WriteLine("enter valid option");
      MarksMenu();
    }
    }catch(Exception e){
      Console.WriteLine("Enter valid data");
      MarksMenu();
    }
    
  }


  public static void AddStudent(){
    Console.WriteLine("1. Add Student\n ----------------------------------");
    try{
    List<int>roll=st.GetRollNumberList();
    Console.WriteLine("Enter Student Roll Number :");
    int rollNumber;
    bool checkInt=int.TryParse(Console.ReadLine(), out rollNumber);
    if(roll.Contains(rollNumber)){
      Console.WriteLine("Student with this roll number already exists");
      AddStudent();
    }
    if(!checkInt || rollNumber<=0){
      Console.WriteLine("Enter valid roll number");
      AddStudent();
    }
    Console.WriteLine("Enter Student Name :");
    string name=Console.ReadLine();
    if(!CheckStringValidity(name) || name.Length<3){
      Console.WriteLine("Enter valid name");
      AddStudent();
    }
    //Student st = new Student();
    if(checkInt){
      st.AddStudentDetails(name, rollNumber);
    }else{
      Console.WriteLine("Enter valid roll number");
      AddStudent();
    }
    //st.AddStudentDetails(name, rollNumber);

    Console.WriteLine("Student details successfully added.");
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    MainMenu();
    }
    catch(Exception e){
      Console.WriteLine("Enter valid data."+ e);
      MainMenu();
    }
    
  }

  
  public static bool checkValidity(int marks){
    if(marks<0 || marks>100){
      Console.WriteLine("enter valid marks");
      AddMark();
    }
    return true;
  }

  public static void AddMark(){
    //Student st=new Student();
    Console.WriteLine("2. Add Marks\n ---------------------");
    List<string>name=st.GetNamesList();
    if(name.Count==0){
      Console.WriteLine("Add a student first");
      AddStudent();
    }
    Console.WriteLine("Enter Student Roll Number :");
    int rollNumber;
    bool checkInteger=int.TryParse(Console.ReadLine(),out rollNumber);
    if(!checkInteger || rollNumber<=0){
      Console.WriteLine("Enter valid roll number");
      AddMark();
    }
    List<int>roll=st.GetRollNumberList();
    if(!roll.Contains(rollNumber)){
      Console.WriteLine("Student does not exist, add student first\n");
      Console.WriteLine("Do you want to add a new student? y/n");
      char c=Console.ReadLine()[0];
      if(c=='y'){
        AddStudent();
      }
      if(c=='n'){
        AddMark();
      }
      if(c!='y' || c!='n'){
        Console.WriteLine("Enter y or n");
        AddMark();
      }
    }
    
    if(marks.Count!=0){
      Console.WriteLine("Are you sure you want to change the marks? y/n");
      char c=Console.ReadLine()[0];
      if(c=='n'){
        MarksMenu();
      }
      if(c=='y'){
        int count=marks.Count;
        marks.RemoveRange(0,count);
      }
      if(c!='y' || c!='n'){
        Console.WriteLine("Please enter 'y' or 'n'");
        AddMark();
      }
    }
    try{
      string[] subjects={"Telugu","English","Hindi","Maths","Science","Social"};
      for(int i=0;i<6;i++){
        Console.WriteLine("Enter "+subjects[i]+" marks");
        int mark;
        checkInteger=int.TryParse(Console.ReadLine(),out mark);
        if(checkInteger && checkValidity(mark)){
          //checkValidity(mark);
          st.AddMarks(mark);
        }
        else{
          Console.WriteLine("Enter valid marks");
          AddMark();
        }
        
      }
    }
    catch(Exception e){
      Console.WriteLine("Enter valid integers");
      MainMenu();
    }
  

    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    MainMenu();
  }


  public static void ExitApplication(){
    Console.WriteLine("Exiting the application...");
    Environment.Exit(0);
    
  }

  public static void ProgressCard(int rollNumber, List<int>marks){
    //Student st=new Student();
    Console.WriteLine("3. Show student progress card\n ---------------------------");
    List<string>names=st.GetNamesList();
    if(names.Count==0){
      Console.WriteLine("Add a student first");
      MainMenu();
    }
    
    Console.WriteLine("Enter student roll number :");
    int rollNum;
    bool checkInt=int.TryParse(Console.ReadLine(), out rollNum);
    if(checkInt){
      st.ShowProgressCard(rollNum, marks);
    }else{
      Console.WriteLine("enter valid roll number");
      ProgressCard(rollNumber, marks);
    }
    //Console.WriteLine("Student with that roll number does not exist");
    List<int>roll=st.GetRollNumberList();
    if(!roll.Contains(rollNum)){
      Console.WriteLine("Student with that roll number does not exist");
      MainMenu();
    }
    MainMenu();
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    SchoolDetails();
  }
}
