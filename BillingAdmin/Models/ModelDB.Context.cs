﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillingAdmin.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C100tx_Orders> C100tx_Orders { get; set; }
        public virtual DbSet<C100tx_OrdersStatuses> C100tx_OrdersStatuses { get; set; }
        public virtual DbSet<C100tx_OrdersStatusHistory> C100tx_OrdersStatusHistory { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Accounts_Roles> Accounts_Roles { get; set; }
        public virtual DbSet<Accounts_RolesHB> Accounts_RolesHB { get; set; }
        public virtual DbSet<Accounts_Users> Accounts_Users { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRolesMenus> AspNetRolesMenus { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Logs_Messages> Logs_Messages { get; set; }
        public virtual DbSet<Logs_Types> Logs_Types { get; set; }
        public virtual DbSet<Price_TypesHB> Price_TypesHB { get; set; }
        public virtual DbSet<PrjPreferences> PrjPreferences { get; set; }
        public virtual DbSet<PrjSettings_Data> PrjSettings_Data { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Projects_Services> Projects_Services { get; set; }
        public virtual DbSet<ProjectTypes> ProjectTypes { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Services_Data> Services_Data { get; set; }
        public virtual DbSet<Services_Preferences> Services_Preferences { get; set; }
        public virtual DbSet<Services_Preferences_HB> Services_Preferences_HB { get; set; }
        public virtual DbSet<Services_Preferences_Types> Services_Preferences_Types { get; set; }
        public virtual DbSet<Services_Prices> Services_Prices { get; set; }
        public virtual DbSet<StatisticLeftMenu> StatisticLeftMenu { get; set; }
        public virtual DbSet<Tarifs> Tarifs { get; set; }
        public virtual DbSet<Tarifs_Data> Tarifs_Data { get; set; }
        public virtual DbSet<Statistic_pbx> Statistic_pbx { get; set; }
        public virtual DbSet<StatusSign> StatusSign { get; set; }
    }
}
