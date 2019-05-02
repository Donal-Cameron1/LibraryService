namespace LibraryService.Data.Utils
{

    public static class CustomRoles
    {
        public const string Admin = "Admin";
        public const string Staff = "Staff";
        public const string User = "User";
        public const string AdminOrStaff = Admin + "," + Staff;
    }

}
