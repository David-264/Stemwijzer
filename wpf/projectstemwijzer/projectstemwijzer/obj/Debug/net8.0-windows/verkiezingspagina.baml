   M S B A M L   `   `   `  ����L  Hprojectstemwijzer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:projectstemwijzerprojectstemwijzer  5   	   )    #projectstemwijzer.verkiezingspagina   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:projectstemwijzer     5   	     � Icon$$  /fotos/neutraalkieslabfoto.ico��5   	   ���#352D27$	��300��5
   	   $	��500��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ verkiezingspagina��5   	   $	��450��6#   $	��800��60   .��5      �  � RowDefinitions 5   
   �� 5      6   �� 5      $��3* �6   6*   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      $��3* �6   6,   �� 5      $��2* �6   6,   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��verkiezingsveld��5   u   $��1��6      
IsReadOnly$
 True��6    $��1��62   ���#EAE6E3   AutoGenerateColumns$ False��6Y      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6%   	   Header$	 Titel��62   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6A   �� *Titel+  5      $
 auto 6%   $	 Beschrijving��62   
 6H   �� *Beschrijving+  5      $
 auto 6%   $	 Start_datum��62   
 6G   �� *Start_datum+  5      $
 auto 6%   $	 
Eind_datum��62   
 6F   �� *
Eind_datum+  5       $
 auto 6%   $	 Aanmaakdatum��62   
 6H   �� *Aanmaakdatum+5&      � 5'   
   $��1��6   $��2��6   $��1��6,   $��2��6@    5(      �� 5)      $��1* �6    6.   �� 5*      $
��0.3* �6    60   �� 5+      $��1* �6    6.   �� 5,      $
��0.3* �6    60   �� 5-      $��1* �6    6.   �� 5.      $
��0.3* �6    60   �� 5/      $��1* �6    6.   �� 50      $
��0.3* �6    60    52      �� 53      6%   �� 54      $��2* �6#   60   .+�56      �� $��0��6   $��1��6&   .+�57      �� ���#EAE6E3$��titel��6-   �� 58      ���#EAE6E3 �� CornerRadius$ 10��6.    �� Padding$ 5q�6@   ._�59      �� -   5         ��Name$ titelbox��59      5:      5;      �� 5<      $��2��6   $��1��6&   .+�5=      �� ���#EAE6E3$��beschrijving��6-   �� 5>      ���#EAE6E3$ 10��6.   $ 5q�6@   ._�5?      �� -   5        $ beschrijvingbox��5?      5@      5A      �� 5B      $��4��6   $��1��6&   .+�5C      �� ���#EAE6E3$��start-datum��6-   �� 5D      ���#EAE6E3$ 10��6.   $ 5q�6@   ._�5E      (  "System.Windows.Controls.DatePicker  -   5        $��
startdatum��5E   !   65   5F      5G      �� 5H      $��6��6   $��1��6&   .+�5I      �� ���#EAE6E3$��
eind-datum��6-   �� 5J      ���#EAE6E3$ 10��6.   $ 5q�6@   ._�5K        -   5        $��	einddatum��5K   !   64   5L      5M      5N      � 5O   
   $��0��6   $��0��6   $��2��6,    5P      �� 5Q      6%   �� 5R      $
��0.4* �6#   62   .+�5T      �� $��1��6   $��0,40,0,0q�6&   .+�5U      �� -   5        $��dashbordknop��5U      $��40��6-   ���#8D99AE$��0,0,0,20q�6N   .��6{   	dashboard6�   5V      5W      � 5X   
   $��0��6   $��1��6   $��2��6,    5Y      �� 5Z      6"   �� 5[      6"    5]      �� 5^      6%   �� 5_      6%   �� 5`      $��1* �6#   60   .+�5b      � $��2��6    5c      �� 5d      $��20 �6'   64   �� 5e      6)   �� 5f      $��20 �6'   64   �� 5g      6)   �� 5h      $��20 �6'   64   �� 5i      6)   �� 5j      $��20 �6'   64   �� 5k      6)   .+�5m      �� -   5        $��
toevoegbtn��5m      ���#EAE6E3$��1��6@   .��6i   	toevoegen6t   �� 5n      -   5        $��	wijzigbtn��5n      ���#EAE6E3$��3��6?   .��6i   
wijzigen6s   �� 5o      -	   5        $��verwijderbtn��5o      ���#EAE6E3$��5��6B   .��6m   verwijderen6z   �� 5p      -
   5        $��zoekbtn��5p      ���#EAE6E3$��6��6=   $��2��6M   $��20,0,0,0q�6a   .��6�   zoeken6�   5q      5r      5s      5t      