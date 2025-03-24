using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using newApp.Models;

namespace newApp.Controllers
{
    public class DepenseController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string SpringBootApiUrl = "http://localhost:8080/api/"; // API Spring Boot

        public DepenseController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

    [HttpGet]
    public async Task<IActionResult> FormulairePourcentage()
    {
        try
        {
            return View("FormulairePourcentage");
        }
        catch (Exception ex)
        {
            
            return View("Liste");
        }
    }


    [HttpPost]
    public async Task<IActionResult> ModifierPourcentage(string pourcentage)
    {
        try
        {
            double parsedPourcentage = double.Parse(pourcentage);

            var requestData = new Dictionary<string, string>
            {
                { "pourcentage", parsedPourcentage.ToString() }
            };

            var content = new FormUrlEncodedContent(requestData);
            HttpResponseMessage response = await _httpClient.PostAsync(SpringBootApiUrl + "depense/modiferPourcentage", content);
            
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse);
            
            return RedirectToAction("Liste");
        }
        catch (Exception ex)
        {
            ViewData["Error"] = $"Erreur interne : {ex.Message}";
            return RedirectToAction("Liste");
        }
    }

    [HttpGet]
    public async Task<IActionResult> supprimer( string idDepense)
    {
        try
        {
            int parsedIdDepense = int.Parse(idDepense);

            var requestData = new Dictionary<string, string>
            {
                { "idDepense", parsedIdDepense.ToString() }
            };

            var content = new FormUrlEncodedContent(requestData);
            HttpResponseMessage response = await _httpClient.PostAsync(SpringBootApiUrl+"depense/delete", content);
            
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse);
            return  RedirectToAction("Liste");   
        
        }
        catch (Exception ex)
        {
            ViewData["Depenses"] = new List<Depense>();
            ViewData["Error"] = $"Erreur interne : {ex.Message}";
            return View("Liste");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Modifier(string montant, string idDepense)
    {
        try
        {
            double parsedMontant = double.Parse(montant);
            int parsedIdDepense = int.Parse(idDepense);

            var requestData = new Dictionary<string, string>
            {
                { "idDepense", parsedIdDepense.ToString() },
                { "montant", parsedMontant.ToString() }
            };

            var content = new FormUrlEncodedContent(requestData);
            HttpResponseMessage response = await _httpClient.PostAsync(SpringBootApiUrl+"depense/update", content);
            
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse);
            return  RedirectToAction("Liste");   
        
        }
        catch (Exception ex)
        {
            ViewData["Depenses"] = new List<Depense>();
            ViewData["Error"] = $"Erreur interne : {ex.Message}";
            return View("Liste");
        }
    }

        public async Task<IActionResult> Liste()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(SpringBootApiUrl+"depense/all");
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Depense> depenses = JsonSerializer.Deserialize<List<Depense>>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                HttpResponseMessage response2 = await _httpClient.GetAsync(SpringBootApiUrl+"budget/all");
                string jsonResponse2 = await response2.Content.ReadAsStringAsync();

                List<CustomerBudget> budgets = JsonSerializer.Deserialize<List<CustomerBudget>>(jsonResponse2, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });



                Console.WriteLine("taille: " + depenses.Count);
                ViewData["Depenses"] = depenses;
                ViewData["Budgets"] = budgets;
                ViewData["totBudget"]=CustomerBudget.getTotalBudget(budgets);
                ViewData["totalLead"]=Depense.getTotalLead(depenses);
                ViewData["totalTicket"]=Depense.getTotalTicket(depenses);
                ViewData["first4"]=CustomerBudget.getFourFirstBudget(budgets);
                return View("Liste"); 
            }
            catch (Exception ex)
            {
                ViewData["Depenses"] = new List<Depense>();
                ViewData["Error"] = $"Erreur interne : {ex.Message}";
                return View("Liste"); 
            }
        }
    }
}
