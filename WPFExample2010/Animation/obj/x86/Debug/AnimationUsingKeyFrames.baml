   M S B A M L   `   `   `  ����  	Animation'    !Animation.AnimationUsingKeyFrames   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  AnimationUsingKeyFrames��5   	   $	��300��6)   $	��300��66   .��5      � .+�5   
   �� 
���   �.+�5      A� $��20��6   $��20��6!    �� Bottom$ 0��6-    �� Left$	 120��6?   ��5      
� 5	       
� RadiusX$ 1Q�6*    
� RadiusY$ 1Q�66    
� GradientOrigin$ 0.3, 0.3&�6B   .��5
      �  � Color$ White��6'    � Offset$ 0Q�65   � 5      $ Black��6'   $ 1Q�65   5       � Triggers 5      :� 5      	 :� RoutedEvent$	 Window.Loaded��6#   .[�5      �� .��5      �� .��5   "   U� 
 �� TargetProperty$
 (Canvas.Bottom)�5   !    g� Duration$ 0:0:3D�5   !    g� AutoReverse .  g� RepeatBehavior$ Forever��5   !    g� DecelerationRatio$ 1Q�6:   .\�5   &   ��  O� KeyTime$ 0%��6;    O� Value$ 0Q�6H   �� 5   &   $	 33%��6;   $ 60Q�6I   �� 5   &   $	 66%��6;   $	 120Q�6I   �� 5   &   $	 99%��6;   $	 180Q�6I   5   #   �� 52   "   $!
 Fill.GradientStops[1].Color�53   !   $ 0:0:3D�54   !   $ Forever��62    . g�57   *   ��  �� KeyTime$ 0:0:1��6>    �� Value$ Green��6N   �� 58   *   $ 0:0:2��6>   $	 Red��6N   5:   #   5<      5=      5>      5@      5A      5B      5C      