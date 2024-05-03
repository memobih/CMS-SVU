using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Data_Access_Layer.Reposatory.Entities;

namespace Data_Access_Layer.Data
{
	public class CMSContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
	{


		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Faculity> Faculities { get; set; }
		public DbSet<Faculity_Node> Nodes { get; set; }
		public DbSet<Faculity_Phases> Phases { get; set; }
		public DbSet<Faculity_Semester> Semesters { get; set; }
		public DbSet<Control> Controls { get; set; }
		public DbSet<ControlSubject> ControlSubjects { get; set; }
		public DbSet<Control_Text> Texts { get; set; }
		public DbSet<Control_Addresses> conrol_Addresses { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<StudentSubject> StudentSubjects { get; set; }
		public DbSet<Subject_Assess> SubjectAssesses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ApplicationUser>().ToTable("User");
			modelBuilder.Entity<Control>().ToTable("Control");
			modelBuilder.Entity<Subject>().ToTable("Subject");
			modelBuilder.Entity<Student>().ToTable("Student");
			modelBuilder.Entity<StudentSubject>().ToTable("StudentSubject");
			modelBuilder.Entity<Subject_Assess>().ToTable("Subject_Assess");
			modelBuilder.Entity<ControlSubject>().ToTable("ControlSubject");
			modelBuilder.Entity<Control_Text>().ToTable("Control_Text");
			modelBuilder.Entity<Control_Addresses>().ToTable("Control_Addresse");
			modelBuilder.Entity<Faculity>().ToTable("Faculity");
			modelBuilder.Entity<Faculity_Node>().ToTable("Faculity_Node");
			modelBuilder.Entity<Faculity_Phases>().ToTable("Faculity_Phase");
			modelBuilder.Entity<Faculity_Semester>().ToTable("Faculity_Semester");

			modelBuilder.Entity<StudentSubject>().HasKey(e => new { e.StudentID, e.SubjectID });
			modelBuilder.Entity<ControlSubject>().HasKey(e => new { e.ControlID, e.SubjectID });






			base.OnModelCreating(modelBuilder);
		}



	}
}
