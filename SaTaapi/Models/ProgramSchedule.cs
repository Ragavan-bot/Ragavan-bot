﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CLSSContentSchedulerSTT_API.Models;

public partial class ProgramSchedule
{
    public int ProgramScheduleId { get; set; }

    public int? ChannelId { get; set; }

    public DateTime? TelecastDate { get; set; }

    public int? ProgramId { get; set; }

    public string ProgramName { get; set; }

    public int? BreakRelativityId { get; set; }

    public string BreakRelativityName { get; set; }

    public TimeSpan? ProgramStartTime { get; set; }

    public TimeSpan? ProgramEndTime { get; set; }

    public TimeSpan? BreakStartTime { get; set; }

    public TimeSpan? BreakEndTime { get; set; }

    public string ShootId { get; set; }

    public string ShootName { get; set; }

    public TimeSpan? ShootDuration { get; set; }

    public DateTime? CreatedOn { get; set; }
}