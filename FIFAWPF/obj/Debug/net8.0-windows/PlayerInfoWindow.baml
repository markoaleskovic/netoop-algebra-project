   M S B A M L   `   `   `  ����  FIFAWPF    FIFAWPF.PlayerInfoWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Player Information��5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	   
������  � ShowInTaskbar$ False��5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   #��Z��5   	   #��Z��5   	    � Topmost$
 True��5   	    � 	Resources 5       �� 
TargetType	 �� Property
 �� Value   PopupShowAnimation �� TargetProperty W� From	 W� To g� Duration��%�  '��       &	  �     �� 5   
   " ��6   .�5      �� 	 � ��
 �#FFFFFF6+   �� 5      	 � ��$
 Segoe UI)�6+   �� 5      	 � ��$
 18(�6)   5      �� 5   
   .��5      W� $ Opacity�6    05       16'   $ 0:0:0.3D�6.   W� 5      $/ )(RenderTransform).(ScaleTransform.ScaleX)�6    0.75       16)   $ 0:0:0.3D�60   W� 5      $/ )(RenderTransform).(ScaleTransform.ScaleY)�6    0.75       16)   $ 0:0:0.3D�60   5      5      .��5      � -   5        ���	#80000000.+�5   
   �� -   5         ��Name$ 	InfoPanel��5       �� CornerRadius$ 15��5      ���	#CC1E1E1E���	#40FFFFFF$��1q�6R    J� RenderTransformOrigin$ 0.5,0.5&�5       $	��340��5!      $	��340��6   $��Center=�5"      $��Center=�6.   z�5$      �� 5%       �� ScaleX$	 0.7Q�6!    �� ScaleY$	 0.7Q�6.   ._�5'      � $��20q�6    � RowDefinitions 5(      �� 5)      $
��Auto �6$   �� 5*      $��* �6$   .+�5,      � -   5         �Name$ LoadingIndicator��5,      $��4��61   ���	#CC1E1E1E$y�Visible=�6Y   .+�5-      �  � IsIndeterminate$
 True��6"   $��50��5.      $��50��6$   ���#FFFFFF
������ 50      51      �� 53      $��1��6   $��0,20,0,0q�6*   $��Center=�6<   .+�54      �� $��28(�55      $��SemiBold"�56      $��Center=�57      $��0,0,0,10q�58      ��54       �� *
PlayerName+58      �� 59       �� Orientation$ 
Horizontal=�6!   $��Center=�6:   $��0,0,0,10q�6W   .+�5:      �� $��Number:��6$   $
��Bold"�63    J� Opacity$	 0.8Q�6E   �� 5;      $��5,0,0,0q�6B   ��6$   �� *Shirt_Number+6B   5<      �� 5=      $ 
Horizontal=�6!   $��Center=�6:   $��0,0,0,10q�6W   .+�5>      �� $��	Position:��6$   $
��Bold"�65   $	 0.8Q�6G   �� 5?      $��5,0,0,0q�6>   ��6$   �� *
Position+6>   5@      �� 5A      $ 
Horizontal=�6!   $��Center=�6:   $��0,0,0,10q�6W   .+�5B      �� $��Total Goals:��6$   $
��Bold"�68   $	 0.8Q�6J   �� 5C      $��5,0,0,0q�6;   ��6$   �� *Goals+6;   5D      �� 5E      $ 
Horizontal=�6!   $��Center=�6:   $��0,0,0,10q�6W   .+�5F      �� $��Total Yellow Cards:��6$   $
��Bold"�6?   $	 0.8Q�6Q   �� 5G      $��5,0,0,0q�6B   ��6$   �� *Yellow_Cards+6B   5H      �� 5I      $ 
Horizontal=�6!   $��Center=�6:   $��0,0,0,10q�6W   .+�5J      �� $��Captain:��6$   $
��Bold"�64   $	 0.8Q�6F   �� 5K      $��5,0,0,0q�6=   ��6$   �� *	Captain+6=   5L      5M      5N      5O      5P      5Q      