   M S B A M L   `   `   `  ����H  DCompanion 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Companion_1._0.Cwa_EngineCompanion_1._0.Cwa_Engine  5      /    )Companion_1._0.Cwa_Engine.SemesterAverage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      N JFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  5      3local'clr-namespace:Companion_1._0.Cwa_Engine  5      -   5        .��5
      �   � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��16* �6   �� 5      $	��13* �6   �� 5      $
��Auto �6   �� 5      $	��98* �6   �� 5      $
��Auto �6   �� 5      $��6* �6   .+�5   
   6  0FirstFloor.ModernUI.Windows.Controls.BBCodeBlock  $��1��6      BBCode$_ Y[b]Choose the Program , level and Semester you wish to check the semester average For[/b]��6'   $��1��6�   �� 5   
   $��0,5,0,0q�6    �� Orientation$ 
Horizontal=�6'   $��2��6@   $��1��6M   .+�5      �� $��
Program : ��6   $��Center=�6*    �� Target 6E   ��  �� ElementName$ 
cboProgram���� 5      -   5        $��
cboProgram��5       �� SelectedItem 6)   �� *	program+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��Level : ��6   $��50,0,0,0q�6'   $��Center=�69    6T   �� $ cboLevel���� 5      -   5        $��cboLevel��5       6'   �� *level+$ PropertyChanged=��� 5      $��Semester : ��6   $��50,0,0,0q�6*   $��Center=�6<    6W   �� $ cboSemester���� 5       -   5        $��cboSemester��5        6*   �� *
semester+$ PropertyChanged=�5!      6  0FirstFloor.ModernUI.Windows.Controls.ModernFrame  5"   
   -   5        $��frame��5"      $��3��6'   $��1��64   $��0,10,0,0q�5#      5&      5'      