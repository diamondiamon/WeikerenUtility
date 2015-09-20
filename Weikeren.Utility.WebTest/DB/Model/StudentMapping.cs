using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.WebTest.DB.Model
{
    public class StudentMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Student>
    {
		/// <summary>
        /// 构造函数
        /// </summary>
        public StudentMap()
        { 
			
			
			// Primary Key
			this.HasKey(t => t.Id);
			
			
			// Properties
			this.Property(t => t.Name)
                .HasMaxLength(10);


			// Table & Column Mappings
			this.ToTable("Student");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.Age).HasColumnName("Age");
			this.Property(t => t.TeacherId).HasColumnName("TeacherId");
			this.Property(t => t.Score).HasColumnName("Score");


			// Relationships
			this.HasOptional(t => t.techer)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.TeacherId);

        }
    }
}
