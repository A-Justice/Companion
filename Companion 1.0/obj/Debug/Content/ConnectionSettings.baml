   M S B A M L   `   `   `  ����H  DCompanion 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Companion_1._0.ContentCompanion_1._0.Content  5      /    )Companion_1._0.Content.ConnectionSettings   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:Companion_1._0.Content     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��1* �6   .+�5   
   �� $=��7Configure proxies and credential to access the internet��6   .��5      �   5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $	��97* �6$   .+�5      	� -   5        $��	rdNoProxy��5      $��No Proxy��6/   $��0��6B    d� 	IsChecked 6l   �� *	NoProxy+ �� UpdateSourceTrigger$ PropertyChanged=�	� 5      -   5        $��rdSystemProxy��5      $��Use System Proxy��63   $��1��6N    6x   �� *
SytemProxy+$ PropertyChanged=�	� 5      -   5        $��rdManualProxy��5      $ ��Manual Proxy Configuration��63   $��2��6X    6�   �� *ManualProxy+$ PropertyChanged=��� 5       -   5        $	��stP��5       $��3��6(    �� Orientation$ 
Horizontal=�65   $��5q�6N   {�.  .+�5!      �� $��HTTP Proxy :��6   $��Center=�63   �� 5"      -   5        $��txtProxy��5"      $��5q�6.   $	��150��69   ��6E   �� *Proxy+$ PropertyChanged=��� 5#      $��Port :��6   $��Center=�6-   �� 5$      -   5        $��txtPort��5$      $��5q�6-   $	��150��68   ��6D   �� *Port+$ PropertyChanged=�5&      �� 5'      -   5        $��
chkUseCred��5'      $��Use Credentials��6-   $��4��6G   $��Center=�6T   $��5q�6q   $��17��6|   $	��Top=�6�   {�.   5(   H   �� *UseCredentials+$ PropertyChanged=��� 5)      -   5        $��gbCred��5)      $��5��6)   $��Credentials��66   .��5*      �   5+      �� 5,      $
��Auto �6,   �� 5-      $
��Auto �6,    5/      �� 50      $
��Auto �6/   �� 51      $
��131* �6/   �� 52      $	��91* �6/   .+�54      �� $��
UserName :��6    $��Center=�65   $��Center=�6Y   �� 55      $��
PassWord :��6    $��1��65   $��Center=�6B   $��Center=�6f   �� 56      $��1��6"   $��5q�62   ��6=   �� *
UserName+$ PropertyChanged=��� 57      $��1��6"   $��1��6/   $��5q�6?   ��6J   �� *
PassWord+$ PropertyChanged=�58      59      5:      5;      5<      5=      