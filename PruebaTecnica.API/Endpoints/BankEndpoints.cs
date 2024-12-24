namespace PruebaTecnica.API.Endpoints;

public static class BankEndpoints
{
    
    public static void Map(WebApplication app){

        var group = app.MapGroup("/Bank").WithTags("Bank");

        group.MapGet("/{BIC}", async (IMediator mediator, IBankRepository repository, string BIC) =>
        {
            var response = await repository.FindByBicAsync(BIC);
            return Results.Ok(response);
        });

        group.MapGet("/", async (IMediator mediator, IBankRepository repository) =>
        {
            var response = await repository.FindAllAsync();
            return Results.Ok(response);
        });

        group.MapPost("ImportNewData", async (IMediator mediator, ImportBankCommand command) =>
        {
            var response = await mediator.Send(command);
            return Results.Ok(response);
        });
    }
}