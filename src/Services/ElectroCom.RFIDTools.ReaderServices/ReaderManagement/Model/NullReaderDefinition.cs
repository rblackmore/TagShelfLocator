﻿namespace ElectroCom.RFIDTools.ReaderServices;
using FEDM;

public class NullReaderDefinition : ReaderDefinition
{
  public NullReaderDefinition()
  {
    this.CommsInterface = CommsInterface.None;
  }

  public override uint DeviceID => 0;
  public override string DeviceName => "No Reader";
  public override bool IsConnected => false;
}
