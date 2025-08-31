using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SocialNodeModule.UseCases.Interfaces.Dtos.Requests;

public record PlaceRequestDto(string Name, string? Description, IFormFile? Avatar);