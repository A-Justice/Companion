   M S B A M L   `   `   `  ����H  DCompanion 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Companion_1._0.PagesCompanion_1._0.Pages  5      $    Companion_1._0.Pages.LoginPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      N JFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  5      .local"clr-namespace:Companion_1._0.Pages  5      $��104.228��5      $��406.07��60     � Title$  Login��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $	��100 �6    � RowDefinitions 5   
   �� 5      $��1 �6   �� 5      $
��Auto �6   .+�5   
   �� $��1��6   $��1��6"    �� Orientation$ Vertical=�62    � 	Resources 5       �� 
TargetType �� Property �� Value �� BasedOn��%�  '��       '�� �     7��'�� �     7��'	� I    7	��� 5      " ��6   .�5      ��  �@��Orientation$ 
Horizontal=�64   �� 5       � ��$ 0,0,0,4q�6/   5      �� 5      " ��6   8   .�5      ��  � ��$	 100��6.   �� 5        � ��$ Center=�6:   5!      �� 5"      " ��6   8   .�5#      ��  � ��$	 0,3q�60   5$      �� 5%      " 	�6   8   .�5&      ��  � ��$	 0,3q�60   5'      5(      .+�5*      �� $ 
Horizontal=�6   .+�5+      �� $��1��6   $��Name : ��6&   	 �� Target	 68   �� 
 �� ElementName$
 TxtName���� 5,      -   5        $��TxtName��5,      $	��200��6)   ��65   �� *Name+ �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�5-      �� 5/      $ 
Horizontal=�6   $��0,10,0,0q�63   .+�50      �� $��1��6   $��	Email :  ��6&   	 6:   �� $
 TxtEmail���� 51      -   5        $��TxtEmail��51      $	��200��6*   ��66   �� *Email+ . $ PropertyChanged=�$ TwoWay=�52      �� 55      $ 
Horizontal=�6   $��19��62   .+�56      �� 
������  �� TextAlignment$ Center=�6/   
������ {�.  |�.  $��Hobo Std)�58      $��Light"�61   $��13(�6D   $��	117,0,0,0q�6R   ��57   -   �� *ErrorMessage+$ PropertyChanged=�$ TwoWay=�58   R   59      5:      5=      5>      