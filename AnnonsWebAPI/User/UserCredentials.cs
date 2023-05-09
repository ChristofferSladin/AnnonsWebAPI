namespace AnnonsWebAPI.User
{
    public class UserCredentials
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() // Full read/write access
            {
                UserName = "richard_admin",
                EmailAddress = "richard_admin@email.se",
                Password = "passwordAdmin",
                GivenName = "Richard",
                SurName = "chalk",
                Role = "Admin",
            },
            new UserModel() // Can only Read
            {
                UserName = "richard_user",
                EmailAddress = "richard_user@email.se",
                Password = "passwordUser",
                GivenName = "Richard",
                SurName = "Chalk",
                Role = "User",
            },
            new UserModel() // Full read/write access
            {
                UserName = "Christo_admin",
                EmailAddress = "christo_admin@email.se",
                Password = "passwordAdmin",
                GivenName = "Christoffer",
                SurName = "Sladin",
                Role = "Admin",
            },
            new UserModel() // Can only Read
            {
                UserName = "Christo_user",
                EmailAddress = "christo_user@email.se",
                Password = "passwordUser",
                GivenName = "Christoffer",
                SurName = "Sladin",
                Role = "User",
            }
        };
    }
}
