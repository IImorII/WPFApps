   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  5   	       WpfApp2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp2     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  J� RenderTransformOrigin$ 0.492,0.541&�6   .+�5
   
   �� -   5         ��Name$ 
ButtonData��5
      $��Рассчитать��6%   $��Center=�6:   $��365,331,351,0q�6W   $	��Top=�6n   $��76��6�   $��23��6�   �� 5   
   -   5         ��Name$ 
InputBox_1��5      $
��Left=�6&   $��23��6A   $��182,40,473,0q�6M    �� TextWrapping$
 Wrap=�6c   $��Введите xMin��6w   $	��Top=�6�   $	��137��6�   6�   �� 5   
   -   5        $ 
InputBox_2��5      $
��Left=�6&   $��23��6A   $��182,68,473,0q�6M   $
 Wrap=�6c   $��Введите xMax��6w   $	��Top=�6�   $	��137��6�   6�   �� 5   
   -   5        $ 
InputBox_3��5      $
��Left=�6&   $��23��6A   $��182,96,473,0q�6M   $
 Wrap=�6c   $��Введите шаг dx��6w   $	��Top=�6�   $	��137��6�   6�   �� 5   
   $��Center=�6   $	��100��6-   $��295,119,397,0q�6:   $	��Top=�6Q   $	��100��6i   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ BoxData��5         RowHeaderWidth$ 0Q�6%   "   VerticalScrollBarVisibility$
 Auto=�68   $   HorizontalScrollBarVisibility$ Hidden=�6\   	   AutoGenerateColumns	 .  $
��Left=�6�   $	��257��6�   $��
540,40,0,0q�6�   $	��Top=�6�   $	��220��6�   
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ X��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6K   1  +System.Windows.Controls.DataGridBoundColumn   Binding 60   ��  �� Path$ x�6K     5      $ Y��6%   $	 100 6K    60   �� $ y�6K   5      �� 5   
   $��/Resources/Function.jpg��6   $
��Left=�61   $	��161��6L   $��
56,136,0,0q�6Y   $	��Top=�6m   $	��442��6�   5      5      