using Demo.DAL.Model;
using System.Data.Entity.ModelConfiguration;

namespace Demo.DAL.EntityConfiguration
{
    public class HumanConfiguration : EntityTypeConfiguration<Human>
    {
        public HumanConfiguration()
        {
            // Primary Key
            ToTable("Humans");
            HasKey(t => t.Id);

            // Constraints
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            Property(t => t.Id).HasColumnName("HumanID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
