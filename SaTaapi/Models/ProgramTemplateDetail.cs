﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CLSSContentSchedulerSTT_API.Models;

public partial class ProgramTemplateDetail
{
    public int ProgramTemplateDetailId { get; set; }

    public string ProgramTemplateDetailName { get; set; }

    public int? MediaId { get; set; }

    public string MediaName { get; set; }

    public TimeSpan? MediaDuration { get; set; }

    public int? MediaTypeId { get; set; }

    public int? OrderNumber { get; set; }

    public TimeSpan? TemplateStartTime { get; set; }

    public TimeSpan? TemplateEndTime { get; set; }

    public int? ProgramTemplateHeaderId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }
}