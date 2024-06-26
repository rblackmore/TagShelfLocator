﻿namespace OBID.Scratch;

using System.Collections.Generic;

using MediatR;

using Microsoft.Extensions.Logging;

using OBID.Scratch.ReaderManagement;
using OBID.Scratch.ReaderManagement.Model;

public class ReaderManager : IReaderManager
{
  private List<ReaderDefinition> readerDefinitions;

  private int selectedIdx;
  private readonly ILogger<ReaderManager> logger;
  private readonly IMediator mediator;

  public ReaderManager(ILogger<ReaderManager> logger, IMediator mediator)
  {
    this.readerDefinitions = new List<ReaderDefinition>();
    this.logger = logger;
    this.mediator = mediator;
  }

  public ReaderDefinition SelectedReader =>
    this.readerDefinitions[selectedIdx];

  public IReadOnlyCollection<ReaderDefinition> GetReaderDefinitions()
  {
    return this.readerDefinitions.AsReadOnly();
  }

  public void RegisterReader(ReaderDefinition readerDefinition)
  {
    if (!readerDefinition.IsValid())
      return;

    this.readerDefinitions.Add(readerDefinition);

    this.selectedIdx =
      EnsureSelectedIndexIsWithinBounds(this.selectedIdx);
  }

  public bool UnregisterSelectedReader()
  {
    var rdToRemove = this.SelectedReader;

    return UnregisterReader(rdToRemove);
  }

  public bool UnregisterReader(uint deviceId)
  {
    var rdToRemove =
      this.readerDefinitions.FirstOrDefault(rd => rd.DeviceID == deviceId);

    return UnregisterReader(rdToRemove);
  }

  public bool UnregisterReader(ReaderDefinition? rdToRemove)
  {
    var isRemoved = this.readerDefinitions.Remove(rdToRemove!);

    this.selectedIdx =
      EnsureSelectedIndexIsWithinBounds(this.selectedIdx);

    return isRemoved;
  }

  public bool SelectReader(int idx)
  {
    int newIdx = EnsureSelectedIndexIsWithinBounds(idx);

    if (this.selectedIdx == newIdx)
      return false;

    this.mediator.Publish(new SelectedReaderChanged(SelectedReader));

    return true;
  }

  public bool SelectReader(uint deviceId)
  {
    var rd =
      this.readerDefinitions.FirstOrDefault(r => r.DeviceID == deviceId);

    return SelectReader(rd);
  }

  public bool SelectReader(ReaderDefinition? rd)
  {
    if (rd is null)
      return false;

    var idx = this.readerDefinitions.IndexOf(rd);

    return SelectReader(idx);
  }

  private int EnsureSelectedIndexIsWithinBounds(int idx)
  {
    if (this.readerDefinitions.Count <= 0)
      return -1;

    if (this.readerDefinitions.Count == 1)
      return 0;

    if (selectedIdx >= this.readerDefinitions.Count)
      return this.readerDefinitions.Count - 1;

    return idx;
  }
}
