   M S B A M L   `   `   `  ���� D  @Attention, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Attention	Attention      Attention.BanListWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:Attention       � Title$  BanListWindow��$	��500��$	��400�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Topmost . .��� ����  �� ImageSource$ /Image/background9.png�� j� Stretch$
 Fill=� �� Opacity$	 0.8Q� � RowDefinitions �� $
��AUTO ��� $��* ��� $
��1.5* ��� $
��AUTO �.+��� $��0��$��25(�
�������.��禁止事項設定�� $��1�����	#FF1B2942$��1q� �� CornerRadius$ 15��$��10q�._��� .+��� 	 �� Orientation$	 
Horizontal=�.+��� $
��Left=����	#FF1B2942$��1q�$ 10��$��3q�._��� $
��Left=�.��	Keyword�� $��5q����� *AddKeywordCommand+.��Add�� $��5q����� *DeleteKeywordCommand+.��Delete�� $��0q�$��90��$	��300��
������ ���� *
Keywords+
 �� SelectedItem
 �� *SelectedKeyword+���� .R��� $��5q�$	��100������ *Name+���� .R�� $	��250���� $��2�����	#FF1B2942$��1q�$ 15��$��10q�._��� .+��� $	 
Horizontal=�.+��� $
��Left=����	#FF1B2942$��1q�$ 10��$��3q�._��� $
��Left=�.��程式/軟體/應用�� $��5q����� *AddProcessCommand+.��Add�� $��5q����� *DeleteProcessCommand+.��Delete�� -   $��listbox��$��0q�$	��160��$	��300��
������ ���� *	Processes+
 �� *SelectedProcess+���� .R��� $	��250�� �� 
IsReadOnly . $��4q����� *.+�� -   $��blw_btn_save��$��3��$��Center=�$��10q����� *SaveBanListCommand+.��儲存