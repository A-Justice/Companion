using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.BaseClasses;
using ViewModels.BaseClasses.CwaEngine;
using IOops;
using System.Collections.ObjectModel;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;

namespace ViewModels.ViewModels
{
   public class Cwa_EngineViewModel : ObservableObjects, IDataErrorInfo
    {
        Student CurrentStudent;
        public Dictionary<string, List<Tuple<string, string, int, double?>>> SaDataCollection;
        public ObservableCollection<Courses> _courses;
        public Courses[] _tempcourses;
        public ObservableCollection<SAoutput> _outputs;

        #region privatevariables
        private Program _saprogram;
        private Semester _sasemester;
        private Level _salevel;
        #endregion
        public Cwa_EngineViewModel()
        {
            SubmitButtonCommand = new RelayCommand(SubmitExecute);
            _courses = new ObservableCollection<Courses>();
            _outputs = new ObservableCollection<SAoutput>();
            LoadData();
            GetData();
        }

        #region publicProperties

        public RelayCommand SubmitButtonCommand { get; set; }
        public ObservableCollection<Courses> coursess
        {
            get
            {
                return _courses;
            }

            set
            {
                _courses = value;
            }
        }

        public ObservableCollection<SAoutput> output
        {
            get
            {
                return _outputs;
            }
            set
            {
                _outputs = value;
            }
        }
        public Semester semester
        {
            get
            {
                return _sasemester;
            }

            set
            {
                _sasemester = value;
                LoadData();
                GetData();
            }
        }

        public Program program
        {
            get
            {
                return _saprogram;
            }

            set
            {
                _saprogram = value;
                LoadData();
                GetData();
            }
        }

        public Level level
        {
            get
            {
                return _salevel;
            }

            set
            {
                _salevel = value;
                LoadData();
                GetData();
            }
        }


        #endregion

        #region IDatErroIfo_Iplementation

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region public Methods
        public void LoadData()
        {
            CurrentStudent = new Student(program, level, semester);
            SaDataCollection = data.LoadDataFromFile(CurrentStudent);
        }

        public void GetData()
        {
            _courses.Clear();
              var item = SaDataCollection[CurrentStudent.SemesterFileDirectory];
            int TotalCreditHours = 0;
          
                foreach (var stuff in item)
                {
                    _courses.Add(new Courses() { CourseCode=stuff.Item1,CourseName=stuff.Item2,CreditHour=stuff.Item3.ToString(),Mark=stuff.Item4.ToString()});
                TotalCreditHours += stuff.Item3;
                    
                }
            
        }

        #endregion

        #region CommandMethods

        public void SubmitExecute(object parameter)
        {
            try
            {
                int totalcourses = 0;
                int totalcredithours = 0;
                double WeightedMark = 0;
                double semesteraverage = 0;

                foreach (var item in coursess)
                {
                    if (!string.IsNullOrEmpty(item.CourseCode) || !string.IsNullOrEmpty(item.CourseName))
                    {
                        double mark = string.IsNullOrEmpty(item.Mark) ? 0 : Convert.ToDouble(item.Mark);
                        int credithour = string.IsNullOrEmpty(item.CreditHour) ? 0 : Convert.ToInt32(item.CreditHour);

                        totalcourses += string.IsNullOrEmpty(item.CourseName) ? 0 : 1;
                        totalcredithours += credithour;

                        if (mark > 100 && ! String.IsNullOrEmpty(item.Mark))
                        {
                            throw new InvalidOperationException("A mark cannot be greater than 100");
                        }

                        if ((credithour > 3 || credithour < 1)&& !String.IsNullOrEmpty(item.CreditHour))
                        {
                            throw new InvalidOperationException("The credit hour can neither be less than 1 nor greater than 3");
                        }


                        WeightedMark += mark * credithour;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.CreditHour) || !string.IsNullOrEmpty(item.Mark))
                        {
                            ModernDialog.ShowMessage("Please enter the course code and course name before entering the credit hour and mark", "Wrong Precision Order", MessageBoxButton.OK);
                            coursess.ElementAt(coursess.IndexOf(item)).CreditHour = null;
                            coursess.ElementAt(coursess.IndexOf(item)).Mark = null;
                        }
                  
                    }
                   
                }


                semesteraverage = WeightedMark / totalcredithours;
                output.Clear();
                output.Add(new SAoutput() { TotalCourseNumber = totalcourses.ToString(), TotalCreditHours = totalcredithours.ToString(), WeightedMark = WeightedMark.ToString(), SemesterAverage = semesteraverage.ToString("F2") });

        }
            catch(Exception ex)
            {
                if(ex is InvalidOperationException)
                {
                    ModernDialog.ShowMessage(ex.Message, "Invalid Input", MessageBoxButton.OK);
                }
                else
                ModernDialog.ShowMessage("Please Enter Valid texts for the appopriate imputs", "Invalid Input",MessageBoxButton.OK);
                output.Clear();
            }
}

         public void ExecuteSubmitMethod()
        {
            SubmitExecute("");
        }
        #endregion
    }
}
