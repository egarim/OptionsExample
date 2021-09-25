using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace OptionsExample
{
    public class ConfigurePublicSlackApiSettings : IConfigureNamedOptions<SlackApiSettings>
    {
        // inject the PublicSlackDetailsService directly
        private readonly PublicSlackDetailsService _service;
        public ConfigurePublicSlackApiSettings(PublicSlackDetailsService service)
        {
            _service = service;
        }

        // Configure the named instance
        public void Configure(string name, SlackApiSettings options)
        {
            // Only configure the options if this is the correct instance
            if (name == "Public")
            {
                options.WebhookUrl = _service.GetPublicWebhookUrl();
            }
        }

        // This won't be called, but is required for the interface
        public void Configure(SlackApiSettings options) => Configure(Options.DefaultName, options);
    }
}
