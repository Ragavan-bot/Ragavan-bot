﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CLSSContentSchedulerSTT_API.Models;

public partial class MediaTypeHeader
{
    public int MediaTypeHeaderId { get; set; }

    public string MediaTypeHeaderName { get; set; }

    public string MediaTypeHeaderDescription { get; set; }

    public TimeSpan? AverageDuration { get; set; }

    public int? MediaCount { get; set; }

    public int? MediaTypeId { get; set; }

    public int? ActiveStatus { get; set; }

    public string MediaTypeHeaderColor { get; set; }

    public int? ChannelId { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }
}