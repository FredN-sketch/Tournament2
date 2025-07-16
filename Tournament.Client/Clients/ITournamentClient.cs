namespace Tournament.Client.Clients
{
    public interface ITournamentClient
    {
        Task<T> GetAsync<T>(string path, string contentType = "application/json");
    }
}
