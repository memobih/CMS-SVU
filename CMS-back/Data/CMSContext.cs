using Microsoft.EntityFrameworkCore;
using CMS_back.Reposatory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CMS_back.Data
{
	public class CMSContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
	{

		public DbSet<ApplicationUser> ApplicationUser { get; set; }
		public DbSet<Faculity> Faculity { get; set; }
		public DbSet<Faculity_Node> Node { get; set; }
		public DbSet<Faculity_Phases> Phase { get; set; }
		public DbSet<Faculity_Semester> Semester { get; set; }
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
        public DbSet<Faculity_Node> Faculity_Nodes { get; set; }
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

			modelBuilder.Entity<ControlSubject>().HasKey(e => new { e.ControlID, e.SubjectID });
			modelBuilder.Entity<ControlUsers>().HasKey(e => new { e.ControlID, e.UserID });
			modelBuilder.Entity<Control_UserTasks>().HasKey(e => new { e.UserTaskID, e.Control_TaskID });
		


			base.OnModelCreating(modelBuilder);
		}



	}
}
