using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsExample
{
    public class PublicSlackDetailsService
    {
        public string GetPublicWebhookUrl()
        {
            return "/some/url"; 
        }
        //public string GetPublicWebhookUrl();
    }
}
