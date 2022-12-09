using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SignalrDemo.EFModels
{
    public partial class ChatAppContext : DbContext
    {
 

        public ChatAppContext(DbContextOptions<ChatAppContext> options)
            : base(options)
        {
        }

  
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<MessageDetail> MessageDetails { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<Connections> Connections { get; set; }
        public virtual DbSet<Person> Person { get; set; }


//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=LAPN222393\\MSSQL2019;Database=ChatApp;Trusted_Connection=True; Encrypt=false");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

//            modelBuilder.Entity<Group>(entity =>
//            {
//                entity.ToTable("Group");

//                entity.Property(e => e.GroupId).ValueGeneratedNever();

//                entity.Property(e => e.GroupName)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<MessageDetail>(entity =>
//            {
//                entity.HasKey(e => e.MessageId);

//                entity.Property(e => e.MessageId).ValueGeneratedNever();

//                entity.Property(e => e.Message).HasMaxLength(500);

//                entity.HasOne(d => d.Group)
//                    .WithMany(p => p.MessageDetails)
//                    .HasForeignKey(d => d.GroupId)
//                    .HasConstraintName("FK_MessageDetails_Group");

//                entity.HasOne(d => d.SentByNavigation)
//                    .WithMany(p => p.MessageDetails)
//                    .HasForeignKey(d => d.SentBy)
//                    .HasConstraintName("FK_MessageDetails_Person");
//            });

//            modelBuilder.Entity<Person>(entity =>
//            {
//                entity.ToTable("Person");
//            });

//            modelBuilder.Entity<UserGroup>(entity =>
//            {
//                entity.ToTable("UserGroup");

//                entity.Property(e => e.UserGroupId).ValueGeneratedNever();

//                entity.HasOne(d => d.Group)
//                    .WithMany(p => p.UserGroups)
//                    .HasForeignKey(d => d.GroupId)
//                    .HasConstraintName("FK_UserGroup_Person");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
