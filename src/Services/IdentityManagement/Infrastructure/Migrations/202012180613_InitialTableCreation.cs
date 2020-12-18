using FluentMigrator;
using System;
using System.Globalization;

namespace IdentityManagement.Infrastructure.Migrations
{
    [Migration(202012180613)]
    public class InitialTableCreation_202012180613 : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
               .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
               .WithColumn("FirstName").AsString().NotNullable()
               .WithColumn("LastName").AsString().NotNullable()
               .WithColumn("UserName").AsString().NotNullable()
               .WithColumn("NickName").AsString().Nullable()
               .WithColumn("PersonalEmail").AsString().NotNullable()
               .WithColumn("Gender").AsInt32().NotNullable()
               .WithColumn("DateOfBirth").AsDate().NotNullable()
               .WithColumn("CreatedDate").AsDateTime().NotNullable()
               .WithColumn("LastUpdatedDate").AsDateTime().Nullable()
               .WithColumn("IsActive").AsBoolean().NotNullable();

            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.Parse("536F8C74-E624-4F11-AECD-13237B5F2372"),
                FirstName = "Parthiban",
                LastName = "Karnan",
                UserName = "tn.0001.in",
                NickName = "Parthi",
                PersonalEmail = "parthi@abc.com",
                Gender = 1,
                DateOfBirth = DateTime.ParseExact("13-09-1989", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                IsActive = true,
            });

            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.Parse("731EB7E4-2A09-4778-91BA-C349EFEBA83B"),
                FirstName = "George",
                LastName = "Jose",
                UserName = "tn.0002.in",
                NickName = "GTJ",
                PersonalEmail = "george@abc.com",
                Gender = 1,
                DateOfBirth = DateTime.ParseExact("13-09-1990", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                IsActive = true,
            });
            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.Parse("2e9f887c-b71b-4cbf-84cf-a11b460195d8"),
                FirstName = "Anil",
                LastName = "Kumar",
                UserName = "tn.0003.in",
                NickName = "Anil",
                PersonalEmail = "anil@abc.com",
                Gender = 1,
                DateOfBirth = DateTime.ParseExact("13-09-1989", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                IsActive = true,
            });
            Insert.IntoTable("Users").Row(new
            {
                Id = Guid.Parse("231dd0c8-f237-416a-ac13-232bdc670e04"),
                FirstName = "Ken",
                LastName = "Devis",
                UserName = "tn.0004.in",
                NickName = "Ken",
                PersonalEmail = "ken@abc.com",
                Gender = 1,
                DateOfBirth = DateTime.ParseExact("13-09-1979", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                IsActive = true,
            });
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
