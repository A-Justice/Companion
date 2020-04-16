using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOops;
using ViewModels.BaseClasses;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Reflection;


namespace ViewModels.ViewModels
{
    
  public  class RegistrationViewModel:ObservableObjects,IDataErrorInfo
    {
      
       

        #region Commands

        #region CommandProperties
        public RelayCommand SubmitButtonCommand
        {
            get;set;
        }

        #endregion

        #region CommandMethods
        public void SubmitButtonExecute(object parameter)
        {
            bool doesStudentExist = false;

            if (Directory.Exists(DirectoryManager.AccountsDirectory) && (string)parameter == "")
            {
                Student tempStudent= DirectoryManager.ReadStudentobject(FirstName, LastName, Email);
                if (tempStudent != null)
                {
                    doesStudentExist = true;
                    verified = false;
                    ErrorEmail = "This Email Already Exist";
                }
                else
                    ErrorEmail = string.Empty;
            
            }

            if (this.Error==null && doesStudentExist == false && (string)parameter=="")
            {
               
                    //just trying to invoke the setter in the Gender property to carry out the logic that is built into it.
                    this.Gender = "";
                    Student newStudent = new Student(FirstName, LastName, Gender, BirthDate, Program, CurrentLevel, CurrentSemester, Institution, Email);
 
                DirectoryManager.CreatePersonalDirectory(ref newStudent);
                    verified = true;
                CurrentStudent = Student.Login(FirstName, Email);


            }
            
        }
    
        public bool SubmitButtonCanExecute(object parameter)
        {
            return this.Error == null ? true : false;
            
        }
        #endregion

        #endregion

    
        #region Constructor
        public RegistrationViewModel()
        {
            DirectoryManager.CreateDirectories();
            SubmitButtonCommand = new RelayCommand(SubmitButtonExecute, SubmitButtonCanExecute);
        }

        #endregion

        #region privateVariables
        string _firstName;
        string _Gender;
        string _lastName;
        Program _program ;
        Semester _currentSemester;
        string _institution;
        Level _currentLevel;
        string _email;
        DateTime _birthdate;
        string _erroremail;
       

        #endregion

        #region publicVariables
        public bool verified = false;
        public static Student CurrentStudent;
        #endregion

        #region publicProperties
        public bool Male { get; set;}

        public bool Female { get; set; }
        public string FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged(); } }

        public string LastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged(); } }

        public string Gender
        {
            get { return _Gender; }

            set {
                if (Male == true)
                {
                    _Gender = "Male";
                }
                else if (Female == true)
                {
                    _Gender = "Female";
                }
                else
                    _Gender = null;
                
                
            }
        }

        public Program Program { get { return _program; } set { _program = value; OnPropertyChanged(); } }

        public Semester CurrentSemester { get { return _currentSemester; } set { _currentSemester = value; OnPropertyChanged(); } }

        public string Institution { get { return _institution; } set { _institution = value; OnPropertyChanged(); } }

        public Level CurrentLevel { get { return _currentLevel; } set { _currentLevel = value; OnPropertyChanged(); } }

        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }

        public string ErrorEmail
        {
            get { return _erroremail; }
            set
            {
                _erroremail = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate { get { return _birthdate; } set { _birthdate = value; OnPropertyChanged(); } }

        public bool Agreed { get; set; }
        #endregion


       



        public string Error
        {           
            get
            {
                string errorValue = null;

                foreach (var m in typeof(RegistrationViewModel).GetProperties())
                {
                    if (m.PropertyType == typeof(string)|| m.PropertyType == typeof(bool))
                    {
                        if (this[m.Name] == "Required value") { errorValue = "Required value"; }
                    }

                }
                return errorValue;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "FirstName")
                {
                    return string.IsNullOrEmpty(this.FirstName) ? "Required value" : null;
                }
                if (columnName == "LastName")
                {
                    return string.IsNullOrEmpty(this.LastName) ? "Required value" : null;
                }
                if (columnName == "Program")
                {
                    return string.IsNullOrEmpty(Program.ToString()) ? "Required value" : null;
                }
                if (columnName == "CurrentSemester")
                {
                    return string.IsNullOrEmpty(CurrentSemester.ToString()) ? "Required value" : null;
                }
                if (columnName == "CurrentLevel")
                {
                    return string.IsNullOrEmpty(CurrentLevel.ToString())? "Required value" : null;
                }
                if (columnName == "Email")
                {                 
                    return string.IsNullOrEmpty(Email) ? "Required value" : null;
                }
                if (columnName == "Agreed")
                {
                    return Agreed ? null : "Required value";
                }

                return null;
            }

        }
    }
}
