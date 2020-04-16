using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOops;
using ViewModels.BaseClasses.CwaEngine;
using System.IO;
//using FirstFloor.ModernUI.Presentation;
using ViewModels.BaseClasses;
using System.Threading;

namespace ViewModels.ViewModels
{
  public  class CwaCalculationViewModel:ObservableObjects
    {
        #region PrivateVariables
        private Dictionary<string, List<Tuple<string, string, int, double?>>> RecordCollection;
        private ObservableCollection<Courses> _courses;
        private List<ObservableCollection<Courses>> allCourseList;
        public Student CurrentStudent;
        private double cwaValue;
        private string cwa;
        private ObservableCollection<Courses> trailList;
        private double dreamCwa;
        public  double SemesterAverage;
        public string cwaErrorMessage;
        private string highestCwa;
        private string lowestCwa;
        SynchronizationContext currentContext;
        #endregion

        #region PublicProperties
        public string CWA
        {
            get
            {
                return "Your Current CWA is " + cwa;
            }
            set
            {
                cwa = value;
            }
        }

        public RelayCommand CalculatCwaCommand
        {
            get; set;
        }

        public List<ObservableCollection<Courses>> AllCoursList
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

        public ObservableCollection<Courses> TrailList
        {
            get
            {
               return trailList;
            }
            set
            {
                trailList = value;
            }

        }

        public double DreamCwa
        {
            get
            {
                return dreamCwa;
            }
            set
            {
                dreamCwa = value;
                ShareSAEvenly();
            }
        }

        public string CwaErrorMessage
        {
            get {
                return cwaErrorMessage;
            }

            set
            {
                cwaErrorMessage = value;
            }
        }

        public string HighestCwa
        {
            get
            {
                return highestCwa;
            }
            set
            {
                highestCwa = value;
            }
        }

        public string LowestCwa
        {
            get
            {
                return lowestCwa;
            }
            set
            {
                lowestCwa = value;
            }
        }

        #endregion


        #region constuctor
        public CwaCalculationViewModel()
        {
            RecordCollection = new Dictionary<string, List<Tuple<string, string, int, double?>>>();
            allCourseList = new List<ObservableCollection<Courses>>();
            _courses = new ObservableCollection<Courses>();
            CalculatCwaCommand = new RelayCommand(CalculateButtonExecute);
            
            LoadData();
            GetData();
            TrailList = GetTrails(allCourseList);
            GetMinAndMaxCwa();
            currentContext = SynchronizationContext.Current;
        }



        #endregion


        #region Commands

        public void CalculateButtonExecute(object obj)
        {
            LoadData();
            GetData();
            Tuple<int, double> values = CalculateValues();
            cwaValue = values.Item2 / values.Item1;
            cwa = cwaValue.ToString("F2");
        }

        #endregion


        #region GetAndLoadData
        private void LoadData()
        {
            CurrentStudent = LoginViewModel.CurrentStudent;
            RecordCollection = data.LoadDataFromFile(CurrentStudent);
        }

        /// <summary>
        /// Expand the loaded data into a list of observable collections to be used in a dataGrid 
        /// </summary>
        private void GetData()
        {
            allCourseList.Clear();
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

        #endregion


        #region CrudOperations

        #region TrailData
        ObservableCollection<Courses> GetTrails(List<ObservableCollection<Courses>> courseList)
        {
            List<int> trailIndexes = new List<int>();
            List<ObservableCollection<Courses>> TrailCollection = new List<ObservableCollection<Courses>>();
            ObservableCollection<Courses> FinalTrailCollection = new ObservableCollection<Courses>();
            for (int i=2;i<courseList.Count;i=i+2)
            {
                string trailPath = GetTrailFilePath(courseList[i]);
                List<Tuple<string, string, int, double?>> TrailData = data.LoadTrailsFromFile(trailPath);
                TrailCollection.Add(GetObservableCollection(TrailData));
            }
            foreach(var list in TrailCollection)
            {
                foreach(var course in list)
                {
                    double courseMark = 0;
                    double.TryParse(course.Mark,out courseMark);
                    if(courseMark<50)
                    FinalTrailCollection.Add(course);
                }
            }

            return FinalTrailCollection;
        }

        string GetTrailFilePath(ObservableCollection<Courses> CourseList)
        {
           
            string path = null;
            foreach (var word in RecordCollection.Keys)
            {
                foreach (var tuple1 in RecordCollection[word])
                {
                    foreach (var tuple2 in CourseList)
                    {
                        if (tuple1.Item1 == tuple2.CourseCode)
                        {
                            if (path == null)
                            {
                                path = CreateTrailFileName(word);
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

        public static string CreateTrailFileName(string semeterfileDirectory)
        {

            string[] splittedDirectory = semeterfileDirectory.Split('.');
            int tracker = splittedDirectory.Count() - 1;
            semeterfileDirectory = null;

            for (int i = 0; i <= tracker; i++)
            {
                if (i != tracker)
                {
                    if (semeterfileDirectory == null)
                    {
                        semeterfileDirectory = splittedDirectory[i];
                    }
                    else
                        semeterfileDirectory = semeterfileDirectory + "." + splittedDirectory[i];
                }
                else
                {
                    semeterfileDirectory = semeterfileDirectory + "T." + splittedDirectory[i];
                }
            }
            //semeterfileDirectory = splittedDirectory[0]+splittedDirectory[1] + "T.txt";
            DirectoryManager.CreateFile(semeterfileDirectory);
            return semeterfileDirectory;
        }

        public static ObservableCollection<Courses> GetObservableCollection(List<Tuple<string, string, int, double?>> tuple)
        {
            ObservableCollection<Courses> collection = new ObservableCollection<Courses>();

            if (tuple != null)
            {
                foreach (var item in tuple)
                {

                    collection.Add(new Courses()
                    {
                        CourseCode = item.Item1,
                        CourseName = item.Item2,
                        CreditHour = item.Item3.ToString(),
                        Mark = item.Item4.ToString()
                    });
                }
            }
            return collection;
        }


        #endregion

        #region Prediction

        private Tuple<int, double> CalculateValues()
        {
            string trailPath = null;
            List<double> CrMaProducts = new List<double>();
            List<int> CrMaSum = new List<int>();
            List<Tuple<string, string, int, double?>> trailFile = new List<Tuple<string, string, int, double?>>();
            double WeightedMark = 0;
            int TotalCredits = 0;

            foreach (var item in RecordCollection.Keys)
            {
                trailPath = data.CreateTrailFileName(item);
                if (File.Exists(trailPath))
                {
                    if (item != RecordCollection.ElementAt(0).Key)
                    {
                        trailFile = data.LoadTrailsFromFile(trailPath);
                    }
                }
                foreach (var stuff in RecordCollection[item])
                {
                    if (item != RecordCollection.ElementAt(0).Key)
                    {
                        CrMaSum.Add(Convert.ToInt32(stuff.Item3));
                        CrMaProducts.Add(Convert.ToDouble(stuff.Item3 * stuff.Item4));
                    }
                }
                foreach (var stuff in trailFile)
                {
                    CrMaSum.Add(Convert.ToInt32(stuff.Item3));
                    CrMaProducts.Add(Convert.ToDouble(stuff.Item3 * stuff.Item4));
                }
            }
            foreach (var stuff in CrMaProducts)
            {
                WeightedMark = WeightedMark + stuff;
            }
            foreach (var stuff in CrMaSum)
            {
                TotalCredits = TotalCredits + stuff;
            }
            return new Tuple<int, double>(TotalCredits, WeightedMark);
        }

        public int GetCurrentSemesterTotalCredits()
        {
            int totalCredits = 0;
            foreach (var item in RecordCollection.ElementAt(0).Value)
            {
                totalCredits += item.Item3;
            }
            foreach (var item in trailList)
            {
                int tempCredit = 0;
                int.TryParse(item.CreditHour, out tempCredit);
                totalCredits += tempCredit;
            }
            return totalCredits;
        }

        public double GetSemesterAverage(double dreamCwa)
        {
            int PTC, CTC;
            double PTRS, SA;

            Tuple<int, double> values = CalculateValues();
            PTC = values.Item1;
            PTRS = values.Item2;
            CTC = GetCurrentSemesterTotalCredits();

            SA = ((dreamCwa * (PTC + CTC)) - PTRS) / CTC;
            SemesterAverage = SA;
            return SA;
        }

        public double MinAndMaxCwa(double _SA)
        {
            int PTC, CTC;
            double PTRS;

            Tuple<int, double> values = CalculateValues();
            PTC = values.Item1;
            PTRS = values.Item2;
            CTC = GetCurrentSemesterTotalCredits();

            dreamCwa = ((_SA * CTC) + PTRS) / (PTC + CTC);
            
            return dreamCwa;
        }

        public void ShareSAEvenly()
        {
            CwaErrorMessage = null;

            int totalCourses = allCourseList.ElementAt(0).Count + TrailList.Count;
            double singlePiece = GetSemesterAverage(dreamCwa);
            try
            {
                if (singlePiece > 0 && singlePiece <= 100)
                {
                    foreach (var item in AllCoursList.ElementAt(0))
                    {
                        item.Mark = singlePiece.ToString("F2");
                    }
                    foreach (var item in trailList)
                    {
                        item.Mark = singlePiece.ToString("F2");
                    }
                }
                else
                {
                    throw new InvalidOperationException("The value you've entered cannot be attained");
                }
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    CwaErrorMessage = ex.Message;

                }
             }
        }

        public void GetMinAndMaxCwa()
        {
            LowestCwa = MinAndMaxCwa(0).ToString("F2");

            HighestCwa = MinAndMaxCwa(100).ToString("F2");                              
        }

        #endregion

        #endregion
    }
}
