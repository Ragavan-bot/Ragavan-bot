﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CLSSContentSchedulerSTT_API.Models;

public partial class SecondaryEvent
{
    public int SecondaryEventId { get; set; }

    public string SecondaryEventName { get; set; }

    public string SecondaryEventType { get; set; }

    public int? ActiveStatus { get; set; }

    public int? ChannelId { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }
}