namespace Jewellery.Domain.Entities
{
    public interface IUser
    {
        string FirstName { get; set; }
        int Id { get; set; }
        string LastName { get; set; }
        string UserId { get; set; }
        string Type { get; set; }
    }
}