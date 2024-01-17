namespace iShopServerSide.Model
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int Role { get; set; }

    }
}
