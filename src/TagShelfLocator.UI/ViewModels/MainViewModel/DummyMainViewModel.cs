﻿namespace TagShelfLocator.UI.ViewModels;

using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using TagShelfLocator.UI.Model;

public class DummyMainViewModel : ObservableObject, IMainViewModel
{
  public ObservableCollection<ObservableTagDetails> TagList
  {
    get
    {
      return new ObservableCollection<ObservableTagDetails>(
        new[] {
        new DummyTagDetails(),
        new DummyTagDetails(),
        new DummyTagDetails(),
        });
    }
  }

  public IAsyncRelayCommand StartInventoryAsync => throw new System.NotImplementedException();

  public IAsyncRelayCommand StopInventoryAsync => throw new System.NotImplementedException();

  public bool IsReaderConnected => true;

  public bool IsReaderDisconnected => false;
}
