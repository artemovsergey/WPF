   M S B A M L   `   `   `  ����  	Animation    Animation.Example1   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Example1��5   	   $	��300��6   $	��300��6'    � Triggers 5      :� 5   
    :� RoutedEvent$ Button.Click��6   [�5      �� 5	      .��5
      ��  g� DesiredFrameRate$ 60X�6!   .��5      W�  �� 
TargetName$
 ball��6*    �� TargetProperty$ (Canvas.Bottom)�6G    W� From$ 0X�5   *   	 W� To$	 160X�63    g� AutoReverse . 	 g� Duration$	 0:0:2.5D�6O   
 g� RepeatBehavior$
 Forever��6b    g� DecelerationRatio$ 1Q�5   *   5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $
��Auto �6   .+�5   
   �� 
�������.+�5      A� -   5        $
��ball��5      
���  ��
���   �$��15��6=   $��15��6H    �� Left$	 130��5       �� Bottom$ 0��6(   5      �� 5   
   $��70��6   $��25��6   $��Кнопка��6(   $��1��69   $��10q�6F   5      5       