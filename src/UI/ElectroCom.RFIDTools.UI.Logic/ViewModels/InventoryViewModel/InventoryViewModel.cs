﻿namespace ElectroCom.RFIDTools.UI.Logic.ViewModels;

using System;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using ElectroCom.RFIDTools.ReaderServices.InventoryService;

using Microsoft.Extensions.Logging;

public class InventoryViewModel : ViewModel,
  IInventoryViewModel,
  IDisposable
{
  private readonly ILogger<InventoryViewModel> logger;

  private readonly ITagReadingService tagInventoryService;
  private readonly INavigationService navigationService;

  private bool isReaderConnected;
  private bool clearOnStart;

  private Task readTask = Task.CompletedTask;

  public InventoryViewModel(
    ILogger<InventoryViewModel> logger,
    ITagReadingService tagInventoryService,
    INavigationService navigationService)
  {
    ClearOnStart = true;
    this.logger = logger;
    this.tagInventoryService = tagInventoryService;
    this.navigationService = navigationService;
    TagList = new();


    ClearTagList =
      new RelayCommand(ClearTagListExecute);

    OpenSettings =
      new RelayCommand(NavigateToSettingsMenuExecute);

    StartInventoryAsync =
      new AsyncRelayCommand(
        StartInventoryExecuteAsync,
        StartInventoryCanExecute);

    StopInventoryAsync =
      new AsyncRelayCommand(
        StopInventoryExecuteAsync,
        StopInventoryCanExecute);
  }

  public bool IsReaderConnected
  {
    get => isReaderConnected;
    private set
    {
      OnPropertyChanging(nameof(IsReaderDisconnected));
      SetProperty(ref isReaderConnected, value);
      OnPropertyChanged(nameof(IsReaderDisconnected));
      OnInventoryTaskCanExecuteChanged();
    }
  }

  public bool IsReaderDisconnected => !IsReaderConnected;

  public ObservableTagEntryCollection TagList { get; }

  public bool ClearOnStart
  {
    get => clearOnStart;
    set => SetProperty(ref clearOnStart, value);
  }
  public IRelayCommand ClearTagList { get; private set; }
  public IAsyncRelayCommand StartInventoryAsync { get; private set; }
  public IAsyncRelayCommand StopInventoryAsync { get; private set; }
  public IRelayCommand OpenSettings { get; private set; }

  private void ClearTagListExecute()
  {
    if (TagList.Any())
      TagList.Clear();
  }

  private void NavigateToSettingsMenuExecute()
  {
    navigationService.NavigateTo<ISettingsViewModel>();
  }

  private async Task StartInventoryExecuteAsync()
  {
    if (ClearOnStart)
      ClearTagListExecute();

    await tagInventoryService.StartAsync();
  }

  private async Task StopInventoryExecuteAsync()
  {
    await tagInventoryService.StopAsync();
  }

  private async Task CancelInventoryChannelReaderAsync()
  {
    if (readTask is null)
      return;

    await readTask;
  }

  private bool StartInventoryCanExecute()
  {
    return IsReaderConnected && tagInventoryService.IsNotRunning;
  }

  private bool StopInventoryCanExecute()
  {
    return tagInventoryService.IsRunning;
  }

  //public async void Receive(ReaderConnected message)
  //{
  //  IsReaderConnected = true;
  //}

  //public async void Receive(ReaderDisconnected message)
  //{
  //  IsReaderConnected = false;
  //  await CancelInventoryChannelReaderAsync();
  //  OnInventoryTaskCanExecuteChanged();
  //}

  //public void Receive(InventoryStartedMessage message)
  //{
  //  OnInventoryTaskCanExecuteChanged();
  //}

  //public async void Receive(InventoryStoppedMessage message)
  //{
  //  await CancelInventoryChannelReaderAsync();
  //  OnInventoryTaskCanExecuteChanged();
  //}

  //public void Receive(InventoryTagItemsDetectedMessage message)
  //{
  //  DispatcherHelper.CheckBeginInvokeOnUI(() =>
  //  {
  //    foreach (var tag in message.Tags)
  //    {
  //      TagList.Add(tag);
  //    }
  //  });
  //}

  private void OnInventoryTaskCanExecuteChanged()
  {
    DispatcherHelper.CheckBeginInvokeOnUI(() =>
    {
      StartInventoryAsync.NotifyCanExecuteChanged();
      StopInventoryAsync.NotifyCanExecuteChanged();
    });
  }

  public void Dispose()
  {
    //messenger.UnregisterAll(this);
  }
}