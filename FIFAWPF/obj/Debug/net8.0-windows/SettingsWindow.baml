   M S B A M L   `   `   `  ����  FIFAWPF    FIFAWPF.SettingsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Application Settings��5   	   $	��500��5   	   $	��400��6    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Edit Application Settings��6   $��20(�5      $
��Bold"�6"   $��0,0,0,20q�64   �� 5   
   $��1��6   $��	0,10,0,10q�6"   .+�5      �� $��Language��6   $��0,0,0,20q�6)   .��5      �� $��10q�6   .+�5      	� -   5         	�Name$ 	rbEnglish��5   "   $��English��65    	� 	GroupName$ LanguageGroup��6G   $��0,0,0,5q�6a   	� 5      -   5        $ 
rbCroatian��5   "   $��Croatian��66   $ LanguageGroup��6I   5      5      �� 5      $��Display Mode��6   .��5      �� $��10q�6   .+�5      	� -   5        $ rbFullScreen��5   "   $��Full Screen��68   $ DisplayGroup��6N   $��0,0,0,10q�6g   6y   	� 5       -   5        $ 
rbWindowed��5    "   $��Windowed Mode��66   $ DisplayGroup��6N   $��0,0,0,10q�6g   6y   �� 5!      -   5         ��Name$ grpWindowSize��5!   G   $��Window Size��6   $��	20,10,0,0q�64   .��5"      �� $��10q�6%   .+�5#      	� -   5        $ rbSmall��5#   *   $��Small (1280x720)��6;   $ 	SizeGroup��6V   $��0,0,0,5q�6l   	� 5$      -   5        $ rbMedium��5$   *   $��Medium (1600x900)��6<   $ 	SizeGroup��6X   $��0,0,0,5q�6n   	� 5%      -	   5        $ rbLarge��5%   *   $��Large (1920x1080)��6;   $ 	SizeGroup��6W   5&      5'      5(      5)      5*      �� 5,   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,20,0,0q�6W   .+�5-      �� -
   5         ��Name$ btnSave��5-      $
��Save��6&   $��90��65   $��32��6@   $��0,0,10,0q�6L   	 �� 	IsDefault$
	 True��6^   6o   �� 5.      -   5        $ 	btnCancel��5.      $��Cancel��6(   $��90��69   $��32��6D   
 �� IsCancel$

 True��6P   6`   5/      50      51      