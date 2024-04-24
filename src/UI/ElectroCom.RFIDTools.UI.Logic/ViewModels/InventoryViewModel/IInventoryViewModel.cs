﻿namespace ElectroCom.RFIDTools.UI.Logic.ViewModels;

using CommunityToolkit.Mvvm.Input;

public interface IInventoryViewModel : IViewModel
{
  public bool IsReaderConnected { get; }
  public bool IsReaderDisconnected { get; }
  public ObservableTagEntryCollection TagList { get; }
  public bool ClearOnStart { get; set; }
  public IRelayCommand ClearTagList { get; }
  public IAsyncRelayCommand StartInventoryAsync { get; }
  public IAsyncRelayCommand StopInventoryAsync { get; }
  public IRelayCommand OpenSettings { get; }
}