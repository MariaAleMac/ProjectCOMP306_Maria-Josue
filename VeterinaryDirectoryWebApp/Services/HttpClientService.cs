using System.Net.Http.Headers;

namespace VeterinaryDirectoryWebApp.Services
{
    public class HttpClientService
    {
        //Hosted web API REST Service base url
        // private static string Baseurl = "https://localhost:32768";
        private static string Baseurl = "https://34.144.192.118.nip.io";

        public HttpClient client = new HttpClient();
        public HttpClientService() {

            //Passing service base url
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            //Define request data format
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
            
    }
}
