   M S B A M L   `   `   `  ����@  <Task8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Task8Task8  5   	       Task8.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Task8     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��0.1* �6   6,   �� 5      6   �� 5      $
��0.2* �6   6,    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� -   5        $
��10,0q�5      $��1��6   .��6D   Start6K   �� 5   
   -   5         ��Name$ textBox��5      $��1��6#   $��3��60   $��15q�6D   $��15(�6P    �� TextWrapping$
 Wrap=�6^   6t   ]� 5   
   $��2��6   .+�5      �� $
��Left=�6   $
��0,15q�61   $��18(�6?   Count vowels:6\   �� 5      -   5         ��Name$ vowelsTextBlock��5      $
��Left=�61   $
��Left=�6G   $	��100��6c   $��	5,15,0,15q�6o   $��18(�6�   06�   5      ]� 5   
   $��2��6   $��1��6!   .+�5      �� $
��Left=�6   $
��0,15q�61   $��18(�6?   Count consonants:6`   �� 5      -   5        $ consonantsTextBlock��5      $
��Left=�65   $
��Left=�6K   $��18(�6f   $	��100��6u   $��	5,15,0,15q�6�   06�   5       ]� 5!   
   $��2��6   $��2��6!   .+�5"      �� $
��Left=�6   $
��0,15q�61   $��18(�6?   Count symbols:6]   �� 5#      -   5        $ symbolsTextBlock��5#      $
��Left=�62   $
��Left=�6H   $	��100��6d   $��	5,15,0,15q�6p   $��18(�6�   06�   5$      5%      5&      