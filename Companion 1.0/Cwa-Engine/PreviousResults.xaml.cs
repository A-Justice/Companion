using IOops;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels.ViewModels;

namespace Companion_1._0.Cwa_Engine
{
    /// <summary>
    /// Interaction logic for PreviousResults.xaml
    /// </summary>
    public partial class PreviousResults : UserControl
    {
        PreviousResultsViewModel viewModel;
        int labelCounter = 100;
        Stack<int> CollectionIndices;
        Student CurrentStudent;
        int StackSize;
  
        public PreviousResults()
        {
            InitializeComponent();
            if (RegistrationViewModel.CurrentStudent != null)
            {
                CurrentStudent = RegistrationViewModel.CurrentStudent;
                new LoginViewModel();
                LoginViewModel.CurrentStudent = CurrentStudent;
            }
            else
                CurrentStudent = LoginViewModel.CurrentStudent;

            
            MainWindow.mainWindow.BackButtonIsEnabled = true;
            viewModel = new PreviousResultsViewModel();
            DataContext = viewModel;
            CollectionIndices = new Stack<int>(viewModel.AllCourseList.Count());
            viewModel.EditingCompleted = false;
            FillStack();
            StackSize = CollectionIndices.Count;
            LevelLabel.Text = Lth(LevelLabel.Text);     // Generate the required level name to be displayed on the screen
            SemesterOneTrailLabel.Visibility = Visibility.Hidden;
            SemesterTwoTrailLabel.Visibility = Visibility.Hidden;
            TDG1.Visibility = Visibility.Hidden;
            TDG2.Visibility = Visibility.Hidden;
            if (CollectionIndices.Count >= 2)
            {

                DG1.DataContext = viewModel.AllCourseList.ElementAt(CollectionIndices.Pop());
                DG2.DataContext = viewModel.AllCourseList.ElementAt(CollectionIndices.Pop());

                if (CollectionIndices.Count <= 2)
                    SubmitButton.Content = "Submit";
            }
            else
            {
                if (CollectionIndices.Count == 1)
                {
                    DG2.Visibility = Visibility.Hidden;
                    SemesterTwoLabel.Visibility = Visibility.Hidden;
                    DG1.DataContext = viewModel.AllCourseList.ElementAt(CollectionIndices.Pop());
                    DG2.DataContext = null;
                    if (CollectionIndices.Count <= 2)
                        SubmitButton.Content = "Submit";
                }
                else {
                    DG1.DataContext = null;
                    DG2.DataContext = null;
                }
            }

            viewModel.ValidateGrid(DG1.DataContext, "DG1");
            viewModel.ValidateGrid(DG2.DataContext, "DG2");
            //viewModel.SaveTrailRecord();
        }
        


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            SemesterOneTrailLabel.Visibility = Visibility.Visible;
            SemesterTwoTrailLabel.Visibility = Visibility.Visible;
            TDG1.Visibility = Visibility.Visible;
            TDG2.Visibility = Visibility.Visible;
            viewModel.ValidateGrid(DG1.DataContext, "DG1");
            viewModel.ValidateGrid(DG2.DataContext, "DG2");
            //viewModel.ValidateGrid(TDG1.DataContext, "TDG1");
            //viewModel.ValidateGrid(TDG2.DataContext, "TDG2");
            viewModel.SaveTrailRecord();

            if (CollectionIndices.Count <= 2)
                SubmitButton.Content = "Submit";

            if (SubmitButton.Content.ToString() == "Submit")
            {
                if(CollectionIndices.Count ==0)
                {
                    viewModel.EditingCompleted = true;

                    if(!File.Exists(CurrentStudent.PersonalDirectory + @"/PreviousRecords.xml"))
                    File.Create(CurrentStudent.PersonalDirectory + @"/PreviousRecords.xml").Dispose();
                   // MainWindow.mainWindow.Content = new CwaAnalyst();
                    MainWindow.mainWindow.ContentSource = new Uri("/Cwa-Engine/CwaAnalyst.xaml", UriKind.Relative);
                    return;
                }               
                else
                {
                    int i = 0;
                    if (viewModel.SemesterTrails.Count > i)
                        TDG1.DataContext = viewModel.SemesterTrails[i];
                    else
                        TDG1.DataContext = null;

                    int j = 1;
                    if (viewModel.SemesterTrails.Count > j)
                        TDG2.DataContext = viewModel.SemesterTrails[j];
                    else
                        TDG2.DataContext = null;

                    LevelLabel.Text = Lth(LevelLabel.Text);
                    
                    if (CollectionIndices.Count == 1)
                    {
                        DG2.Visibility = Visibility.Hidden;
                        SemesterTwoLabel.Visibility = Visibility.Hidden;
                        SemesterTwoTrailLabel.Visibility = Visibility.Hidden;
                        TDG2.Visibility = Visibility.Hidden;
                        DG1.DataContext = viewModel.AllCourseList.ElementAt(CollectionIndices.Pop());
                    }
                    if (CollectionIndices.Count == 2)
                    {
                        DG1.DataContext = viewModel.AllCourseList.ElementAt(CollectionIndices.Pop());
                        DG2.DataContext = viewModel.AllCourseList.ElementAt(CollectionIndices.Pop());
                    }
                }
             
            }
            else if (CollectionIndices.Count > 2)
            {
                int i = 0;
                if (viewModel.SemesterTrails.Count > i)
                    TDG1.DataContext = viewModel.SemesterTrails[i];
                else
                    TDG1.DataContext = null;

                int j = 1;
                if (viewModel.SemesterTrails.Count > j)
                    TDG2.DataContext = viewModel.SemesterTrails[j];
                else
                    TDG2.DataContext = null;

                LevelLabel.Text = Lth(LevelLabel.Text);                
                DG1.DataContext = viewModel.AllCourseList.ElementAt(CollectionIndices.Pop());
                DG2.DataContext = viewModel.AllCourseList.ElementAt(CollectionIndices.Pop());             
            }

           viewModel.ValidateGrid(DG1.DataContext, "DG1");
          viewModel.ValidateGrid(DG2.DataContext, "DG2");
           viewModel.ValidateGrid(TDG1.DataContext, "TDG1");
           viewModel.ValidateGrid(TDG2.DataContext, "TDG2");
            viewModel.SaveTrailRecord();
        }

       
        string Lth(string text)
        {
             
            string finalString = null;
          

            
           

            List<string> mText = text.Split(' ').ToList();

            if (mText.Count > 4)
                mText.RemoveAt(5);

            mText.Add(labelCounter.ToString());

            for (int i = 0; i < mText.Count; i++)
            {
                if (finalString == null)
                    finalString = mText[i];
                else
                finalString = finalString + " " + mText[i];
            }

            if (labelCounter <= Convert.ToInt32(((int)Math.Round(((Convert.ToDouble(StackSize) / 2) + 0.1))).ToString()+"00")) ; 
            labelCounter += 100;

            return finalString;
        }

        void FillStack()
        {
            for (int i = 0; i < viewModel.AllCourseList.Count(); i++)
            {
                if(i!=0)
                CollectionIndices.Push(i);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.BackButtonIsEnabled = false;        
        }

        private void DG1_KeyUp(object sender, KeyEventArgs e)
        {
          DG1.DataContext =  viewModel.ValidateGrid(DG1.DataContext,"DG1");
        }

        private void TDG1_KeyUp(object sender, KeyEventArgs e)
        {
            //TDG1.DataContext = viewModel.ValidateGrid(TDG1.DataContext, "TDG1");
        }

        private void DG2_KeyUp(object sender, KeyEventArgs e)
        {
            DG2.DataContext = viewModel.ValidateGrid(DG2.DataContext, "DG2");
        }

        private void TDG2_KeyUp(object sender, KeyEventArgs e)
        {
           // TDG2.DataContext = viewModel.ValidateGrid(TDG2.DataContext, "TDG1");
        }
    }
}
