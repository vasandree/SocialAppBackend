using PersonService.Domain.Enums;

namespace PersonService.Application.Dtos.Responses;

public class PlaceDto : BaseSocialNodeDto
{
    public SocialNodeType Type => SocialNodeType.Place;
}