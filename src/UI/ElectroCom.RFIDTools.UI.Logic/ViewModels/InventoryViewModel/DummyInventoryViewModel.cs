﻿namespace ElectroCom.RFIDTools.UI.Logic.ViewModels;

using CommunityToolkit.Mvvm.Input;

public class DummyInventoryViewModel : ViewModel, IInventoryViewModel
{
  public ObservableTagEntryCollection TagList
  {
    get
    {
      return null!;
    }
  }

  public ObservableTagEntryCollection TagListData => throw new System.NotImplementedException();

  public bool ClearOnStart { get; set; } = true;

  public IRelayCommand ClearTagList => throw new System.NotImplementedException();

  public IAsyncRelayCommand StartInventoryAsync => throw new System.NotImplementedException();

  public IAsyncRelayCommand StopInventoryAsync => throw new System.NotImplementedException();

  public IRelayCommand OpenSettings => throw new System.NotImplementedException();

  public bool IsReaderConnected => true;

  public bool IsReaderDisconnected => false;

  public IRelayCommand AddTagEntry => throw new System.NotImplementedException();

}