   M S B A M L   `   `   `  ����C  ?Factures, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Factures.ViewsFactures.Views  5      #    Factures.Views.ConversionView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Factures.Views     5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      $��Times New Roman)�5	      $��14(�6+   .��5      �   � ColumnDefinitions  5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Conversions��5         AlternatingRowBackground
 �����$��1��5      $��1��6#      CanUserAddRows .  $	��100��5      $��50��6"   $��Times New Roman)�5         AutoGenerateColumns .     
IsReadOnly . ��5      �� *Conversions+   Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ First Currency��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6=   �� 
 �� Path$
 FirstSymbol�  5$      $ Second Currency��6%   	 6>   �� $
 SecondSymbol�  5%      $ Ratio��6%   	 64   �� $
 Ratio�4  .System.Windows.Controls.DataGridTemplateColumn  5&      $
 View��6)      CellTemplate 5'      �� 5(      .R�5)      ��  ��Name$
 View��6%     Caliburn.Micro.Message   Attach$( "[Event Click]=[View($dataContext)]��5*   %   $
��View��5+   %   66   5,      5.        5/      $ Delete��6)    50      �� 51      .R�52      �� $ Delete��6%   $* $[Event Click]=[Delete($dataContext)]��53   %   $��Delete��54   %   68   55      57      59      � 5<   
   $��2��6   $��1��6     5=      �� 5>      $��20 �6#   �� 5?      $
��auto �6#   �� 5@      $
��auto �6#   �� 5A      $��* �6#    5C      �� 5D      $
��auto �6    �� 5E      $��* �6    �� 5F      $��20 �6    .+�5J      �� -   5        $ 	CreateNew��5K      $��1��5J      $��0��6%   $	��100��5L      $��	10,0,10,0q�5M      .��6(   
Create New64   �� 5O      -   5         ��Name$ 
ActiveItem��5P   &   $��1��5O      $��2��6-   $��1��5P      6&   5Q      5R      5S      