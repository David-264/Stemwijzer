   M S B A M L   `   `   `  ����L  Hprojectstemwijzer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:projectstemwijzerprojectstemwijzer  5   	   I)clr-namespace:projectstemwijzer.DbClassesprojectstemwijzer.DbClasses  5   	   .    (projectstemwijzer.poletiekenpartijpagina   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:projectstemwijzer     5   	   8db)clr-namespace:projectstemwijzer.DbClasses     5   	     � Icon$$  /fotos/neutraalkieslabfoto.ico��5	   	   $	��300��5
   	   $	��500��5   	    � WindowStartupLocation$ CenterScreen=�5   	   ���#352D27 � Title$ poletiekenpartijpagina��5   	   $	��450��6(   $	��800��65    � 	Resources 5      ;   5projectstemwijzer.DbClasses.ByteArrayToImageConverter   ByteArrayToImageConverter��%   &	          5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      6   �� 5      $��3* �6   6*   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      $��3* �6   6,   �� 5      $��2* �6   6,   .+�5    
   &   System.Windows.Controls.DataGrid  -   5        $��uitkomstveld��5    s   $��1��6      
IsReadOnly$
 True��6    $��1��62   ���#EAE6E3   AutoGenerateColumns$ False��6W      Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Naam��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Naam+6L     5#      $
	 Info��6%   $

 auto 6L    63   �� *Info+6L   4  .System.Windows.Controls.DataGridTemplateColumn  5$      $
	 Foto��6)   $	
 100 67      CellTemplate 5%      �� 5&      .R�5'      �� $��80��6$   $��50��6/   ��6;   �� *Foto+ �� 	Converter# [  5(      5*      5,      � 5-   
   $��1��6   $��2��6    5.      �� 5/      $��1* �6    6.   �� 50      $
��0.3* �6    60   �� 51      6"   �� 52      $
��0.3* �6    60   �� 53      6"    55      �� 56      6%   �� 57      $��2* �6#   60   .+�59      �� $��0��6   $��1��6&   .+�5:      �� ���#EAE6E3$��
partijnaam��6-   �� 5;      ���#EAE6E3 �� CornerRadius$ 10��6.    �� Padding$ 5q�6@   ._�5<      �� -   5         ��Name$ PartijTextBox��5<      5=      5>      �� 5?      $��2��6   $��1��6&   .+�5@      �� ���#EAE6E3$
��info��6-   �� 5A      ���#EAE6E3$ 10��6.   $ 5q�6@   ._�5B      �� -   5        $ infotextbox��5B      5C      5D      �� 5E      $��6��6   $��1��6&   .+�5F      �� ���#EAE6E3$
��foto��6-   �� 5G      ���#EAE6E3$ 10��6.   $ 5q�6@   ._�5H      �� -   5        $��
UploadFoto��5H      62   5I      5J      5K      � 5L   
   $��0��6   $��0��6   $��2��6,    5M      �� 5N      6%   �� 5O      $
��0.4* �6#   62   .+�5Q      �� $��1��6   $��0,40,0,0q�6&   .+�5R      �� -   5        $��dashbordknop��5R      $��40��6-   ���#8D99AE$��0,0,0,20q�6N   .��6{   	dashboard6�   5S      5T      � 5U   
   $��0��6   $��1��6   $��2��6,    5V      �� 5W      6"   �� 5X      6"    5Z      �� 5[      6%   �� 5\      6%   �� 5]      $��1* �6#   60   .+�5_      � $��2��6    5`      �� 5a      $��20 �6'   64   �� 5b      6)   �� 5c      $��20 �6'   64   �� 5d      6)   �� 5e      $��20 �6'   64   �� 5f      6)   �� 5g      $��20 �6'   64   �� 5h      6)   .+�5j      �� -   5        $��
toevoegbtn��5j      ���#EAE6E3$��1��6@   .��6i   	toevoegen6t   �� 5k      -   5        $��	wijzigbtn��5k      ���#EAE6E3$��3��6?   .��6i   
wijzigen6s   �� 5l      -   5        $��verwijderbtn��5l      ���#EAE6E3$��5��6B   .��6m   verwijderen6z   �� 5m      -	   5        $��zoekbtn��5m      ���#EAE6E3$��6��6=   $��2��6M   $��20,0,0,0q�6a   .��6�   zoeken6�   5n      5o      5p      5q      