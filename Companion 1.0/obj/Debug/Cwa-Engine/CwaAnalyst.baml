   M S B A M L   `   `   `  ����H  DCompanion 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Companion_1._0.Cwa_EngineCompanion_1._0.Cwa_Engine  5      *    $Companion_1._0.Cwa_Engine.CwaAnalyst   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:Companion_1._0.Cwa_Engine     5      N JFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  5      -   5        $��userControl��5      .��5
      �� .��5      � -   5        $��MainGrid��5        � ColumnDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��140* �6   �� 5      $
��Auto �6   �� 5      $
��199* �6   .+�5   
   �� -   5        $��
FlatCanvas��5      $��29��6$   $��29��6/   $
��Left=�6X   $	��Top=�6s   .+�5      F� $��25��6    �� Left$ 2��6   
 �� Top$ 2��6/    �� Stretch$
 Fill=�6>   $��7��6M   ���	#FF000000���	#FFFFFFFF-�����>�R�p
ЎQ����mP B��LO6�   F� 5      $��25��6   $ 2��6   $ 11��6/   $
 Fill=�6?   $��7��6N   ���	#FF000000���	#FFFFFFFF6���P��>�� �r�p@��6
 �(�����pY�7�A`�p@6�   F� 5       $��25��6   $ 2��6   $ 20��6/   $
 Fill=�6?   $��7��6N   ���	#FF000000���	#FFFFFFFF-���Ѐ�>��6
 �(����pY��6�    � Triggers 5!      :� 5"       :� RoutedEvent$ Canvas.MouseUp��6   [�5#      �� 5$      .��5%      �� .��5&   "   W�  �� 
TargetName$ ControlsListBox��5'   %    �� TargetProperty$ Width�5(   %   	 W� From$	 0X�5)   %   	
 W� To$	
 206X�6.    g� Duration$ 	0:0:0:0.3D�67    g� AutoReverse .  5*   %   W� 5-   "   $ 
BlurEffect��62   $ Radius�5.   :   $	 -1X�5/   :   $
 5X�6D    .   g� RepeatBehavior$ 1x��50   :   $ 	0:0:0:0.1D�6N   51      52      54      57      �� 59   
   -   5        $��ErectCanvas��59   &   $y�Hidden=�6   $��29��69   $��29��6D   $
��Left=�6p   $	��Top=�6�   .+�5:      F� $��7��6   $��25��6   $ 2��6)   $ 2��69   $
 Fill=�6I   $��7��6X    �� StrokeLineJoin$ Round=�6l   ���	#FF000000���	#FFFFFFFF-�����>�R�p
ЎQ����mP B��LO6�   F� 5;      $��7��6   $��25��6   $ 11��6)   $ 2��6:   $
 Fill=�6I   $��7��6X   $ Round=�6l   ���	#FF000000���	#FFFFFFFF6���P��>�� �r�p@��6
 �(�����pY�7�A`�p@6�   F� 5<      $��7��6   $��25��6   $ 20��6)   $ 2��6:   $
 Fill=�6I   $��7��6X   $ Round=�6l   ���	#FF000000���	#FFFFFFFF-���Ѐ�>��6
 �(����pY��6�    5=      :� 5>      $ Canvas.MouseUp��6   [�5?      �� 5@      .��5A      �� .��5B   "   W� $ ControlsListBox��5C   %   $ Width�5D   %   $ 	0:0:0:0.3D�5E   %    .  5F   %   W� 5H   "   $ CwaLabel��62   $ RenderTransform.X�5I   :    .  $ 	0:0:0:0.5D�5K   ;   W� 5M   "   $ CwaLabel��62   $ Opacity�5N   :   $
 0X�5O   :    .  $ 	0:0:0:0.5D�5Q   ;   W� 5S   "   $ 
BlurEffect��62   $ Radius�5T   :   $	 5X�5U   :   $
 -1X�6C    .  $ 	0:0:0:0.1D�5V   ;   5W      5X      5Z      5\      �� 5^   
   -   5        $��	MainStack��5^   E   $��1��6   $��2��6$   $��5��64   .+�5`      �� $��1��6   $��2��6&    �� Orientation$ 
Horizontal=�66   .+�5a      6  0FirstFloor.ModernUI.Windows.Controls.BBCodeBlock     BBCode$ [b]Aspired CWA : ��6"   $��Center=�6=   �� 5b      -   5        $��DreamCwaTextBox��5b      $��50��6�   $��Center=�5c      ��5b   1   �� *
DreamCwa+ �� Mode$ OneWayToSource=� �� UpdateSourceTrigger$ PropertyChanged=�5c      �� 5d      -   5        $��lblErrorMessage��5d      $	��200��6�   $��10,0,0,0q�6�   ��6/   �� *CwaErrorMessage+$ TwoWay=�$ PropertyChanged=�6�   5e      �� 5g      .��5h      �� -   5        $��CourseStackPanel��5i      $ 
Horizontal=�5h      $��3��62   $��2��6?   $��	0,100,0,0q�6O   {�.  .+�5k      �� .+�5l      &   System.Windows.Controls.DataGrid  -	   5        $��DG��5l         AutoGenerateColumns .     	RowHeight$ 40Q�6Y   ��6%   ��    Columns 5m      =  7FirstFloor.ModernUI.Windows.Controls.DataGridTextColumn  5n      ,  &System.Windows.Controls.DataGridColumn   Header$ Course Code��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *
CourseCode+$ PropertyChanged=�$ TwoWay=�  5o      $ Course Name��61    6F   �� *
CourseName+$ PropertyChanged=�$ TwoWay=�  5p      $ Credit hour��61    6F   �� *
CreditHour+$ PropertyChanged=�$ TwoWay=�  5q      $
 Mark��61      
IsReadOnly .  6?   �� *Mark+$ PropertyChanged=�$ TwoWay=�6�   5s        5u      -
   5        $��SemesterTrailLabel��5u   "   $ [b]Semeter Trails��6<   $��0,0,0,5q�6W     5v      -   5        $	��TDG��5v       .  $��
10,5,10,10q�6|   $ 40Q�6�   ��6&   ��  � DataContext 6>   �� *	TrailList+ 5w        5x      $ Course Code��61    6G   �� *
CourseCode+$ PropertyChanged=�$ TwoWay=�  5y      $ Course Name��61    6F   �� *
CourseName+$ PropertyChanged=�$ TwoWay=�  5z      $ Credit hour��61    6F   �� *
CreditHour+$ PropertyChanged=�$ TwoWay=�  5{      $
 Mark��61    6?   �� *Mark+$ PropertyChanged=�$ TwoWay=�5}      5~      �� 5      -   5        $��SliderStackPanel��5      5�      5�      5�      � 5�       � Orientation$ 
Horizontal=�6   .+�5�        $5 /[b]Highest Attaible CWA For Current Semester : ��6"   $��Center=�6[   �� 5�      -   5        $��
HighestCwa��5�      ��6*   �� *
HighestCwa+$ OneWay=�  5�      $5 /[b]Highest Attaible CWA For Current Semester : ��6"   $��Center=�6[   $��20,0,0,0q�6v   �� 5�      -   5        $��	LowestCwa��5�      ��6*   �� *	LowestCwa+$ OneWay=�5�       J� Effect 5�      -  'System.Windows.Media.Effects.BlurEffect  5�      -   5        
BlurEffect��   Radius$ -1Q�5�   1    5�      :� 5�      $ Canvas.MouseUp��6   [�5�      �� 5�      .��5�      �� .��5�   "   W� $ ControlsListBox��5�   %   $ Width�5�   %   $ 	0:0:0:0.3D�5�   %    .  5�   %   W� 5�   "   $ 
BlurEffect��62   $ Radius�5�   :   $	 5X�5�   :   $
 -1X�6C    .  $ 1x��5�   :   $ 	0:0:0:0.1D�6N   5�      5�      5�      5�      �� 5�   
   -   5        $��CwaValue��5�      $��1��6!   $��4��6.   $��4��6?   .+�5�        -   5        $��CwaLabel��5�      
���  ��$��	ExtraBold"�5�   0   $	 100��5�      $��20(�60   $ 1��6>   $y�Hidden=�6M    5�   .   �� *CWA+$ TwoWay=�$ PropertyChanged=�z�5�      T� 5�      5�      5�      �� 5�   
   -   5        $��SidePaneCanvas��5�      $��1��6'   $��4��64   .+�5�      �� -   5        $��ControlsListBox��5�      $��3��6.   $
��Left=�6?   $��0��6[   $��1q�6g   $��144444��6{   .@�5�      �� -   5        $��CalculateCwaButton��5�      $
��Left=�63   $��CalculateCwa��6O   $	��200��6f    5�      :� 5�      $ Button.Click��6'   [�5�      �� 5�   "   .��5�   &   �� .��5�   *   W� $ CwaLabel��6:   $ Opacity�5�   :   $	 0X�5�   :   $	
 100X�5�   :    .  $ 	0:0:0:0.5D�5�   ;   W� 5�   *   $ CwaLabel��6:   $ RenderTransform.X�5�   :   $	 0X�5�   :   $	
 140X�6C    .  $ 1x��5�   :   $ 	0:0:0:0.5D�6N   5�   '   5�   #   5�      5�      �� 5�      -   5        $��PreviousRecordButton��5�      $
��Left=�65   $��EditPreviousRecord��6Q   $	��200��6n   6z   5�      5�      5�      5�      5�      