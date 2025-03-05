namespace UserService.Application.Dtos.Telegram;

public class TelegramUser
{
    public long Id { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Username { get; set; }
    public string Language_Code { get; set; }
    public bool Allows_Write_To_Pm { get; set; }
    public string Photo_Url { get; set; }
}