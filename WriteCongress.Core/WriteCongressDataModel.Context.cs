﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WriteCongress.Core
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WriteCongressConnection : DbContext
    {
        public WriteCongressConnection()
            : base("name=WriteCongressConnection")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<IssueLetter> IssueLetters { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
    }
}
