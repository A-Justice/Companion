   M S B A M L   `   `   `  ����H  DCompanion 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Companion_1._0.PagesCompanion_1._0.Pages  5      =#clr-namespace:ViewModels.ViewModelsViewModels.ViewModels  5      '    !Companion_1._0.Pages.Registration   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Companion_1._0.Pages     5      N JFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  5   
   3	viewmodel#clr-namespace:ViewModels.ViewModels  5      -   5        .��5      �   � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��2* �6   .+�5   
   �� $��1��6   $��1��6$   .��5      �� $	��200��6   .+�5      �� $��Sign Up��6      Heading2#��[  6+   $��0,0,0,8q�6M   6  0FirstFloor.ModernUI.Windows.Controls.BBCodeBlock  5         BBCode$C =There are a few things companion would like to know about you��6"   $��0,0,0,16q�6i   �� 5      -   5         ��Name$
 Form��5       �� Orientation$ Vertical=�6+    � 	Resources 5       �� 
TargetType �� Property �� Value	 �� BasedOn��%�  '��       '�� �     7��'�� �     7��'	� I    7	��� 5      " ��6    .�5       ��  �@��Orientation$ 
Horizontal=�6<   �� 5!       � ��$ 0,0,0,4q�67   5"      �� 5#      " ��6    8	   .�5$      ��  � ��$	 100��66   �� 5%       � ��$ Center=�6B   5&      �� 5'      " ��6    8	   .�5(      ��  � ��$	 0,3q�68   5)      �� 5*      " 	�6    8	   .�5+      ��  � ��$	 0,3q�68   5,      5-      .+�5/      �� .+�50      �� $��
First name��6    
 �� Target
 65   ��  �� ElementName$ TxtFirstName���� 51      -   5         ��Name$ TxtFirstName��51   "   $	��200��68   ��6D   �� *	FirstName+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . 52      �� 53      .+�54      �� $��	Last name��6    
 64   �� $ TxtLastName���� 55      -   5        $ TxtLastName��55   "   $	��200��67   ��6C   �� *
LastName+$ TwoWay=� . 56      �� 57      .+�58      �� $��Gender��6    
 61   �� $ RadioGendeMale��	� 59      -   5         	�Name$ RadioGendeMale��59   &    d� 	IsChecked 6>   �� *Male+$ TwoWay=� . .��6�   Male5;      	� 5<      $��Female��6&   $��8,0,0,0q�67    6H   �� *Female+$ TwoWay=� . 5=      �� 5>      .+�5?      �� $��
Birth date��6    
 65   �� $ 	DateBirth��(  "System.Windows.Controls.DatePicker  5@      -   5          Name$ 	DateBirth��5@   %      SelectedDate 68   �� *	BirthDate+$ TwoWay=� . 5A      �� 5B      .+�5C      �� $��Program��6    
 62   �� $ 
cboProgram���� 5D      -   5         ��Name$ 
cboProgram��5D   #   $	��200��67    �� SelectedItem 6C   �� *	Program+$ TwoWay=� . 5F      �� 5G      .+�5H      �� $��CurrentLevel��6    
 67   �� $ cboCurrentLevel���� 5I      -   5        $ cboCurrentLevel��5I   #   $	��200��6<    6H   �� *CurrentLevel+$ TwoWay=� . 5J      �� 5K      .+�5L      �� $��CurrentSemester��6    
 6:   �� $ cboCurrentLevel���� 5M      -	   5        $ cboCurrentSemester��5M   #   $	��200��6?    6K   �� *CurrentSemester+$ TwoWay=� . 5O      5P      �� 5Q      .+�5R      �� $��Institution��6    
 66   �� $ cboCurrentLevel���� 5S      -
   5         ��Name$ TbInstitution��5S   $   $	��200��6;   UMaT5U      5V      �� 5W      .+�5X      �� $��Email��6    
 60   �� $ TxtEmail���� 5Z      -   5        $ TxtEmail��5Z   "   $	��200��64   ��6@   �� *Email+$ TwoWay=� . 6~   �� 5[      -   5        $ TxtErrorEmail��5[   "   $	��200��69   {�. $��Hobo Std)�5\   3   $��16(�6I    �� TextAlignment$ Right=�6W    J� Opacity$	 0.5Q�6m   
������ 
���  ��
������ ��5[   E   �� *
ErrorEmail+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5]   L   5`      �� 5a      .+�5b      �� �� 5c      -   5        $��chkAgree��5c   #    63   �� *Agreed+$ TwoWay=�$ PropertyChanged=�.��6�   I agree to the terms of use5e      5f      �� 5h      -   5        $��SubmitButton��5h      $��Submit��61   $��
100,16,0,0q�6B   $
��Left=�6V   ��6q   �� *SubmitButtonCommand+5i      5j      5k      5l      5m      5n      