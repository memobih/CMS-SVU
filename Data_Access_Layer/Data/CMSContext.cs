using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Data_Access_Layer.Consts;

namespace Data_Access_Layer.Data
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
			.HasForeignKey(cu => cu.Control_TaskID) // Assuming Control_TaskID is the foreign key property in UserTasks
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
