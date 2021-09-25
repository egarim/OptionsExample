using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsExample
{
    public class SlackApiSettings
    {
        public SlackApiSettings()
        {
        }
        public string WebhookUrl { get; set; }
        public string DisplayName { get; set; }
    }
}
