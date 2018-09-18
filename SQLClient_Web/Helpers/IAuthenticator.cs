namespace SQLClient_Web.Helpers
{
    public interface IAuthenticator
    {
        bool IsAuthenticated(string auth);
    }
}
