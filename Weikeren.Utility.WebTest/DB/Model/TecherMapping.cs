using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.WebTest.DB.Model
{
    public class TecherMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Techer>
    {
		/// <summary>
        /// 构造函数
        /// </summary>
        public TecherMap()
        { 
			
			
			// Primary Key
			this.HasKey(t => t.Id);
			
			
			// Properties
			this.Property(t => t.Name)
                .HasMaxLength(100);


			// Table & Column Mappings
			this.ToTable("Techer");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.Description).HasColumnName("Description");


			// Relationships
			
        }
    }
}
