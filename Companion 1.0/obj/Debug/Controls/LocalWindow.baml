   M S B A M L   `   `   `  ����H  DCompanion 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Companion_1._0.ContentCompanion_1._0.Content  5      A%clr-namespace:Companion_1._0.ControlsCompanion_1._0.Controls  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089N JFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null  �� MergedDictionaries �� Source)    #Companion_1._0.Controls.LocalWindow �� 
TargetType �� Property   WindowBackgroundContent �� Value  DefaultFontFamily  DefaultFontSize  
WindowText&   System.Windows.Media.TextOptions �� 
TargetType ��Name%  System.Windows.SystemParameters$   WindowNonClientFrameThickness �� Source  WindowBackgroundColor	 �� Color
 � 	Resources  Key �� 
TargetName �� TargetProperty	 �� To g� Duration  WindowBorder �Name � RowDefinitions    BackgroundContent 
 Accent  LocalWindowTitle    IsTitleVisible � DataContext 	 ToUpperConverter �� 	Converter  
 BooleanToVisibilityConverter �� 	Converter � LayoutTransform �� Angle    LogoData  NullToVisibilityConverter �� ConverterParameter �� Stretch �� Orientation'  !System.Windows.Shell.WindowChrome   IsHitTestVisibleInChrome  SystemButton#  System.Windows.SystemCommands   MinimizeWindowCommand �� RelativeSource �� Mode  �� AncestorType! ��Name"   RestoreWindowCommand# � UseLayoutRounding$   MaximizeWindowCommand%   CloseWindowCommand&    
TitleLinks' ��Name	( �� X1	) �� Y1	* �� X2	+ �� Y2  SeparatorBackground  SystemButtonLink, �� ElementName- �� Triggers. �� Binding/ �� Value0 �� 
TargetName5  /FirstFloor.ModernUI.Windows.Controls.ModernMenu1    MenuLinkGroups2   
LinkGroups3   SelectedSource4 �� Mode7  1FirstFloor.ModernUI.Windows.Controls.ModernButton5   IconData6 � ToolTip6  0FirstFloor.ModernUI.Windows.Controls.ModernFrame7  Name8    ContentLoader9   ContentLoader:   Source; F�Name  WindowBorderActive< �� Triggers= P� Property> P� Value? ]� 
Conditions@ �� PropertyA �� ValueB   CornerRadiusC   GlassFrameThicknessD   UseAeroCaptionButtonsE   NonClientFrameEdges�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      1modernui$clr-namespace:Companion_1._0.Content  5      3	Lcontrols%clr-namespace:Companion_1._0.Controls  5      5controls&http://firstfloorsoftware.com/ModernUI  5        5      �� 5   
   $7 1/FirstFloor.ModernUI;component/Assets/Button.xaml?�6   �� 5	   
   $; 5/FirstFloor.ModernUI;component/Assets/Converters.xaml?�6   %N  '         7 	 7 
 7  7  7  7  7  7  7 
 �� 5      "   6   .�5   
   ��  �@  BackgroundContent# �   6.   �� 5   
    � ��# �  6'   �� 5   
    � ��# �  6%   �� 5   
    � ��# �  6'   �� 5   
    � ��$	 800��6"   �� 5   
    � ��$	 640��6#   �� 5   
    � ��$	 320��6%   �� 5   
    � ��$	 320��6&   �� 5   
    �@�
ResizeMode$ CanResizeWithGrip=�6'   �� 5   
    �@�UseLayoutRounding . 6.   �� 5   
    �@ TextFormattingMode$ Display=�6;   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� $ WindowBorder��6   ��63   �� # Z ��5      �� 5      WindowBorderBackground��#	 �  6N   
 5       ���� 5!      BackgroundAnimation .��5"   "   �� $ WindowBorderBackground��61   $ Color�6`   # �  6�   $ 0:0:.6D�6�   5#      5$      ._�5&      � .+�5'      �� #���  6%   $��1q�6R   ._�5(   "   �� .u�5)   &   � $ 
LayoutRoot��6+    5*   *   �� 5+   .   $��36 �6<   �� 5,   .   $
��Auto �6<   �� 5-   .   $
��Auto �6<   �� 5.   .   $��* �6<   �� 5/   .   $��15 �6<   .+�53   *   �� $��5��69   #��z 6J   �� 56   *   $��5��61   #���  57   1   $z�1,0,0,1,0,32W�58   1   $��14��59   1   $��53��6?   $
��Left=�5:   1   $	��Top=�5;   1   ._�5<   .   �� 
�������$	��0,8q�5>   8   #���  5?   8   $��Center=�5@   8   # z 5A   8   ��5<   8   �� *Title+8   y�5B   8   �� 8   5C   2   �� 5D   6   $	 -90Q�6F   5F   /   5G   +   �� 5J   *   #���  61   $��36��6W   $��36��6b   $��Right=�6n   $	��Top=�6�   $��0,0,76,0q�6�   # z 5K   1   y�5L   1   �� 8  $ inverse��._�5M   .   F� $
 Fill=�6D   
�������$��24��6`   $��24��6k   $��Center=�6w   $��Center=�6�   ��63   �� 6�   5N   +   �� 5Q   *   $ 
Horizontal=�65   $��Right=�6N   $	��Top=�6j   $��0,8,6,0q�6�    . .+�5R   .   �� 8�� ��65   �� # Z ��5S   2   � 5T   6   $��13��6;   $��12��6F   $z�1,0,0,1,0,1W�6R   .+�5U   :   F� ����    ��[ � z ��[ 	$��8��6R   $��7��6\   $��Center=�6g   $��Center=�6�   $��2��5V   �   ��6?   �� *
Foreground+ �� $ FindAncestor=�$  ButtonL�6�   5W   7   5Y   /   � 5Z   .   $��1,0,1,0q�63   .+�5[   2   �� $! Restore��69   8�� $y�	Collapsed=�6�   ��6J   �� # Z" ��5\   6   � 5]   :   $��13��6?   $��12��6J   # . $z�1,0,0,1,.5,.5W�6o   .+�5^   >   F� X���Ѐ�     � z     � z ��[ �    ��- р�[ ��- �    �� р�[ �� р�[  z �     z 	$��8��6y   $��8��6�   $��Center=�6�   $��Center=�6�   $��1��5_   �   ��6C   �� *
Foreground+ �� $ FindAncestor=�$  ButtonL�6�   5`   ;   5b   3   �� 5c   2   $! Maximize��69   8�� ��6K   �� # Z$ ��5d   6   � 5e   :   $��13��6?   $��12��6J   .+�5f   >   F� +����    @B �@T� @B �@T�  z �     z 	$��9��6`   $��8��6j   $��Center=�6u   $��Center=�6�   $��2��5g   �   ��6C   �� *
Foreground+ �� $ FindAncestor=�$  ButtonL�6�   5h   ;   5j   3   5k   /   �� 5l   .   8�� ��65   �� # Z% ��5m   2   � 5n   6   $��13��6;   $��12��6F   $z�1,0,0,1,0,1W�6R   .+�5o   :   F� +����        � z ��j � z     �    ��j 	$��8��6\   $��7��6f   $��Center=�6q   $��Center=�6�   $	��1.5��5p   �   ��6?   �� *
Foreground+ �� $ FindAncestor=�$  ButtonL�6�   5q   7   5s   /   5t   +   �� 5w   *   #��z& 67   $��Right=�6b   $	��Top=�6~   $��	0,6,180,0q�6�    . ��5x   .   �� 5y   2   .R�5z   6   �� $ 
Horizontal=�6A   5{   3   ��5}   .   �� 5~   2   .R�5   6   �� $ 
Horizontal=�6A   .+�5�   :   �� $' 	Separator��6?   $( .5��6R   $) 3��6Z   $* .5��6a   $+ 12��6i   $��7,0,5,0q�6q   #���  6�   �� 5�   :   $!��NavigationCommands.GoToPage��5�   A   8�� ��5�   A   �� *DisplayName+��5�   A   �� *Source+��5�   A   �� $, ContentFrame��5�   A   5�   7   - 5�   6   �� 5�   :   . 6F   ��  �� *PreviousData+/ 6�   Z� .a�5�   >   �� $0 	Separator��6[    � y�$ 	Collapsed=�6r   5�   ;   5�   3   5�   +     5�   *   $��1��6>   $��36,0,0,0q�5�   >   #2 z1 5�   >   3 5�   >   �� *Source+$, ContentFrame��$4 TwoWay=�5�   >     5�   *   a5 ��@����O�@������p\-�������@��� �8@����ا  6n� �8��� ������� �p\-��O�@����O$#��NavigationCommands.BrowseBack��5�   @   $
6 back��5�   @   $��2��5�   @   $
��Left=�6Q   $	��Top=�6l   $��	34,24,0,0q�6�    . ��5�   @   �� $, ContentFrame��5�   +     5�   .   $7 ContentFrame��6C   $��3��6Y   $��2��6f   $��
36,8,16,16q�6w   #9 z8 6�   : 6�   �� *ContentSource+ �� *TemplatedParent+$4 TwoWay=�6�   � 5�   *   $��4��6/   .+�5�   .   F� $; 
ResizeGrip��63   $y�	Collapsed=�6G   $��12��6^   $��12��6i   $��1q�6u   $��Right=�6�   #���  5�   3   $��1��5�   3   $
 None=�5�   3   �����@B ��� ���- ��� �@KL ��� ���j ��� �@T� ��� ��ا ��� Ѐ� @T� р� �ا Ѐ�[ @T� р�[ �ا Ѐ�� @T� р�� �ا �@KL ��[ ���j ��[ �@T� ��[ ��ا ��[ Ѐ�[ @KL р�[ ��j Ѐ�� @KL р�� ��j �@T� �� ��ا �� Ѐ�� @B р�� ��- 5�   3   5�   +   5�   '   5�   #   5�      �� 5�      #���  6%   $��1q�6X   y�6l   �� *
IsActive+ �� *FindAncestor+"  �8  5�      5�      < 5�      P� 5�      = �@�WindowState$> 	Maximized=�69   . �5�      �� $0 Maximize��6%    � y�$ 	Collapsed=�6Q   �� 5�      $0 Restore��6%    � y�$ Visible=�6P   �� 5�      $0 
LayoutRoot��6%    � ��$ 7q�6O   5�      P� 5�      = �@�WindowState$> Normal=�69   . �5�      �� $0 Maximize��6%    � y�$ Visible=�6Q   �� 5�      $0 Restore��6%    � y�$ 	Collapsed=�6P   5�      ]� 5�      ? 5�      �� 5�   "   @ �@�
ResizeMode$A CanResizeWithGrip=�6B   �� 5�   "   @ �@�WindowState$A Normal=�6C   ./�5�      �� $0 
ResizeGrip��6%    � y�$ Visible=�6S   5�      5�      5�      �� 5�   
    �@ WindowChrome 5�        5�      $B 0��6   $C 1q�60   D .  $
E None=�6f   5�      5�      5�      