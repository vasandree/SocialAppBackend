using UserService.Domain;
using UserService.Domain.Entities;

namespace UserService.Application.Dtos.Telegram;

public class TelegramInitData
{
    public TelegramUser User { get; set; }
    public string ChatInstance { get; set; }
    public string ChatType { get; set; }
    public long AuthDate { get; set; }
    public string Signature { get; set; }
    public string Hash { get; set; }
}