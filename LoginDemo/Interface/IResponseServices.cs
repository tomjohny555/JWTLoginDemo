namespace LoginDemo.Interface
{
    public interface IResponseServices
    {
        string GenerateResponse(string Description, int StatusCode, dynamic Content, string Status);
        string CreateErrorResponse(string Description, int statusCode, string Status);
    }
}
