   M S B A M L   `   `   `  ���� D  @Attention, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Attention	Attention      Attention.RunWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:Attention     -     � Title$  
MainWindow��$	��400��$	��600�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�.���� ����  �� ImageSource$ /Image/bg.jpg�� �� Opacity$	 0.8Q�.+��� $��30(�$��Font/#Ice kingdom)�
�������$
��Bold"�$��Right=�.��A T T E N T I O N !�� $��Center=����	#FF1B2942$��2q� �� CornerRadius$ 10��
�������._��� $��20(����� *	TodayDate+�� -   $��list1��$��0,5,0,0q�$	��255��$	��380��
������ $��Center=�$��0q����� *
TodayPlans+���� .R���  �� Orientation$ 
Horizontal=�.+��� $��0��$ 
Horizontal=�$��5q�.+��� $��15(�$
��Bold"����� *	StartTime+�� $��15(�$
��Bold"�$
��:00~���� $��15(�$
��Bold"����� *	EndTime+�� $��15(�$
��Bold"�$	��:00���� $��5q�$	��禁��
������ 
����p��
���� K����� *!DataContext.ShowBanItemsCommand+ �� ElementName$ list1������ *Ban+�� $��5q����� *Tip+