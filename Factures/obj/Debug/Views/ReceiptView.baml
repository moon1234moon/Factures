   M S B A M L   `   `   `  ����C  ?Factures, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Factures.ViewsFactures.Views  5           Factures.Views.ReceiptView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Factures.Views     5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      $��Times New Roman)�5	      $��14(�6+   .��5      �   � ColumnDefinitions  5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Receipts��5         AlternatingRowBackground
 �����$��1��5      $��1��6#      CanUserAddRows .  $	��100��5      $��50��6"   $��Times New Roman)�5         AutoGenerateColumns .     
IsReadOnly . ��5      �� *
Receipts+   Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� 
 �� Path$
 Id�  5$      $ Number��6%   	 65   �� $
 Number�  5%      $ Customer��6%   	 67   �� $
 CustomerName�  5&      $ Delivery Date��6%   	 6<   �� $
 Delivery�4  .System.Windows.Controls.DataGridTemplateColumn  5'      $
 View��6)      CellTemplate 5(      �� 5)      .R�5*      ��  ��Name$
 View��6%     Caliburn.Micro.Message   Attach$( "[Event Click]=[View($dataContext)]��5+   %   $
��View��5,   %   66   5-      5/      51      � 54   
   $��2��6   $��1��6     55      �� 56      $��20 �6#   �� 57      $
��auto �6#   �� 58      $
��auto �6#   �� 59      $
��auto �6#   �� 5:      $
��auto �6#   �� 5;      $��* �6#    5=      �� 5>      $
��auto �6    �� 5?      $��* �6    �� 5@      $��20 �6    .+�5D      �� -   5        $ 	CreateNew��5E      $��1��5D      $��0��6%   $	��100��5F      $��	10,0,10,0q�5G      .��6(   
Create New64   �� 5H      $��2��6   $��0��6(   $��	10,0,10,0q�5I      Search By Number:5J   ,   �� 5K      -   5         ��Name$ SearchNumber��5K      $��3��5L      $��0��6&   $	��200��5M      $��	10,0,10,0q�5O      ��5N      �� $
 SearchNumber� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5O      �� 5P      -   5        $ Search��5Q      $��4��5P      $��0��6%   $	��100��5R      $��	10,0,10,0q�5S      .��6(   Search60   �� 5U      -   5         ��Name$ 
ActiveItem��5V   &   $��1��5U      $��4��6-   $��1��5V      6&   5W      5X      5Y      