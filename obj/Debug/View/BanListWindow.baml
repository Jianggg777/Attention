   M S B A M L   `   `   `  ����D  @Attention, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Attention	Attention  5   	       Attention.BanListWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Attention     5   	     � Title$  BanListWindow��5   	   $	��500��6   $	��400��6,    � WindowStartupLocation$ CenterScreen=�68    � 
ResizeMode$ NoResize=�6]    � Topmost . .��5	      � ��5
   
   �� 5       �� ImageSource$ /Image/background9.png��6    j� Stretch$
 Fill=�6>    �� Opacity$	 0.8Q�6M   6^    � RowDefinitions 5   
   �� 5      $
��AUTO �6   �� 5      $��* �6   �� 5      $
��1.5* �6   �� 5      $
��AUTO �6   .+�5   
   �� $��0��6   $��25(�6   
�������.��6>   禁止事項設定6F   �� 5   
   $��1��6   ���	#FF1B2942$��1q�66    �� CornerRadius$ 15��6J   $��10q�6\   ._�5      �� .+�5      �� 	 �� Orientation$	 
Horizontal=�6   .+�5      �� $
��Left=�6   ���	#FF1B2942$��1q�6P   $ 10��6d   $��3q�6v   ._�5      �� $
��Left=�6    .��6;   	Keyword6D   5      �� 5      $��5q�6   ��6(   �� *AddKeywordCommand+.��6N   Add6S   �� 5      $��5q�6   ��6(   �� *DeleteKeywordCommand+.��6Q   Delete6Y   5      �� 5      $��0q�6;   $��90��6O   $	��300��6[   
������ ��6   �� *
Keywords+
 �� SelectedItem
 6g   �� *SelectedKeyword+��5      �� 5      .R�5       �� $��5q�6<   $	��100��6G   ��6&   �� *Name+6U   5!      ��5#      �� 5$      .R�5%      � $	��250��6(   66   5&      5(      5)      5*      �� 5+   
   $��2��6   ���	#FF1B2942$��1q�66   $ 15��6J   $��10q�6\   ._�5,      �� .+�5-      �� $	 
Horizontal=�6   .+�5.      �� $
��Left=�6   ���	#FF1B2942$��1q�6P   $ 10��6d   $��3q�6v   ._�5/      �� $
��Left=�6    .��6;   程式/軟體/應用6E   50      �� 51      $��5q�6   ��6(   �� *AddProcessCommand+.��6N   Add6S   �� 52      $��5q�6   ��6(   �� *DeleteProcessCommand+.��6Q   Delete6Y   53      �� 54      -   5        $��listbox��54      $��0q�6K   $	��160��6_   $	��300��6l   
������ ��6)   �� *	Processes+
 6x   �� *SelectedProcess+��55      �� 56      .R�57      �� $	��250��69    �� 
IsReadOnly . $��4q�6W   ��6&   �� *.+6W   58      5:      5;      5<      �� 5=   
   -   5        $��blw_btn_save��5=   G   $��3��6   $��Center=�6   $��10q�6;   ��6[   �� *SaveBanListCommand+.��6�   儲存6�   5>      5?      