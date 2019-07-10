using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PatientConfiguration : EntityTypeConfiguration<Patient>
    {
        public PatientConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.PatientID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Patient");
            this.Property(t => t.PatientID).HasColumnName("PatientID");
            this.Property(t => t.MetaData).HasColumnName("MetaData");
        }
    }
}
