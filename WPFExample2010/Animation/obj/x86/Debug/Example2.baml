   M S B A M L   `   `   `  ����  	Animation    Animation.Example2   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Example2��5   	   $	��300��6   $	��300��6'   .��5      � .+�5	   
   A� -   5        $
��ball��5	      
���   �$��50��6.   $��50��69    �� Left$	 130��6E    �� Bottom$ 0��6W   ��5
      
� 5       
� RadiusX$ 1Q�6&    
� RadiusY$ 1Q�62    
� GradientOrigin$ 0.3, 0.3&�6>   .��5      �  � Color$ White��6#    � Offset$ 0Q�61   � 5      $
 Blue��6#   $ 1Q�60   5       � Triggers 5      :� 5      	 :� RoutedEvent$	 Window.Loaded��6   .[�5      �� .��5      �� .��5      �� 
 �� TargetProperty$!
 Fill.GradientStops[1].Color�6-   	 �� To$ YellowX�5   -    g� Duration$ 0:0:8D�69    g� AutoReverse .  g� RepeatBehavior$ Forever��5   -   5      5      5      5      5      5      