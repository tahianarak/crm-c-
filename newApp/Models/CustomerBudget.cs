using System;
using System.Text.Json.Serialization;

namespace newApp.Models
{
    public class CustomerBudget
    {
        [JsonPropertyName("idBudgetCustomer")]
        public int IdBudgetCustomer { get; set; }

        [JsonPropertyName("montant")]
        public double Montant { get; set; }

        [JsonPropertyName("dateEns")]
        public string DateEns { get; set; }

        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        public CustomerBudget() { }

        public CustomerBudget(int idBudgetCustomer, double montant, string dateEns, int customerId)
        {
            IdBudgetCustomer = idBudgetCustomer;
            Montant = montant;
            DateEns = dateEns;
            CustomerId = customerId;
        }



        public static Dictionary<int,double> getFourFirstBudget(List<CustomerBudget> budgets)
        {
            Dictionary<int,double> ans=new Dictionary<int, double>();
            foreach(var budget in budgets)
            {
                var key=budget.CustomerId;
                if(ans.ContainsKey(key))
                {
                    ans[key]+=budget.Montant;
                }
                else
                {
                     ans[key]=budget.Montant;
                }
            }
            return ans.OrderByDescending(x=>x.Value).Take(3).ToDictionary(k=>k.Key,v=>v.Value);
        }
        public static double getTotalBudget(List<CustomerBudget> budgets)
        {
            double ans=0;
            for(int i=0;i<budgets.Count;i++)
            {
                ans=ans+budgets[i].Montant;    
            }
            return ans;
        }
        public static CustomerBudget FromJson(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<CustomerBudget>(json);
        }

        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}