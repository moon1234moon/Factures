   M S B A M L   `   `   `  ����C  ?Factures, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Factures.ViewsFactures.Views  5      &     Factures.Views.CustomertypesView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Factures.Views     5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      $��Times New Roman)�5	      $��14(�6+   .��5      �   � ColumnDefinitions  5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ CustomerTypes��5         AlternatingRowBackground
 �����$��1��5      $��1��6#      CanUserAddRows .  $	��100��5      $��50��6"   $��Times New Roman)�5         AutoGenerateColumns .     
IsReadOnly . ��5      �� *CustomerTypes+   Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� 
 �� Path$
 Id�  5#      $
 Name��6%   	 63   �� $

 Name�4  .System.Windows.Controls.DataGridTemplateColumn  5$      $
 Edit��6)      CellTemplate 5%      �� 5&      .R�5'      ��  ��Name$ BtnEdit��6%     Caliburn.Micro.Message   Attach$+ %[Event Click]=[BtnEdit($dataContext)]��5(   %   $
��Edit��5)   %   66   5*      5,        5-      $ Delete��6)    5.      �� 5/      .R�50      �� $ 	BtnDelete��6%   $- '[Event Click]=[BtnDelete($dataContext)]��51   %   $��Delete��52   %   68   53      55      57      � 5:   
   $��2��6   $��1��6     5;      �� 5<      $��20 �6#   �� 5=      $
��auto �6#   �� 5>      $��* �6#    5@      �� 5A      $
��auto �6    �� 5B      $��* �6    �� 5C      $��20 �6    .+�5E      �� -   5        $ 	CreateNew��5F      $��1��5E      $��0��6%   $��0,0,5,0q�5G      .��6&   
Create New62   �� 5H      -   5         ��Name$ 
ActiveItem��5I   &   $��1��5H      $��2��6-   $��1��5I      6&   5J      5K      5L      