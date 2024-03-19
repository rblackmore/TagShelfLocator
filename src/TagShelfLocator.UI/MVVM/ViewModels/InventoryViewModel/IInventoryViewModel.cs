﻿namespace TagShelfLocator.UI.MVVM.ViewModels;

using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;

using TagShelfLocator.UI.MVVM.Modal;

public interface IInventoryViewModel : IViewModel
{
  public bool IsReaderConnected { get; }
  public bool IsReaderDisconnected { get; }
  public ObservableCollection<TagEntry> TagList { get; }
  public bool ClearOnStart { get; set; }
  public IRelayCommand ClearTagList { get; }
  public IAsyncRelayCommand StartInventoryAsync { get; }
  public IAsyncRelayCommand StopInventoryAsync { get; }
  public IRelayCommand OpenSettings { get; }
}