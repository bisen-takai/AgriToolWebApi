namespace AgriToolWebApi.Application.Requests
{
    public class UserUpdateRequest
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int PrivilegeId { get; set; }
        public int ColorId { get; set; }
        public string Memo { get; set; }
    }
}
