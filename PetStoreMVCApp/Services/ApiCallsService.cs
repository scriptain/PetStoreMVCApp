namespace PetStoreMVCApp            // used to organise code and avoid naming conflicts
{

    // we start by creating an 'interface' for the service. the interface will define the functionality the service will provide
    // interfaces are 

    public interface IApiCallsService // public so it is available to other parts of the code
    {
        Task<string> GetDataFromApiAsync(string url); // takes a string (the url) and returns an async task that resolves to a string
    }

    public class ApiCallsService : IApiCallsService // this class inherits ":" from the IApiCallsService interface so it must implement the "GetDataFromApiAsync" method
    {
        private readonly HttpClient _httpClient; // readonly means value can not change after it is initialised. 

        public ApiCallsService(HttpClient httpClient) // defines public constructor for the class. takes in a httpclient and assigns it to the private field created above
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDataFromApiAsync(string url) // this is the implementation of the method inhereted from the I
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
