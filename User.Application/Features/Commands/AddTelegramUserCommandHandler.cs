using MediatR;
using Shared.Domain;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Dtos.Requests;
using SocialNode.Contracts.Commands.Person;
using SocialNode.Contracts.Dtos.Requests;
using User.Contracts.Commands;
using User.Contracts.Helpers;
using User.Contracts.Repositories;
using User.Domain.Entities;

namespace User.Application.Features.Commands;

public class AddTelegramUserCommandHandler : IRequestHandler<AddTelegramUserCommand, ApplicationUser>
{
    private readonly ISender _mediator;
    private readonly IUserRepository _userRepository;
    private readonly ITelegramHelper _telegramHelper;
    private readonly ITelegramAccountRepository _telegramAccountRepository;

    public AddTelegramUserCommandHandler(IUserRepository userRepository, ITelegramHelper telegramHelper,
        ITelegramAccountRepository telegramAccountRepository, ISender mediator)
    {
        _userRepository = userRepository;
        _telegramHelper = telegramHelper;
        _telegramAccountRepository = telegramAccountRepository;
        _mediator = mediator;
    }

    public async Task<ApplicationUser> Handle(AddTelegramUserCommand request,
        CancellationToken cancellationToken)
    {
        var parsedTelegramData = _telegramHelper.ParseInitData(request.InitData.InitData);

        if (parsedTelegramData.User == null) throw new NullReferenceException(nameof(parsedTelegramData));

        var user = new ApplicationUser()
        {
            FirstName = parsedTelegramData.User.First_Name,
            LastName = parsedTelegramData.User.Last_Name,
            UserName = parsedTelegramData.User.Username,
            PhotoUrl = parsedTelegramData.User.Photo_Url,
            Language = _telegramHelper.GetLanguage(parsedTelegramData.User.Language_Code),
        };

        var telegramAccount = new TelegramAccount
        {
            Id = parsedTelegramData.User.Id,
            Userneme = parsedTelegramData.User.Username,
            FirstName = parsedTelegramData.User.First_Name,
            LastName = parsedTelegramData.User.Last_Name,
            LanguageCode = parsedTelegramData.User.Language_Code,
            AllowsWriteToPm = parsedTelegramData.User.Allows_Write_To_Pm,
            PhotoUrl = parsedTelegramData.User.Photo_Url,
            User = user,
            UserId = user.Id,
        };

        await _userRepository.AddAsync(user);
        await _telegramAccountRepository.AddAsync(telegramAccount);

        await _mediator.Send(new CreatePersonCommand(user.Id, new PersonRequestDto
        {
            Name = $"{user.FirstName} {user.LastName} (вы)",
            Avatar = null,
            AvatarUrl = user.PhotoUrl
        }), cancellationToken);


        return user;
    }
}