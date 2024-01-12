namespace Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        bool Success { get; set; }
        string? Message { get; set; }
    }
}
