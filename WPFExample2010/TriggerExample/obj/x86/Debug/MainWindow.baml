   M S B A M L   `   `   `  ����  TriggerExample    TriggerExample.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � 	Resources 5       �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value ]� 
Conditions	 �� Property
 �� Value :� RoutedEvent �� TargetProperty g� Duration	 W� To g� AutoReverse �� Binding �� ElementName �� Path �� Value��%�  '��       �� 5   
   " ��6   �5      �� 5	       � ��
 �   �66   �� 5
       � ��
 �����66   �� 5       � ��$ Verdana)�66   �� 5       � ��$ 10q�62    5      P� 5       �@J�IsMouseOver . . �5      ��  � ��$ 14(�61   �� 5       � ��
 �  ��63   5      ]� 5       5      �� 5      	 �@J�IsMouseOver
 . 6;   �� 5      	 � ��
 . 69   /�5      �� 5       � ��$ 14(�65   �� 5       � ��
 � � �67   5      :� 5#      $ Click��6   [�5$      �� 5%      .��5&      �� .��5'   "   W� $ Width�62   $ 0:0:1D�6T   $	 220X�6e    . 6n   W� 5(   "   $ Height�62   $ 0:0:1D�6U   $ 80X�6f    . 6n   5)      5*      5,      �� 5.       6   �� $ 	checkBox1��$ 	IsChecked� True5/      .a�50      ��  � {� .  62   51      57      58      .��5:      � .+�5<   
   �� 
���   �.+�5=      �� -   5         ��Name$ button1��5=      $��Кнопка 1��6&   $	��100��69   $��20��6E   �� 5>      -   5        $ button2��5>      $��Кнопка 2��6&   $	��100��69   $��20��6E   �� 5?      -   5         ��Name$ 	checkBox1��5?      $	��100��6*   $��20��66   $��Disable��6B   5@      5A      5B      