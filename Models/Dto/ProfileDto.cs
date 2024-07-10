namespace Models.Dto
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string PortfolioUrl { get; set; }
        public required string ProfileImage { get; set; }
    }
}
