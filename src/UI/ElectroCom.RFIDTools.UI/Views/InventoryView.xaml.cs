﻿namespace ElectroCom.RFIDTools.UI.Views;

using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for InventoryView.xaml
/// </summary>
public partial class InventoryView : UserControl
{
  public InventoryView()
  {
    InitializeComponent();

  }

  private void TiledButton_Click(object sender, System.Windows.RoutedEventArgs e)
  {
    lv_Inventory.Style = (Style)FindResource("TiledLayout");
  }

  private void ListButton_Click(object sender, System.Windows.RoutedEventArgs e)
  {
    lv_Inventory.Style = (Style)FindResource("ListedLayout");
  }
}