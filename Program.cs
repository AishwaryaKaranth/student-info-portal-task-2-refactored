using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class School{
  private string schoolName;
  public string setSchoolName(string schoolName){
    this.schoolName=schoolName;
    return schoolName;
  }
}

class Student{

  //mainClass m =new mainClass();
  private string name;
  private int rollNumber;
  List<string>namesList=new List<string>();
  List<int>rollNumberList=new List<int>();
  List<int>MarksList=new List<int>();

  string[] subjects={"Telugu","English","Hindi","Maths","Science","Social"};

  public List<string> getNamesList(){
    return namesList;
  }
  
  public List<int>getRollNumberList(){
    return rollNumberList;
  }

  public List<int>getMarkslist(){
    return MarksList;
  }

  public void AddStudentDetails(string name, int rollNumber){
    this.name=name;
    this.rollNumber=rollNumber;
    namesList.Add(name);
    rollNumberList.Add(rollNumber);
  }

  public void AddMarks(int mark){
    MarksList.Add(mark);
  }
  
  public void ShowProgressCard(int rollNumber){
    var totalmarks=0;
    var percentage=0;
    Console.WriteLine("Student roll number : "+rollNumber);
    Console.WriteLine("Student Name : "+namesList[rollNumber-1]);
    Console.WriteLine("Student Marks");
    Console.WriteLine("-------------------------------------");
    for(int i=0;i<MarksList.Count;i++){
      Console.WriteLine(subjects[i]+" : "+MarksList[i]);
      totalmarks+=MarksList[i];
    }
    percentage=(totalmarks * 100)/600;
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

class mainClass{
  static int rollNumber;
  static School sc=new School();
  static Student st=new Student();
  public static void Main(string[]args){
    SchoolDetails();
  }


  public static void SchoolDetails(){
    Console.WriteLine("Enter School Name:");
    string schoolName=Console.ReadLine();

    //School sc= new School();
    var t=sc.setSchoolName(schoolName);

    if(!CheckStringValidity(t)){
      Console.WriteLine("Enter valid school name");
      SchoolDetails();
    }
    Console.WriteLine("Welcome to "+schoolName+" Student Information Portal");
    Console.WriteLine("--------------------------------");
    mainMenu();
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


  public static void mainMenu(){
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
          case 3: ProgressCard(rollNumber);
          break;
          case 4: ExitApplication();
                  break;
          default:
                break;
        }
      }
    }else{
      Console.WriteLine("enter valid option");
      mainMenu();
    }
    }catch(Exception e){
      Console.WriteLine("Enter valid data");
    }
    
  }

  public static void AddStudent(){
    try{
    Console.WriteLine("Enter Student Roll Number :");
    int rollNumber;
    bool checkInt=int.TryParse(Console.ReadLine(), out rollNumber);
    if(!checkInt){
      Console.WriteLine("Enter valid roll number");
      AddStudent();
    }
    Console.WriteLine("Enter Student Name :");
    string name=Console.ReadLine();
    if(!CheckStringValidity(name)){
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
    mainMenu();
    }
    catch(Exception e){
      Console.WriteLine("Enter valid data."+ e);
      mainMenu();
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

    Console.WriteLine("Enter Student Roll Number :");
    int rollNumber;
    bool checkInteger=int.TryParse(Console.ReadLine(),out rollNumber);
    if(!checkInteger){
      Console.WriteLine("Enter valid roll number");
      AddMark();
    }
    List<int>roll=st.getRollNumberList();
    if(!roll.Contains(rollNumber)){
      Console.WriteLine("Student does not exist");
    }
    try{
      string[] subjects={"Telugu","English","Hindi","Maths","Science","Social"};
      for(int i=0;i<6;i++){
        Console.WriteLine("Enter "+subjects[i]+" marks");
        int mark;
        checkInteger=int.TryParse(Console.ReadLine(),out mark);
        if(checkInteger){
          checkValidity(mark);
          st.AddMarks(mark);
        }else{
          Console.WriteLine("Enter valid marks");
          AddMark();
        }
        
      }
    }
    catch(Exception e){
      Console.WriteLine("Enter valid integers");
      mainMenu();
    }
  

    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    mainMenu();
  }


  public static int ExitApplication(){
    Console.WriteLine("Exiting the application...");
    return 0;
  }

  public static void ProgressCard(int rollNumber){
    //Student st=new Student();
    List<string>names=st.getNamesList();
    if(names.Count==0){
      Console.WriteLine("Add a student first");
      mainMenu();
    }
    
    Console.WriteLine("Enter student roll number :");
    int rollNum=int.Parse(Console.ReadLine());
    st.ShowProgressCard(rollNum);
    mainMenu();
    
    
    
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    SchoolDetails();
  }
}
