   M S B A M L   `   `   `  ����H  DICTProjectPOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ICTProjectPOSICTProjectPOS  5           ICTProjectPOS.Form_Payment   5      %localclr-namespace:ICTProjectPOS  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ���#518f65$	��540��5      $	��835��6   .��5	      �� .+�5   
   ��   �� Left$  30��5      
 �� Top$	 100��6   $	��480��5      $��12��6   
�������5      �� 5   
   $  30��5       �� Bottom$	 120��6   $	��480��5      $��12��6   
�������5      �� 5   
   $  30��5      $ 30��6   $	��480��5      $��12��6   
�������5      �� 5   
   $
��Bold"�5      $��48(�5      $��60��6   $  30��5      $ 20��6   
�������	PAYMENT5      �� 5   
   -   5         ��Name$ Button_CloseTableForm��5       �� Right$ 30��5       $ 35��6      CloseButtonStyle#��[  5!      .��5#      CLOSE6   �� 5%   
   $
��Bold"�5&      $��14(�5'      $  30��5(      $	 125��6   
�������PAYMENT DETAILS5*      �� 5,   
   -   5        $ Button_PaymentMethod��5-      $  30��5.      $	 150��6   $	��480��5/      $��35��6     PaymentMethodButtonStyle#��[ 50      .��52      �� $	��480��53      $��35��6   .+�54      �� -   5         ��Name$ TextBlock_PaymentMethod��56      $ 8��55      $  10��6$   $��SELECT PAYMENT METHOD��57       J� IsHitTestVisible .  62   � 58      -   5         �Name$  Polygon_DropDown_Indicator��59      $ 12��5:      $ 10��6%   $��20��5;       � Points �    $�  z  � ���#363636 .  5=   $   5>      5?      �� 5@   
   -   5        $"��StackPanel_PaymentMethodList��5A      $  30��5B      $	 185��6   ���#eeeeee$y�Hidden=�5D      $	��230��5E      $	��120��5F      .+�5G      �� -   5        $��Button_Cash��5G      $
��CASH��6(      PaymentMethodListButtonStyle#��[ 67   6m   �� 5H      -   5        $��Button_CreditCard��5H      $��CREDIT CARD��6.   #��[ 6D   6z   �� 5I      -   5        $��Button_DebitCard��5I      $��
DEBIT CARD��6-   #��[ 6B   6x   �� 5J      -	   5        $��Button_Nets��5J      $
��NETS��6(   #��[ 67   6m   5K      �� 5M   
   $��14(�5N      $
��Bold"�5O      $��AMOUNT DUE :��5P      $	 150��5Q      $	 205��6    
�������$��Right=�5S      �� 5T   
   -
   5        $ TextBlock_DueNum��5U      $��14(�5V      $
��Bold"�5W      $
��0.00��5X      
�������$ 45��5Z      $	 205��6   $��Right=�5[      �� 5\   
   -   5        $��TextBlock_PayWith��5]      $��14(�5^      $
��Bold"�5_      $��(PAY WITH) :��5`      
�������$	 150��5b      $	 235��6    $��Right=�5c      �� 5d   
   $ 30��5e      $	 230��6   ���#cccccc$��30��5g      $	��100��6   �� 5i   
   -   5        	 ��Name$	 Rectangle_Change��5j      $ 30��5k      $	 305��6   ���#cccccc$��30��5m      $	��200��6   �� 5o   
   -   5        $ TextBlock_PayNum��5p      $��14(�5q      $
��Bold"�5r      $
��0.00��5s      ���#363636$ 45��5u      $	 235��6   $��Right=�5v      �� 5w   
   -   5        $ TextBlock_Change��5x      $��14(�5y      $
��Bold"�5z      $��CHANGE :��5{      ���#363636$	 150��5}      $	 310��6    $��Right=�5~      �� 5   
   -   5        $ TextBlock_ChangeNum��5�      $��14(�5�      $
��Bold"�5�      $
��0.00��5�      ���#363636$ 45��5�      $	 310��6   $��Right=�5�         ICTProjectPOS.NumPad_Cash  5�   
   -   5        
  Name$
 ContentControl_NumPad��5�      $	��480��5�      $	��380��6   $  30��5�      $	 305��6   �� 5�   
   $
��Bold"�5�      $��14(�5�      $  30��5�      $ 90��6   
�������PRINT RECEIPT5�      �� 5�   
   -   5        $ TextBlock_Error��5�      $��14(�5�      $  30��5�      $ 60��6   
�������-+ERROR: AMOUNT-PAID IS LOWER THAN AMOUNT-DUE5�   ;   �� 5�   
   -   5        $ Button_Confirm��5�      $ 30��5�      $ 55��6     WhiteButtonStyle#��[ 5�      .��5�      	CONFIRM6   5�      5�      