using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOops;
using ViewModels.BaseClasses;
using System.Collections.ObjectModel;
using ViewModels.BaseClasses.CwaEngine;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;

namespace ViewModels.ViewModels
{
    public  class PreviousResultsViewModel:ObservableObjects
    {
        #region privateVariables
        private Student _currentStudent;
        private Dictionary<string, List<Tuple<string, string, int, double?>>> RecordCollection;
        private ObservableCollection<Courses> _courses;
        private List<ObservableCollection<Courses>> allCourseList;
        private ObservableCollection<Courses> semesterOneTrails;
        private ObservableCollection<Courses> semesterTwoTrails;
        public List<ObservableCollection<Courses>> semesterTrails;
        #endregion

        #region Public Properties

        public List<ObservableCollection<Courses>> AllCourseList
        {
            get
            {
                return allCourseList;
            }
            set
            {
                allCourseList = value;
            }
        }
        
        public List<ObservableCollection<Courses>> SemesterTrails
        {
            get
            {
                return semesterTrails;
            }
            set
            {
                semesterTrails = value;
            }
        }
  
        public RelayCommand SubmitButtonCommand { get; set; }

        Student CurrentStudent
        {
            get
            {
                return _currentStudent;

            }

            set
            {
                _currentStudent = value;
            }
        }

        bool Validated { get; set; }
         public bool EditingCompleted
        {
            get; set;
        }

        #endregion

        #region constructor
        public PreviousResultsViewModel()
        {
            RecordCollection = new Dictionary<string, List<Tuple<string, string, int, double?>>>();
            SubmitButtonCommand = new RelayCommand(SubmitButtonExecute, SubmitButtonCanExecute);
            _courses = new ObservableCollection<Courses>();
            allCourseList = new List<ObservableCollection<Courses>>();
            semesterOneTrails = new ObservableCollection<Courses>();
            semesterTwoTrails = new ObservableCollection<Courses>();
            semesterTrails = new List<ObservableCollection<Courses>>();
            LoadData();
            GetData();
            
        }

        #endregion

        #region DataManipulation
        /// <summary>
        /// Load Data about the Identity and the records of the current student
        /// </summary>
        private void LoadData()
        {
            CurrentStudent = LoginViewModel.CurrentStudent;
            //CurrentStudent.Promote(Level.L300, Semester.S1);
            RecordCollection = data.LoadDataFromFile(CurrentStudent);
        }

        /// <summary>
        /// Expand the loaded data into a list of observable collections to be used in a dataGrid 
        /// </summary>
        private void GetData()
        {
            foreach (var stuffs in RecordCollection.Keys)
            {
                var item = RecordCollection[stuffs];
                _courses.Clear();
                foreach (var stuff in item)
                {
                    _courses.Add(new Courses()
                    {
                        CourseCode = stuff.Item1,
                        CourseName = stuff.Item2,
                        CreditHour = stuff.Item3.ToString(),
                        Mark = stuff.Item4.ToString()
                    });
                }
                allCourseList.Add(new ObservableCollection<Courses>(_courses));                         
            }
        }

        /// <summary>
        /// After interaction with the ui Save the back into the database
        /// </summary>
        private void SaveData()
        {
            //try {               
                int i = 0;
                List<Tuple<string, string, int, double?>> IndividualCourseList = new List<Tuple<string, string, int, double?>>();
               Dictionary<string, List<Tuple<string, string, int, double?>>> TempRecordCollection = new Dictionary<string, List<Tuple<string, string, int, double?>>>(RecordCollection);
          

            //foreach(var thing in RecordCollection.Keys)
            //{
            //    TempRecordCollection.Add(thing,RecordCollection[thing]);
            //}

            foreach (var item in RecordCollection.Keys)
                {
                    ObservableCollection<Courses> IndividualCourse = AllCourseList.ElementAt(i);
                    foreach (var stuff in IndividualCourse)
                    {
                    int Credithour = 0;
                    double Mark = 0;

                    int.TryParse(stuff.CreditHour,out Credithour);
                    double.TryParse(stuff.Mark, out Mark);

                        IndividualCourseList.Add(new Tuple<string, string, int, double?>(stuff.CourseCode, stuff.CourseName,Credithour , Mark));
                    }

                TempRecordCollection[item].Clear();
                foreach (var thing in IndividualCourseList)
                {
                    TempRecordCollection[item].Add(thing);
                }

                    
                    IndividualCourse.Clear();
                    IndividualCourseList.Clear();
                    i++;
                }

                data.SaveDataToFile(TempRecordCollection);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }



        #endregion

        #region Commands
        private void SubmitButtonExecute(object obj)
        {
            if(EditingCompleted)
            SaveData();
        }

        private bool SubmitButtonCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region CrudOperations
        /// <summary>
        /// Checks if all values placed inside the data Grid are potent and 
        /// decided per the mark entered if its a trail or not
        /// </summary>
        /// <param name="CourseCollection">The data from the Grid</param>
        /// <param name="dgName">The name of the Grid i.e that of semester one or semester two</param>
        /// <returns>return back the Data context of the grid to it</returns>
        public ObservableCollection<Courses> ValidateGrid(object CourseCollection,string dgName)
        {
            ObservableCollection<Courses> TempCourse;
            if (CourseCollection != null)
                TempCourse = (ObservableCollection<Courses>)CourseCollection;
            else
                TempCourse = new ObservableCollection<Courses>();
            try
            {
                foreach (var item in TempCourse)
                {
                    if (!string.IsNullOrEmpty(item.CourseCode) || !string.IsNullOrEmpty(item.CourseName))
                    {
                        string ptMark = item.Mark.ToString();
                        if (ptMark.Contains("."))
                            ptMark += "0";
                        double mark = string.IsNullOrEmpty(ptMark) ? 0 : Convert.ToDouble(ptMark);
                        int credithour = string.IsNullOrEmpty(item.CreditHour) ? 0 : Convert.ToInt32(item.CreditHour);


                        if (mark > 100 && !String.IsNullOrEmpty(item.Mark))
                        {
                            Validated = false;
                            throw new InvalidOperationException("A mark cannot be greater than 100");
                        }

                        if ((credithour > 3 || credithour < 1) && !String.IsNullOrEmpty(item.CreditHour))
                        {
                            Validated = false;
                            throw new InvalidOperationException("The credit hour can neither be less than 1 nor greater than 3");
                        }
                        if ((credithour <= 3 || credithour >= 1))
                        {
                            if(dgName == "DG1")
                            {
                                TrailDetection(item, semesterOneTrails);
                            }
                             if(dgName == "DG2")
                            {
                                TrailDetection(item, semesterTwoTrails);
                            }
                             if (dgName == "TDG1")
                            {
                                RemoveTrailRecord(SemesterTrails[0]);
                                return SemesterTrails[0];
                            }
                            if (dgName == "TDG2")
                            {
                                RemoveTrailRecord(SemesterTrails[1]);
                                return SemesterTrails[1];
                            }
                        }
                      
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.CreditHour) || !string.IsNullOrEmpty(item.Mark))
                        {
                            Validated = false;
                        throw new InvalidOperationException("\"Wrong Precision Order\" Please enter the course code and course name before entering the credit hour and mark");
                           
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    ModernDialog.ShowMessage(ex.Message, "Invalid Input", MessageBoxButton.OK);
                }
                else
                    ModernDialog.ShowMessage("Please Enter Valid texts for the appopriate imputs", "Invalid Input", MessageBoxButton.OK);
            }
            Validated = true;
            return TempCourse;
        }

        /// <summary>
        /// Responsible to detecting the semester trails based on the particular mark of the course for
        /// that particular semester 
        /// </summary>
        /// <param name="item">Each course taken from the DataGridsCollectiion</param>
        /// <param name="_semesterTrail">data structure to hold the trails for a particular semester</param>
        private void TrailDetection(Courses item,ObservableCollection<Courses> _semesterTrail)
        {
            bool found = false;
            ObservableCollection<Courses> temptrails = new ObservableCollection<Courses>(_semesterTrail);

            foreach (var stuff in temptrails)
            {
                if (stuff.CourseCode == item.CourseCode)
                {
                    double chMark = 0;
                    double.TryParse(stuff.Mark, out chMark);
                    if (chMark < 50 && item.Mark != "")
                    {
                        if (_semesterTrail[temptrails.IndexOf(stuff)] == stuff)
                            _semesterTrail[temptrails.IndexOf(stuff)].Mark = item.Mark;
                    }
                    else
                    {
                        //      if(semesterOneTrails.Contains(stuff))
                        _semesterTrail.Remove(stuff);
                        // SemesterOneTrails = semesterOneTrails;
                    }
                    found = true;
                }
            }
            double chkMark = 0;
            double.TryParse(item.Mark, out chkMark);

            if (!found && chkMark < 50 && item.Mark != "")
                _semesterTrail.Add(new Courses(item));
        }

        private void RemoveTrailRecord(ObservableCollection<Courses> semesterTrail)
        {
            ObservableCollection<Courses> trailedCourses = new ObservableCollection<Courses>(semesterTrail);
            foreach (var course in trailedCourses)
            {
                double courseMark;
                double.TryParse(course.Mark, out courseMark);
                if (courseMark >= 50)
                {
                    semesterTrail.Remove(course);
                  //  semesterTrail.RemoveAt(trailedCourses.IndexOf(course));
                }
            }
        }

        public void SaveTrailRecord()
        {
            SemesterTrails.Clear();
           
            ObservableCollection<Courses> semOneT = new ObservableCollection<Courses>(semesterOneTrails.AsEnumerable());
            ObservableCollection<Courses> semTwoT = new ObservableCollection<Courses>(semesterTwoTrails.AsEnumerable());

            //foreach (var item in semOneT)
            //{
            //    item.Mark = null;
            //}
            //foreach (var item in semTwoT)
            //{
            //    item.Mark = null;
            //}
            semesterTrails.Add(new ObservableCollection<Courses>(semOneT));
            semesterTrails.Add(new ObservableCollection<Courses>(semTwoT));
            SaveTrailsToFile();
        }

        public void SaveTrailsToFile()
        {
            // ObservableCollection<Courses> TrailRecord = (ObservableCollection<Courses>)(RecordCollection);
            Dictionary<string, List<Tuple<string, string, int, double?>>> TrailFile = new Dictionary<string, List<Tuple<string, string, int, double?>>>();
            List<Tuple<string, string, int, double?>> TrailCourses = new List<Tuple<string, string, int, double?>>();
            string path = null;

            foreach (var item in SemesterTrails)
            {
                if (item.Count != 0)
                {
                    path = null;
                    foreach (var stuff in item)
                    {
                        double chMark = 0;
                        double.TryParse(stuff.Mark, out chMark);

                        int chCredithour = 0;
                        int.TryParse(stuff.CreditHour, out chCredithour);

                        path = GetTrailFilePath(stuff);

                        TrailCourses.Add(new Tuple<string, string, int, double?>(stuff.CourseCode, stuff.CourseName, chCredithour, chMark));
                    }
                    string Path = data.CreateTrailFileName(path);
                    if (path != RecordCollection.ElementAt(RecordCollection.Count-2).Key)
                        TrailFile.Add(Path, new List<Tuple<string, string, int, double?>>(TrailCourses));

                    TrailCourses.Clear();
                }

            }
           
            data.SaveDataToFile(new Dictionary<string, List<Tuple<string, string, int, double?>>>(TrailFile));
            TrailFile.Clear();
        }

        string GetTrailFilePath(Courses stuff)
        {
            List<Tuple<string, string, int, double?>> tupleList = null;
            //var tupleList = (from man in RecordCollection.Values
            //                 from thing in man
            //                 where thing.Item1 == stuff.CourseCode
            //                 select man.First()).ToList();

            foreach (var man in RecordCollection.Values)
            {
                foreach (var thing in man)
                {
                    if (thing.Item1 == stuff.CourseCode)
                    {
                        tupleList = new List<Tuple<string, string, int, double?>>(man);
                    }
                    else if (tupleList != null)
                    {
                        break;
                    }
                }
            }

            string path = null;
            //(from word in RecordCollection.Keys
            //    where RecordCollection[word] == tupleList
            //    select word.First()).ToString();

            foreach (var word in RecordCollection.Keys)
            {
                foreach (var tuple1 in RecordCollection[word])
                {
                    foreach (var tuple2 in tupleList)
                    {
                        if (tuple1.Item1 == tuple2.Item1)
                        {
                            if (path == null)
                            {
                                path = word;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return path;
        }



      

        #endregion
    }
}
