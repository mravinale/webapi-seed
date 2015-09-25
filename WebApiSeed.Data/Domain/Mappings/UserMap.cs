namespace WebApiSeed.Data.Domain.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(user => user.Id);

            // Properties
            Property(user => user.UserName)
                .IsOptional()
                .HasMaxLength(20);

            //Optional: if no access token is present,
            //user is considered to be offline/logged out
            Property(user => user.AccessToken)
                .IsOptional();

            Property(user => user.Gender)
                .IsOptional();

            // Table & Column Mappings
            ToTable("User");
            Property(user => user.Id).HasColumnName("Id");
            Property(user => user.AccessToken).HasColumnName("AccessToken");
            Property(user => user.UserName).HasColumnName("UserName");
            Property(user => user.Gender).HasColumnName("Gender");
        }
    }
}