   M S B A M L   `   `   `  ����  	Animation%    Animation.AnimationPathGeometry   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  AnimationPathGeometry��5   	   $	��300��6'   $	��300��64    � 	Resources 5         geometryPath E� IsClosed E� 
StartPoint � Points��%�   &	        B� 5   
   .��5      E�  . $ 10, 10&�6)   .��5	      � ' �    ����  ��In
��� �In
6"   5
      5      5      .��5      �� 
���   �.+�5   
   F� 
���  ��#��[  6   
 �� Top$ 10��6A    �� Left$ 10��6Q   A� 5   
   $��15��6   $��15��6   $	 177��6)   $	 120��6:   ��5      
� 5       
� RadiusX$ 1Q�6&    
� RadiusY$ 1Q�62   	 
� GradientOrigin$	 0.3, 0.3&�6>   .��5      � 
 � Color$
 White��6#    � Offset$ 0Q�61   � 5      $
 Black��6#   $ 1Q�61   5       � Triggers 5      :� 5       :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      T�  �� TargetProperty$ (Canvas.Top)�67    g� Duration$ 0:0:5D�5   7    g� RepeatBehavior$ Forever��6H    T� PathGeometry# [  5   7    T� Source$ Y=�6d   5       T� 5!      $ (Canvas.Left)�67   $ 0:0:5D�5"   7   $ Forever��6H   # [  5#   7   $ X=�6d   5$      5%      5&      5'      5)      5*      5,      