   M S B A M L   `   `   `  ����L  HConwaysGameOfLife, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:ConwaysGameOfLife.nUserControlsConwaysGameOfLife.nUserControls  5   	   G(clr-namespace:ConwaysGameOfLife.nClassesConwaysGameOfLife.nClasses  5   	   "    ConwaysGameOfLife.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   GnUserControls-clr-namespace:ConwaysGameOfLife.nUserControls     5   	   =nClasses(clr-namespace:ConwaysGameOfLife.nClasses     5   	   -   5          � Title$  Conway's Game of Life��5   	   $	��720��6'   $
��1024��64    � WindowState$ 	Maximized=�5   	    � 
ResizeMode$ CanMinimize=�6!   .��5	      �  � 	Resources 5
   
    �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell �� Triggers �� Binding	 �� Path
 �� Value��%�   '��       '  R     �� 5      " ��6   .�5      ��  � ��$ 5,2,5,2q�6+   6=   5      �� 5      "  6    5      �� 5       6"   �� $	 IsNew�
 True6A   .a�5      ��  � ��
 �  ��67   5      5      5       � ColumnDefinitions 5   
   �� 5      -   5         ��Name$ ControlColumn��5      $	��310 �66   �� 5      .+�5   
   �� 
�������$
��Auto��6%   $
��Auto��62   �� 5   
   $��0��6   .+�5      1   +ConwaysGameOfLife.nUserControls.GridControl  -   5          Name$ GControl��5   (   1   +ConwaysGameOfLife.nUserControls.PlayControl  5      -   5          Name$ PControl��5   (   �� 5      -   5         ��Name$ ColorControlCheckbox��5      .��65   Show color controls6J   2   ,ConwaysGameOfLife.nUserControls.ColorControl  5       -   5          Name$ CControl��5    )   y�6;   ��  �� ElementName$ ColorControlCheckbox��$	 	IsChecked�   visConv �� 	Converter# [  �� 5!      -   5        $ RulesControlCheckbox��5!      .��65   Show rules controls6J   1   +ConwaysGameOfLife.nUserControls.RuleControl  5"      -   5          Name$ RControl��5"   (   y�6:   �� $ RulesControlCheckbox��$	 	IsChecked�# [  �� 5#      -	   5        $ GenControlCheckbox��5#      .��63   Show Generator controls6L   6   0ConwaysGameOfLife.nUserControls.GeneratorControl  5$      -
   5          Name$ 
GenControl��5$   -   y�6A   �� $ GenControlCheckbox��$	 	IsChecked�# [  �� 5%      -   5        $ KeyBindingsCheckbox��5%      .��64   Show Keybindings6F   1   +ConwaysGameOfLife.nUserControls.KeyBindings  5&      -   5          Name$ KeyBindingsControl��5&   (   y�6D   �� $ KeyBindingsCheckbox��$	 	IsChecked�# [  6�   �� 5'      -   5        $��
10,10,10,0q�5'      .��6D   Clear6K   �� 5(      -   5        $��
10,10,10,0q�5(      .��6E   Random6M   5)      A� 5*   
   -   5         A�Name$ UniGrid��5*      $��1��6'   69   5+      5,      