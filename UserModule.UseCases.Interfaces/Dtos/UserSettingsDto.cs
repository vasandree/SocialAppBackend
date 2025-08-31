using System.ComponentModel.DataAnnotations;
using UserModule.Domain.Enums;

namespace UserModule.UseCases.Interfaces.Dtos;

public record UserSettingsDto(Theme Theme, Language LanguageCode);