using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace newApp.Models
{
    public class Depense
    {
        [JsonPropertyName("idDepense")]
        public int IdDepense { get; set; }

        [JsonPropertyName("montant")]
        public double Montant { get; set; }

        [JsonPropertyName("dateEns")]
        public string DateEns { get; set; }

        [JsonPropertyName("leadId")]
        public int? LeadId { get; set; } 

        [JsonPropertyName("ticketId")]
        public int? TicketId { get; set; } 

        [JsonPropertyName("customerId")]
        public int? CustomerId { get; set; }  

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        public Depense() { }

       
        public Depense(int idDepense, double montant, string dateEns, int? leadId, int? ticketId, int? customerId, string customerName, string description)
        {
            IdDepense = idDepense;
            Montant = montant;
            DateEns = dateEns;
            LeadId = leadId;
            TicketId = ticketId;
            CustomerId = customerId;
            CustomerName = customerName;
            Description = description;
        }

        public static double getTotalLead(List<Depense> depenses)
        {
            double ans=0;
            for(int i=0;i<depenses.Count;i++)
            {
                if(depenses[i].LeadId!=0)
                {
                    ans=ans+depenses[i].Montant;
                }
            }
            return ans ;
        }

        public static double getTotalTicket(List<Depense> depenses)
        {
            double ans=0;
            for(int i=0;i<depenses.Count;i++)
            {
                if(depenses[i].TicketId!=0)
                {
                    ans=ans+depenses[i].Montant;
                }
            }
            return ans ;
        }

       
        public static Depense FromJson(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<Depense>(json);
        }



        
        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
