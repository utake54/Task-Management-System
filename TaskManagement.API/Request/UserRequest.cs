namespace TaskManagement.API.Request
{
    public class AddUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public int ZipCode { get; set; }
    }

    public class UpdateUserRequest : AddUserRequest
    {
        public int Id { get; set; }
    }

    public class GetUserRequest
    {
        public int Id { get; set; }
    }

    public class DeleteUserRequest : GetUserRequest
    {

    }
}
