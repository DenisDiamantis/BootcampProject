namespace FinalProject.Data.Dtos.AcountDtos
{
    public class LoginDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public UserProfileDto User { get; set; }
        public string Message { get; set; }

    }
}
