   M S B A M L   `   `   `  ����C  ?Factures, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Factures.ViewsFactures.Views  5      !    Factures.Views.ProductsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Factures.Views     5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      $��Times New Roman)�5      $��14(�6+   .��5      �   � ColumnDefinitions  5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Products��5         AlternatingRowBackground
 �����$��1��5      $��1��6#      CanUserAddRows .  $	��100��5      $��50��6"   $��Times New Roman)�5         AutoGenerateColumns .     
IsReadOnly . ��5      �� *
Products+   Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� 
 �� Path$
 Id�  5#      $
 Name��6%   	 63   �� $

 Name�  5$      $ Details��6%   	 66   �� $
 Details�  5%      $ Customer��6%   	 67   �� $
 CustomerName�  5&      $ Price��6%   	 64   �� $
 	FullPrice�4  .System.Windows.Controls.DataGridTemplateColumn  5'      $
 View��6)      CellTemplate 5(      �� 5)      .R�5*      ��  ��Name$ BtnView��6%     Caliburn.Micro.Message   Attach$+ %[Event Click]=[BtnView($dataContext)]��5+   %   $
��View��5,   %   66   5-      5/      51      � 54   
   $��2��6   $��1��6     55      �� 56      $��20 �6#   �� 57      $
��auto �6#   �� 58      $��* �6#    5:      �� 5;      $
��auto �6    �� 5<      $��* �6    �� 5=      $��20 �6    .+�5?      �� -   5        $ 	CreateNew��5@      $��1��5?      $��0��6%   $��0,0,5,0q�5A      .��6&   
Create New62   �� 5B      -   5         ��Name$ 
ActiveItem��5C   &   $��1��5B      $��2��6-   $��1��5C      6&   5D      5E      5F      