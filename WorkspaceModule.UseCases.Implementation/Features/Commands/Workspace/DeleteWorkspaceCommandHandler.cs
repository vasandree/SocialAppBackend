using EventModule.UseCases.Interfaces.Commands.Event;
using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.UseCases.Interfaces.Commands;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.UseCases.Interfaces.Commands.Workspace;

namespace WorkspaceModule.UseCases.Implementation.Features.Commands.Workspace;

internal sealed class DeleteWorkspaceCommandHandler(ISender mediator, IWorkspaceEntityRepository workspaceRepository)
    : IRequestHandler<DeleteWorkspaceCommand, Unit>
{
    public async Task<Unit> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        await using var transaction =
            await workspaceRepository.GetDbContext().Database.BeginTransactionAsync(cancellationToken);

        try
        {
            if (!await workspaceRepository.CheckIfExists(request.WorkspaceId))
                throw new NotFound("Workspace not found");

            var workspace = await workspaceRepository.GetByIdAsync(request.WorkspaceId);

            if (!workspace.IsUserCreator(request.UserId))
                throw new Forbidden("You are not allowed to delete this workspace");

            await mediator.Send(new DeleteTasksByWorkspaceCommand(workspace.Id), cancellationToken);
            await mediator.Send(new DeleteEventsByWorkspaceCommand(workspace.Id), cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            workspaceRepository.ClearChanges();
        }
    }
}