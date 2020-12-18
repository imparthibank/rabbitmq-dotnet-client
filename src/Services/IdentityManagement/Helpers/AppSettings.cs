namespace IdentityManagement.Helpers
{
    public class AppSettings
    {
        public static string MigrationDatabase { get; } = Startup.Configuration["MigrationDatabase"];
    }
}
