﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.InteractionGallery.KinectBO
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class kinectEntities1 : DbContext
    {
        public kinectEntities1()
            : base("name=kinectEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<album> album { get; set; }
        public DbSet<artista> artista { get; set; }
        public DbSet<canciones> canciones { get; set; }
        public DbSet<disquera> disquera { get; set; }
        public DbSet<genero> genero { get; set; }
        public DbSet<rating> rating { get; set; }
    }
}
