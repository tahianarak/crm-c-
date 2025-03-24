using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace  newApp.Models;
public class LoginClientService
{
   /* public static async Task<bool> CheckRoleManagerAsync(string jsessionid,HttpClient client)
    {
        try
        {
            string url="http://localhost:8080/api/lo/data?jsessionid="+jsessionid;
            
            HttpResponseMessage response = await client.SendAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                bool hasRoleManager = bool.Parse(responseBody);
                return hasRoleManager;
            }
            else
            {
                Console.WriteLine($"Erreur: {response.StatusCode}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'appel à l'API: {ex.Message}");
            return false;
        }
    }*/

    //fonction pour check la session
    public static async Task<bool> IsSessionValid(string jsessionId,HttpClient client)
    {
        if (string.IsNullOrEmpty(jsessionId))
        {
            return false;
        }
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:8080/api/lo/data?jsessionid={jsessionId}");
            requestMessage.Headers.Add("Cookie", $"JSESSIONID={jsessionId}");
            var response = await client.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content.Equals("true", StringComparison.OrdinalIgnoreCase);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la vérification de la session : {ex.Message}");
        }

        return false;
    }
}
