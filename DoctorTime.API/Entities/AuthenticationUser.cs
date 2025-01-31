namespace DoctorTime.API.Entities
{
    public interface AuthenticationUser
    {
        string Email { get; set; }
        byte[] PasswordHash { get; set; }
        byte[] PasswordSalt { get; set; }


    }
}
