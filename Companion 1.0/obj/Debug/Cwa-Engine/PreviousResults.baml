   M S B A M L   `   `   `  ����H  DCompanion 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Companion_1._0.Cwa_EngineCompanion_1._0.Cwa_Engine  5      /    )Companion_1._0.Cwa_Engine.PreviousResults   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:Companion_1._0.Cwa_Engine     5      N JFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  5   
   -   5        .��5      �� .��5      �   � RowDefinitions  5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��
LevelLabel��5      $"��Enter Your record for Level ��6&      Heading2#��[  6J   $��10q�6l   �� 5   
   $��1��6   $��1��6#   .+�5      6  0FirstFloor.ModernUI.Windows.Controls.BBCodeBlock     BBCode$ [b]Semeter One��6   &   System.Windows.Controls.DataGrid  5      -   5        $	��DG1��5         AutoGenerateColumns .  $��
10,5,10,10q�6V   ��6"   ��    Columns 5      =  7FirstFloor.ModernUI.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Course Code��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *
CourseCode+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�  5      $ Course Name��6-    6B   �� *
CourseName+$ PropertyChanged=�$ TwoWay=�  5      $ Credit hour��6-    6B   �� *
CreditHour+$ PropertyChanged=�$ TwoWay=�  5       $
 Mark��6-    6;   �� *Mark+$ PropertyChanged=�$ TwoWay=�5"        5$      -   5        $��SemesterOneTrailLabel��5$      $ [b]Semeter One Trails��6;   $��0,0,0,5q�6Z     5%      -   5        $
��TDG1��5%       .  $��
10,5,10,10q�6�   ��6#   �� 	 � DataContext	 6;   �� *SemesterOneTrails+ 5&        5'      $ Course Code��6-    6C   �� *
CourseCode+$ PropertyChanged=�$ TwoWay=�  5(      $ Course Name��6-    6B   �� *
CourseName+$ PropertyChanged=�$ TwoWay=�  5)      $ Credit hour��6-    6B   �� *
CreditHour+$ PropertyChanged=�$ TwoWay=�  5*      $
 Mark��6-    6;   �� *Mark+$ PropertyChanged=�$ TwoWay=�5,        5/      -   5        $��SemesterTwoLabel��5/      $ [b]Semeter Two��66   $��0,0,0,5q�6N     50      -   5        $	��DG2��50       .  $��
10,0,10,10q�6V   ��6"   ��  51        52      $ Course Code��6-    6C   �� *
CourseCode+$ PropertyChanged=�$ TwoWay=�  53      $ Course Name��6-    6B   �� *
CourseName+$ PropertyChanged=�$ TwoWay=�  54      $ Credit hour��6-    6B   �� *
CreditHour+$ PropertyChanged=�$ TwoWay=�  55      $
 Mark��6-    6;   �� *Mark+$ PropertyChanged=�$ TwoWay=�57        58      -   5        $��SemesterTwoTrailLabel��58      $ [b]Semeter Two Trails��6;   $��0,0,0,5q�6Z     59      -	   5        $
��TDG2��59       .  $��
10,0,10,10q�6�   ��6#   �� 	 6;   �� *SemesterTwoTrails+ 5:        5;      $ Course Code��6-    6C   �� *
CourseCode+$ PropertyChanged=�$ TwoWay=�  5<      $ Course Name��6-    6B   �� *
CourseName+$ PropertyChanged=�$ TwoWay=�  5=      $ Credit hour��6-    6B   �� *
CreditHour+$ PropertyChanged=�$ TwoWay=�  5>      $
 Mark��6-    6;   �� *Mark+$ PropertyChanged=�$ TwoWay=�5@      �� 5B      -
   5        $��SubmitButton��5B      $
��Next��6)   $	��100��68   $��10q�6D   $��Right=�6Q   ��5C      �� *SubmitButtonCommand+5D      5E      5F      5G      