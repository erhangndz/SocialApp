namespace ServerApp.Dtos.UserDtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? About { get; set; }
        public string? Hobby { get; set; }
    }
}
