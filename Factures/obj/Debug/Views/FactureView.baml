   M S B A M L   `   `   `  ����C  ?Factures, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Factures.ViewsFactures.Views  5           Factures.Views.FactureView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Factures.Views     5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      $��Times New Roman)�5	      $��14(�6+   .��5      � {�.   � ColumnDefinitions  5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Factures��5         AlternatingRowBackground
 �����$��1��5      $��1��6#      CanUserAddRows .  $	��100��5      $��50��6"   $��Times New Roman)�5         AutoGenerateColumns .     
IsReadOnly . ��5      �� *
Factures+   Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� 
 �� Path$
 Id�  5#      $ Number��6%   	 65   �� $
 Number�  5$      $
 Name��6%   	 63   �� $

 Name�  5%      $ Season��6%   	 65   �� $
 
SeasonYear�  5&      $ Customer��6%   	 67   �� $
 CustomerName�  5'      $ Delivery Date��6%   	 6<   �� $
 Delivery�4  .System.Windows.Controls.DataGridTemplateColumn  5(      $
 View��6)      CellTemplate 5)      �� 5*      .R�5+      ��  ��Name$
 View��6%     Caliburn.Micro.Message   Attach$( "[Event Click]=[View($dataContext)]��5,   %   $
��View��5-   %   66   5.      50      52      � 55   
   $��2��6   $��1��6     56      �� 57      $��20 �6#   �� 58      $
��auto �6#   �� 59      $
��auto �6#   �� 5:      $
��auto �6#   �� 5;      $
��auto �6#   �� 5<      $��* �6#    5>      �� 5?      $
��auto �6    �� 5@      $��* �6    �� 5A      $��20 �6    .+�5E      �� -   5        $ 	CreateNew��5F      $��1��5E      $��0��6%   $	��100��5G      $��	10,0,10,0q�5H      .��6(   
Create New64   �� 5I      $��2��6   $��0��6(   $��	10,0,10,0q�5J      Search By Number:5K   -   �� 5L      -   5         ��Name$ SearchNumber��5L      $��3��5M      $��0��6&   $	��200��5N      $��	10,0,10,0q�5P      ��5O      �� $
 SearchNumber� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5P      �� 5Q      -   5        $ Search��5R      $��4��5Q      $��0��6%   $	��100��5S      $��	10,0,10,0q�5T      .��6(   Search60   �� 5V      -   5         ��Name$ 
ActiveItem��5W   &   $��1��5V      $��4��6-   $��1��5W      6&   5X      5Y      5Z      