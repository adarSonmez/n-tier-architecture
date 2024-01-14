namespace Core.Utilities.Results.Abstract
{
    public interface IDataResult<T>
    {
        T? Data { get; set; }
    }
}
