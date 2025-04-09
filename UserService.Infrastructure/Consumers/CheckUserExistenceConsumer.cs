using Common.ServiceBus;
using Common.ServiceBus.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Infrastructure.Consumers;

public class CheckUserExistenceConsumer : BaseConsumer<CheckUserExistenceRequest>
{
    private readonly IUserRepository _userRepository;
    
    public CheckUserExistenceConsumer(ILogger<BaseConsumer<CheckUserExistenceRequest>> logger, IUserRepository userRepository) : base(logger)
    {
        _userRepository = userRepository;
    }

    protected override async Task HandleMessageAsync(ConsumeContext<CheckUserExistenceRequest> context)
    {

        var message = context.Message;
        await context.Publish(new CheckUserExistenceResponse(await _userRepository.CheckIfExists(message.UserId)));

    }
}