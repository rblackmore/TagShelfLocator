﻿namespace ElectroCom.RFIDTools.UI.Logic.ViewModels;

using CommunityToolkit.Mvvm.Messaging;

using ElectroCom.RFIDTools.ReaderServices;

// TODO: Okay, I should update this to be a full Reader Selection ViewModel.
// Update the view to match as well.
// Display the currently selected Reader Details, included conneciton Status.
public class ReaderManagementVM : ViewModel,
  IReaderManagementVM,
  IRecipient<SelectedReaderChanged>
{

  private readonly IReaderManager readerManager;

  public ReaderManagementVM(IReaderManager readerManager)
  {
    this.IsConnected = false;
    this.DeviceName = string.Empty;
    this.DeviceID = 0;
    this.readerManager = readerManager;
  }

  private bool isConnected;
  public bool IsConnected
  {
    get => isConnected;
    set => SetProperty(ref this.isConnected, value);
  }

  private string deviceName = string.Empty;
  public string DeviceName
  {
    get => deviceName;
    set => SetProperty(ref this.deviceName, value);
  }

  private uint deviceID;

  public uint DeviceID
  {
    get => deviceID;
    set => SetProperty(ref this.deviceID, value);
  }

  public void Receive(SelectedReaderChanged message)
  {
    this.DeviceID = message.DeviceID;
    this.DeviceName = message.DeviceName;
    this.IsConnected = message.IsConnected;
  }
}
