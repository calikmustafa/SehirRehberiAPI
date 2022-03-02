using Microsoft.AspNetCore.Http;

namespace SehirRehberiAPI.Helpers
{
    public static class JwtExtension
    {
        public static void AddApplicationError(this HttpResponse response,string message)
        {
            response.Headers.Add("Application-Error",message);
            response.Headers.Add("Access-Control-Allow-Origin", "*");//BANA HERKES İSTEK ATABİLSİN
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");


        }
    }
}
