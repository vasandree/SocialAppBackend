namespace Common.ServiceBus.Contracts;

public record CheckUserExistenceRequest(Guid UserId);
public record CheckUserExistenceResponse(bool Exists);