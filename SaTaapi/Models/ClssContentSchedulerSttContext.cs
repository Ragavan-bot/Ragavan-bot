﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CLSSContentSchedulerSTT_API.Models;

public partial class ClssContentSchedulerSTTContext : DbContext
{
    public ClssContentSchedulerSTTContext(DbContextOptions<ClssContentSchedulerSTTContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<ChannelGroup> ChannelGroups { get; set; }

    public virtual DbSet<ChannelsConfiguration> ChannelsConfigurations { get; set; }

    public virtual DbSet<CommercialSchedule> CommercialSchedules { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<HotClipDtl> HotClipDtls { get; set; }

    public virtual DbSet<HotClipHdr> HotClipHdrs { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<MediaMaster> MediaMasters { get; set; }

    public virtual DbSet<MediaTcinout> MediaTcinouts { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<MediaTypeDetail> MediaTypeDetails { get; set; }

    public virtual DbSet<MediaTypeHeader> MediaTypeHeaders { get; set; }

    public virtual DbSet<MusicLabel> MusicLabels { get; set; }

    public virtual DbSet<PlayoutDtl> PlayoutDtls { get; set; }

    public virtual DbSet<PlayoutHdr> PlayoutHdrs { get; set; }

    public virtual DbSet<ProgramSchedule> ProgramSchedules { get; set; }

    public virtual DbSet<ProgramTemplateDetail> ProgramTemplateDetails { get; set; }

    public virtual DbSet<ProgramTemplateHeader> ProgramTemplateHeaders { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<SecondaryEvent> SecondaryEvents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserChannelMap> UserChannelMaps { get; set; }

    public virtual DbSet<UserNote> UserNotes { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.UniqueId);

            entity.Property(e => e.UniqueId).HasColumnName("UniqueID");
            entity.Property(e => e.EndDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.ResourceIds).HasColumnName("ResourceIDs");
            entity.Property(e => e.StartDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Subject).HasMaxLength(50);
        });

        modelBuilder.Entity<Channel>(entity =>
        {
            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.ChangedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.ChannelCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChannelDescription).HasMaxLength(50);
            entity.Property(e => e.ChannelName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LicenseCode).HasColumnType("text");
        });

        modelBuilder.Entity<ChannelGroup>(entity =>
        {
            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.ChannelGroupName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.ChannelGroups)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_ChannelGroups_Company");
        });

        modelBuilder.Entity<ChannelsConfiguration>(entity =>
        {
            entity.HasKey(e => e.ChannelConfigId);

            entity.ToTable("ChannelsConfiguration");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Frames)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Scte)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SCTE");

            entity.HasOne(d => d.Channel).WithMany(p => p.ChannelsConfigurations)
                .HasForeignKey(d => d.ChannelId)
                .HasConstraintName("FK_ChannelsConfiguration_Channels");
        });

        modelBuilder.Entity<CommercialSchedule>(entity =>
        {
            entity.ToTable("CommercialSchedule");

            entity.Property(e => e.AdvertisementId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AdvertisementName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BrandId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CommercialMaterialId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CommercialMaterialName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReconciliationId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TelecastDate).HasColumnType("date");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorId);

            entity.Property(e => e.ErrorDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ErrorDescription).HasColumnType("text");
            entity.Property(e => e.ErrorLocateAt)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HotClipDtl>(entity =>
        {
            entity.ToTable("HotClipDtl");
        });

        modelBuilder.Entity<HotClipHdr>(entity =>
        {
            entity.ToTable("HotClipHdr");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.ChangedBy)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HotClipDescription)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.HotClipNameTitle)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.LanguageDescription)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.LanguageName)
                .HasMaxLength(30)
                .IsFixedLength();
        });

        modelBuilder.Entity<MediaMaster>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK_Media");

            entity.ToTable("MediaMaster");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.AlbumName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.ChangedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.ChangedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.EndDateStatus)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Gain)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GenreInfo)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Keyer)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Lyrics).HasColumnType("text");
            entity.Property(e => e.LyricsInfo)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.LyricsLanguage)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MediaCaption)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.MediaGenre)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MediaImages).HasColumnType("image");
            entity.Property(e => e.MediaLocation).HasColumnType("text");
            entity.Property(e => e.MediaNameTitle)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.MediaSubGenre)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Mood)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Popularity)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Remarks)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.SoundFeel)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Templates)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Tempo)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TempoInfo)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.VideoStatus)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.MediaType).WithMany(p => p.MediaMasters)
                .HasForeignKey(d => d.MediaTypeId)
                .HasConstraintName("FK_MediaMaster_MediaType");
        });

        modelBuilder.Entity<MediaTcinout>(entity =>
        {
            entity.HasKey(e => e.UniqueId);

            entity.ToTable("MediaTCINOUT");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Tcin).HasColumnName("TCIN");
            entity.Property(e => e.Tcout).HasColumnName("TCOUT");

            entity.HasOne(d => d.Media).WithMany(p => p.MediaTcinouts)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK_MediaTCINOUT_MediaMaster");
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.ToTable("MediaType");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MediaTypeCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MediaTypeColor)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MediaTypeDescription)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<MediaTypeDetail>(entity =>
        {
            entity.ToTable("MediaTypeDetail");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MediaTypeDetailColor)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.MediaTypeDetailDescription)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.MediaTypeDetailName)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<MediaTypeHeader>(entity =>
        {
            entity.ToTable("MediaTypeHeader");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MediaTypeHeaderColor)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.MediaTypeHeaderDescription)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.MediaTypeHeaderName)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<MusicLabel>(entity =>
        {
            entity.ToTable("MusicLabel");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.ChangedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.ChangedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.MusicLabel1)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MusicLabel");
            entity.Property(e => e.MusicLabelDescription)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PlayoutDtl>(entity =>
        {
            entity.HasKey(e => e.OnairId);

            entity.ToTable("PlayoutDtl");

            entity.Property(e => e.Eom).HasColumnName("EOM");
            entity.Property(e => e.Marker)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MediaName)
                .HasMaxLength(300)
                .IsFixedLength();
            entity.Property(e => e.MediaType)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.OnairStatus)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.ProgramName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Remarks).HasColumnType("text");
            entity.Property(e => e.Som).HasColumnName("SOM");

            entity.HasOne(d => d.PlayoutHdr).WithMany(p => p.PlayoutDtls)
                .HasForeignKey(d => d.PlayoutHdrId)
                .HasConstraintName("FK_PlayoutDtl_PlayoutHdr");
        });

        modelBuilder.Entity<PlayoutHdr>(entity =>
        {
            entity.ToTable("PlayoutHdr");

            entity.Property(e => e.ChangedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PlayoutDate).HasColumnType("date");
        });

        modelBuilder.Entity<ProgramSchedule>(entity =>
        {
            entity.ToTable("ProgramSchedule");

            entity.Property(e => e.BreakRelativityName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProgramName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.ShootId)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.ShootName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.TelecastDate).HasColumnType("date");
        });

        modelBuilder.Entity<ProgramTemplateDetail>(entity =>
        {
            entity.ToTable("ProgramTemplateDetail");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MediaName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.ProgramTemplateDetailName)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<ProgramTemplateHeader>(entity =>
        {
            entity.ToTable("ProgramTemplateHeader");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProgramTemplateHeaderDescriptino)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.ProgramTemplateHeaderName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.SingleTrack).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.UniqueId);

            entity.Property(e => e.UniqueId).HasColumnName("UniqueID");
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.ResourceName).HasMaxLength(50);
        });

        modelBuilder.Entity<SecondaryEvent>(entity =>
        {
            entity.ToTable("SecondaryEvent");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SecondaryEventName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.SecondaryEventType)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Users_Company");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK_Users_UserType");
        });

        modelBuilder.Entity<UserChannelMap>(entity =>
        {
            entity.HasKey(e => new { e.UsermapId, e.UserId, e.ChannelId });

            entity.ToTable("UserChannelMap");

            entity.Property(e => e.UsermapId).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<UserNote>(entity =>
        {
            entity.HasKey(e => e.UserNotesId);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasColumnType("text");

            entity.HasOne(d => d.User).WithMany(p => p.UserNotes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserNotes_Users");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");

            entity.Property(e => e.ActiveStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserType1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UserType");
        });

        OnModelCreatingGeneratedProcedures(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}