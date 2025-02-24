namespace FUForum.ViewModels.Systems;

public class UserPasswordChangRequest
{
    public string UserId { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }
}