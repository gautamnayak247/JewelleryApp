namespace Jewellery.Domain.Entities
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
    }
}
