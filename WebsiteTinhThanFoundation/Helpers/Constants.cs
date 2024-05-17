namespace WebsiteTinhThanFoundation.Helpers
{
    public class Constants
    {
        public static class Roles
        {
            public const string Admin = "Administrator";
            public const string Staff = "Staff";
            public const string Volunteer = "Volunteer";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireStaff = "RequireStaff";
            public const string RequireVolunteer = "RequireVolunteer";
        }
    }
}
