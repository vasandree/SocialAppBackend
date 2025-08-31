using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SocialNodeModule.UseCases.Interfaces.Dtos.Requests;

public record ClusterRequestDto(string Name, string? Description, IFormFile? Avatar, List<Guid>? Users);