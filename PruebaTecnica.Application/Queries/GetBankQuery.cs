namespace PruebaTecnica.Application.Queries;

public class GetBankQuery(string id) : IRequest<List<GetBankResponse>>
{
    public string Id { get; set; } = id;
}