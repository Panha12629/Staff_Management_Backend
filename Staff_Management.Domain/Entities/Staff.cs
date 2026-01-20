namespace Staff_Management.Domain.Entities
{
    public class Staff
    {
        public int Id { get; set; }
        public string? StaffId { get; set; }
        public string? FullName { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
    }
}
