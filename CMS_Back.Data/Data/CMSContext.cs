using Microsoft.EntityFrameworkCore;
using CMS_back.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CMS_back.Consts;

namespace CMS_back.Data
{
    public class CMSContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Faculity> Faculity { get; set; }
        public DbSet<Faculity_Node> Faculity_Node { get; set; }
        public DbSet<Faculity_Phases> Faculity_Phase { get; set; }
        public DbSet<Faculity_Semester> Faculity_Semester { get; set; }
        public DbSet<Control> Control { get; set; }
        public DbSet<ControlSubjects> ControlSubjects { get; set; }
        public DbSet<Control_Task> Control_Task { get; set; }
        public DbSet<Control_Addresses> Control_Address { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<StudentSemester> StudentSemester { get; set; }
        public DbSet<Subject_Assess> SubjectAssess { get; set; }
        public DbSet<Subject_Committees> Subject_Committees { get; set; }
        public DbSet<Student_STATUS> Student_STATUS { get; set; }
        public DbSet<Study_Method> Study_Method { get; set; }
        public DbSet<Student_SemesterSubjects> Student_SemesterSubjects { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<FaculityType> FaculityType { get; set; }
        public DbSet<FaculityHierarycal> FaculityHierarycal { get; set; }
        public DbSet<ControlUsers> ControlUsers { get; set; }
        public DbSet<Control_Note> Control_Note { get; set; }
        public DbSet<Control_UserTasks> Control_UserTasks { get; set; }
        public DbSet<Committees> Committees { get; set; }
        public DbSet<BYLAW> BYLAW { get; set; }
        public DbSet<Assess> Assess { get; set; }
        public DbSet<ACAD_YEAR> ACAD_YEAR { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Control>()
            .HasMany(c => c.ControlUsers)
            .WithOne(cu => cu.Control)
            .HasForeignKey(cu => cu.ControlID)
            .OnDelete(DeleteBehavior.Cascade);
            SeedRoles(modelBuilder);


            #region New Update

            modelBuilder.Entity<Control_Task>()
            .HasMany(c => c.UserTasks)
            .WithOne(cu => cu.Control_Task)
            .HasForeignKey(cu => cu.Control_TaskID)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Control>()
                    .HasMany(cn => cn.Control_Notes)
                    .WithOne(c => c.Control)
                    .HasForeignKey(cn => cn.ControlID)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Control>()
                    .HasMany(cn => cn.Control_Tasks)
                    .WithOne(c => c.Control)
                    .HasForeignKey(cn => cn.ControlID)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ControlSubjects>().HasKey(cs => new { cs.SubjectID, cs.ControlID });
            modelBuilder.Entity<ControlUsers>().HasKey(cu => new { cu.UserID, cu.ControlID });
            modelBuilder.Entity<Control_Addresses>().HasKey(ca => new { ca.Address, ca.ControlID });
            modelBuilder.Entity<Control_UserTasks>().HasKey(ct => new { ct.UserTaskID, ct.Control_TaskID });
            modelBuilder.Entity<Subject_Committees>().HasKey(sc => new { sc.SubjectID, sc.CommitteeID });
            modelBuilder.Entity<Student_SemesterSubjects>().HasKey(ss => new { ss.SubjectID, ss.StudentSemesterID });
            modelBuilder.Entity<Subject_Assess>().HasKey(sa => new { sa.SubjectID, sa.AssessID });


            #endregion

        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                  new IdentityRole() { Name = ConstsRoles.AdminUniversity, NormalizedName = ConstsRoles.AdminUniversity },
                  new IdentityRole() { Name = ConstsRoles.AdminFaculty, NormalizedName = ConstsRoles.AdminFaculty },
                  new IdentityRole() { Name = ConstsRoles.Staff, NormalizedName = ConstsRoles.Staff }
                );

        }

    }
}
