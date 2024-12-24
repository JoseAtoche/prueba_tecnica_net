namespace PruebaTecnica.Application.Dto;

public abstract class ResponseBase
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }

    public ResponseBase()
    {
        Errors = [];
    }
}
