   M S B A M L   `   `   `  ����D  @Attention, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Attention	Attention  5   	       Attention.RunWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Attention     5   	   -   5          � Title$  
MainWindow��5   	   $	��400��6   $	��600��6(    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ NoResize=�6Z   .��5	      �� ��5
   
   �� 5       �� ImageSource$ /Image/bg.jpg��6    �� Opacity$	 0.8Q�65   .+�5   
   �� $��30(�6   $��Font/#Ice kingdom)�6   
�������$
��Bold"�6T   $��Right=�6f   .��6�   A T T E N T I O N !6�   �� 5   
   $��Center=�6   ���	#FF1B2942$��2q�6F    �� CornerRadius$ 10��6Z   
�������._�5      �� $��20(�62   ��6   �� *	TodayDate+62   5      �� 5   
   -   5        $��list1��5   x   $��0,5,0,0q�65   $	��255��6F   $	��380��6R   
������ $��Center=�6�   $��0q�6�   ��6   �� *
TodayPlans+��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��0��6%   $ 
Horizontal=�62   $��5q�6K   .+�5      �� $��15(�6(   $
��Bold"�66   ��6H   �� *	StartTime+6f   �� 5      $��15(�6(   $
��Bold"�66   $
��:00~��6H   6W   �� 5      $��15(�6(   $
��Bold"�66   ��6H   �� *	EndTime+6d   �� 5      $��15(�6(   $
��Bold"�66   $	��:00��6H   6U   5      �� 5      $��5q�6"   $	��禁��6-   
������ 
����p��
���� K���69   �� *!DataContext.ShowBanItemsCommand+ �� ElementName$ list1����6�   �� *Ban+6�   �� 5      $��5q�6$   ��6/   �� *Tip+6F   5      5      5       5!      5"      