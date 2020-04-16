using IOops;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ViewModels.BaseClasses.CwaEngine;
using ViewModels.ViewModels;

namespace Companion_1._0.Cwa_Engine
{
    /// <summary>
    /// Interaction logic for CwaAnalyst.xaml
    /// </summary>
    public partial class CwaAnalyst : UserControl
    {
        #region PrivateVariables

        int CollectionSize;
        CwaCalculationViewModel viewModel;
        ObservableCollection<Courses> FullList = new ObservableCollection<Courses>();
        List<Slider> slList = new List<Slider>();
        List<CheckBox> chkList = new List<CheckBox>();
        List<double> OldSliderValues = new List<double>();
        List<double> OldMarks = new List<double>();
        bool stuck;
        int j;

        #endregion

        public CwaAnalyst()
        {
            InitializeComponent();          
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(DreamCwaTextBox.Text))
            {
                CourseStackPanel.IsEnabled = false;
            }
            else
                CourseStackPanel.IsEnabled = true;


            DG.DataContext = null;
            DG.DataContext = viewModel.AllCoursList.ElementAt(0);
            TDG.DataContext = null;
            TDG.DataContext = viewModel.TrailList;
            FullList.Clear();
            foreach (var item in slList)
            {
                item.Value = 100;
            }
             
            foreach(var item in viewModel.AllCoursList.ElementAt(0))
            {
                double mark;
                double.TryParse(item.Mark, out mark);
                OldMarks.Add(mark);
                FullList.Add(item);
            }
            foreach (var item in viewModel.TrailList)
            {
                double mark;
                double.TryParse(item.Mark, out mark);
                OldMarks.Add(mark);
                FullList.Add(item);
            }

            lblErrorMessage.Content = viewModel.CwaErrorMessage;
        }

        void SliderChanged()
        {
            stuck = false;

            double semesterAverage = viewModel.SemesterAverage;

            for(int i =0;i< FullList.Count;i++)
            {
                double slideChange =0;
                double CourseMark = 0;
                double MarkToDeduct = 0;
                double MarkForEach = 0;

                if(slList[i].Value!= OldSliderValues[i])
                {
                    slideChange = (OldSliderValues[i]) - (slList[i].Value);
                    double.TryParse(FullList[i].Mark, out CourseMark);
                    MarkToDeduct = ((slideChange / 100) * semesterAverage);
                    MarkForEach = MarkToDeduct / (CollectionSize - 1);
                    bool hasUnfitMark = false;

                    #region checkForUnfitMark

                    for (int j = 0; j < slList.Count; j++)
                    {
                        double mark = 0;
                        double.TryParse(FullList[j].Mark, out mark);

                        double assignmentMark;
                        if (j != i)
                        {
                            assignmentMark = (mark + MarkForEach);
                        }
                        else
                            assignmentMark = (mark - MarkToDeduct);

                        if ((Math.Round(assignmentMark, 2) > 100 || Math.Round(assignmentMark, 2) < 0))
                        {
                            hasUnfitMark = true;
                            break;
                        }
                    }

                    #endregion

                    #region determineMarks
                    for (int j =0;j< FullList.Count;j++)
                    {
                        if((slList[j]).IsEnabled == true && AreAllSlidersAlive())
                        {
                            double mark = 0;

                            double.TryParse(FullList[j].Mark, out mark);


                            double assignmentMark;

                            if (j != i)
                            {
                                assignmentMark = (mark + MarkForEach);
                            }
                            else
                                assignmentMark = (mark - MarkToDeduct);


                            if (!hasUnfitMark)
                            {
                                if (j != i)
                                {
                                    FullList[j].Mark = assignmentMark.ToString("F1");
                                }
                                else
                                {
                                    FullList[j].Mark = assignmentMark.ToString("F1");
                                }
                            }
                            else
                            {
                                stuck = true;
                                slList[i].Value = OldSliderValues[i];
                                return;
                                //for (int k = 0; k < OldMarks.Count; k++)
                                //{
                                //    if (viewModel.AllCoursList.ElementAt(0).Count > k)
                                //        viewModel.AllCoursList.ElementAt(0)[i].Mark = OldMarks[k].ToString();
                                //    else
                                //        viewModel.TrailList[k - viewModel.AllCoursList.ElementAt(0).Count].Mark = OldMarks[k].ToString();
                                //}
                            }
                        }
                        else
                        {
                            slList[j].Value = OldSliderValues[j];
                        }

                    }
                    #endregion

                }
            }

            #region AccumulateOldValues
            if (stuck == false)
            {
                OldSliderValues.Clear();
                foreach (var slider in slList)
                {
                    OldSliderValues.Add(slider.Value);
                }
                OldMarks.Clear();
                foreach (var item in viewModel.AllCoursList.ElementAt(0))
                {
                    double mark;
                    double.TryParse(item.Mark, out mark);
                    OldMarks.Add(mark);
                }
                foreach (var item in viewModel.TrailList)
                {
                    double mark;
                    double.TryParse(item.Mark, out mark);
                    OldMarks.Add(mark);
                }
            }
            #endregion

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

                SliderChanged();

            DG.DataContext = null;
            DG.DataContext = viewModel.AllCoursList.ElementAt(0);
            TDG.DataContext = null;
            TDG.DataContext = viewModel.TrailList;
        }

        private void FlatCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FlatCanvas.Visibility = Visibility.Hidden;
            ErectCanvas.Visibility = Visibility.Visible;
            MainStack.IsEnabled = false;
        }

        private void ErectCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ErectCanvas.Visibility = Visibility.Hidden;
            FlatCanvas.Visibility = Visibility.Visible;
            MainStack.IsEnabled = true;
        }

        private void CalculateCwaButton_Click(object sender, RoutedEventArgs e)
        {
            CwaLabel.Visibility = Visibility.Visible;
            CwaLabel.Text = viewModel.CWA;
        }

        //private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    CwaLabel.Visibility = Visibility.Hidden;
        //}

        private void PreviousRecordButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.ContentSource = new Uri("/Cwa-Engine/EngineWizardStartPage.xaml", UriKind.Relative);
            data.EditPreviousRecord(viewModel.CurrentStudent);                    
        }

        private void CwaValue_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(ErectCanvas.Visibility == Visibility.Visible)
            {
                ErectCanvas.Visibility = Visibility.Hidden;
                FlatCanvas.Visibility = Visibility.Visible;
                MainStack.IsEnabled = true;
            }
     
        }

        private void Initialize()
        {
            //ControlsListBox.SelectedIndex = 0;
            slList.Clear();
            chkList.Clear();
            viewModel = new CwaCalculationViewModel();
            DataContext = viewModel;
            DG.DataContext = viewModel.AllCoursList.ElementAt(0);
            CollectionSize = viewModel.AllCoursList.ElementAt(0).Count + viewModel.TrailList.Count;

            if (viewModel.TrailList.Count == 0)
            {
                SemesterTrailLabel.Visibility = Visibility.Hidden;
                TDG.Visibility = Visibility.Hidden;
            }

            for (int i = 0; i < CollectionSize; i++)
            {
                if (i == 0)
                    j = 40;
                else if (i == viewModel.AllCoursList.ElementAt(0).Count)
                    j = 80;
                else
                    j = 13;
                WrapPanel panel = new WrapPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(5, j, 5, 5) };

                Slider slider = new Slider() { Maximum = 100, SmallChange = 1, Width = 200, Value = 100 };
                slider.ValueChanged += Slider_ValueChanged;
                
                CheckBox chkbox = new CheckBox() { Width = 40, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center,Margin = new Thickness(5,0,0,0)};
                chkbox.Click += CheckBox_Click;
                slList.Add(slider);
                chkList.Add(chkbox);
               // Binding binding = new Binding() { Source = slider, Path = new PropertyPath("Value"), Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };

               // block.SetBinding(TextBlock.TextProperty, binding);
                panel.Children.Add(slider);
                panel.Children.Add(chkbox);
                SliderStackPanel.Children.Add(panel);
            }
            foreach (var slider in slList)
            {
                OldSliderValues.Add(slider.Value);
            }
            viewModel.CalculateButtonExecute("");
        }
        private void userControl_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }



        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            int index = chkList.IndexOf(box);
            slList[index].IsEnabled = (bool)box.IsChecked ? false : true;
        }    

        private bool AreAllSlidersAlive()
        {
            int i = 0;
            foreach(var item in slList)
            {
                if(item.IsEnabled == true)
                {
                    i++;
                }
            }
            if (i > 1) return true;
            else return false;
        }
    } 


}
