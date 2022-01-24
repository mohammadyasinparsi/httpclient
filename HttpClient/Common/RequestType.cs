namespace HttpClient.Common
{
    public enum RequestType
    {
        Get,
        Post,
        Put,
        Delete,
        Patch,
        Option
    }

    public enum SendRequstType
    {
        Json,
        FormData,
        UrlEncoded
    }
}
