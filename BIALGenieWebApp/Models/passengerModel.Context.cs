﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BIALGenieWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class passengersEntities1 : DbContext
    {
        public passengersEntities1()
            : base("name=passengersEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BookingDetail> BookingDetails { get; set; }
        public virtual DbSet<PassengerDetail> PassengerDetails { get; set; }
        public virtual DbSet<gymservice> gymservices { get; set; }
        public virtual DbSet<spaservice> spaservices { get; set; }
        public virtual DbSet<GymServiceView> GymServiceViews { get; set; }
        public virtual DbSet<SpaView> SpaViews { get; set; }
        public virtual DbSet<FlightDetail> FlightDetails { get; set; }
    }
}
