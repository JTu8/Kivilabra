   M S B A M L   `   `   `  ����G  CKiviMassaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:KiviMassaAppKiviMassaApp  5   	   &     KiviMassaApp.SeulaMuokkausIkkuna   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:KiviMassaApp     5   	   -   5          � Title$  Muokkaa seuloja��5   	   $��457.334��6!   $	��300��62    � ShowInTaskbar .   � Topmost .  � WindowStartupLocation$ Manual=�6c   .��5	      � .+�5
   
   �� -   5         ��Name$ lbSeulaLista��5
      $
��Left=�6(   $	��346��6C   $��	36,24,0,0q�6P   $	��Top=�6c   $	��120��6{   �� 5   
   -   5         ��Name$ tbUusiSeula��5      $
��Left=�6'   $��23��6B   $��
170,58,0,0q�6N    �� TextWrapping$
 Wrap=�6b   $	��Top=�6v   $��92��6�   6�   �� 5   
   $��Uusi seula:��6   $
��Left=�6&   $��
183,32,0,0q�6A   $	��Top=�6U   �� 5   
   -   5         ��Name$ btnUusiSeula��5      $��Lisää seula��6'   $
��Left=�6=   $��
187,86,0,0q�6X   $	��Top=�6l   $��75��6�   6�   �� 5   
   -   5        $ btnPoistaSeula��5      $
��Left=�6)   $��187,122,0,0q�6D   $	��Top=�6Y   $��75��6q   $��39��6|   ���	#FF630000���	#FFDA6767.��5      ��  �� TextAlignment$ Center=�5      $��Poista valittu
seula��5      $��71��63   5      �� 5   
   -   5        $ btnTallenna��5      $��Tallenna��6&   $
��Left=�69   $��187,329,0,0q�6T   $	��Top=�6i   $��75��6�   $��41��6�   6�   �� 5   
   -   5        $ btnPalautaDefault��5      $'��!Palauta alkuperäiset seula-arvot��6,   $
��Left=�6W   $��
68,384,0,0q�6r   $	��Top=�6�   $	��194��6�   6�   �� 5   
   -   5        	 ��Name$	 lblTallennusViesti��5      $
��Left=�6,   $��161,217,0,0q�6G   $	��Top=�6\   $	��121��6t   .��5      �� -	   5        
 ��Name$
 lbltallennusViestiMsg��5   4   $ Center=�6   $	��111��6(   64   5      5      5      