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
		public DbSet<ControlSubject> ControlSubject { get; set; }
		public DbSet<Control_Task> Control_Task { get; set; }
		public DbSet<Control_Addresses> conrol_Addresse { get; set; }
		public DbSet<Student> Student { get; set; }
		public DbSet<Subject> Subject { get; set; }
		public DbSet<StudentSemester> StudentSemester { get; set; }
		public DbSet<Subject_Assess> SubjectAssesse { get; set; }
		public DbSet<Subject_Committees> Subject_Committee { get; set; }
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
			//modelBuilder.Entity<ApplicationUser>().ToTable("User");
			//modelBuilder.Entity<Control>().ToTable("Control");
			//modelBuilder.Entity<Subject>().ToTable("Subject");
			//modelBuilder.Entity<Student>().ToTable("Student");
			//modelBuilder.Entity<StudentSubject>().ToTable("StudentSubject");
			//modelBuilder.Entity<Subject_Assess>().ToTable("Subject_Assess");
			//modelBuilder.Entity<ControlSubject>().ToTable("ControlSubject");
			//modelBuilder.Entity<Control_Task>().ToTable("Control_Text");
			//modelBuilder.Entity<Control_Addresses>().ToTable("Control_Addresse");
			//modelBuilder.Entity<Faculity>().ToTable("Faculity");
			//modelBuilder.Entity<Faculity_Node>().ToTable("Faculity_Node");
			//modelBuilder.Entity<Faculity_Phases>().ToTable("Faculity_Phase");
			//modelBuilder.Entity<Faculity_Semester>().ToTable("Faculity_Semester");

			//modelBuilder.Entity<ControlSubject>().HasKey(e => new { e.ControlID, e.SubjectID });

			//modelBuilder.Entity<ControlUsers>().HasOne(c=>c.ControlID).WithMany(c=>c)
			//modelBuilder.Entity<Control_UserTasks>().HasKey(e => new { e.UserTaskID, e.Control_TaskID });

			

			base.OnModelCreating(modelBuilder);

            //         modelBuilder.Entity<ControlUsers>()
            //.HasOne(cu => cu.User)
            //.WithMany()
            //.HasForeignKey(cu => cu.UserID)
            //.OnDelete(DeleteBehavior.Cascade); 

            //modelBuilder.Entity<ControlUsers>()
            //    .HasOne(cu => cu.Control)
            //    .WithMany()
            //    .HasForeignKey(cu => cu.ControlID)
            //    .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Control>()
            .HasMany(c => c.ControlUsers)
            .WithOne(cu => cu.Control)
            .HasForeignKey(cu => cu.ControlID) // Assuming ControlID is the foreign key property in ControlUser
            .OnDelete(DeleteBehavior.Cascade);
            SeedRoles(modelBuilder);

			modelBuilder.Entity<Control_Task>()
			.HasMany(c => c.UserTasks)
			.WithOne(cu => cu.Control_Task)
			.HasForeignKey(cu => cu.Control_TaskID) // Assuming ControlID is the foreign key property in ControlUser
			.OnDelete(DeleteBehavior.Cascade);
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
