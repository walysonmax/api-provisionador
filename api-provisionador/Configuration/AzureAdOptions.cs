using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_provisionador
{
    public class AzureAdOptions
    {
        public string Instance { get; set; }
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Authority => Instance + TenantId;
        public string ClientSecret { get; set; }
    }
}
