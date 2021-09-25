using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace OptionsExample
{
    public class ConfigureAllSlackApiSettings : IConfigureNamedOptions<SlackApiSettings>
    {
        // inject the PublicSlackDetailsService directly
        private readonly PublicSlackDetailsService _service;
        public ConfigureAllSlackApiSettings(PublicSlackDetailsService service)
        {
            _service = service;
        }

        // Configure all instances
        public void Configure(string name, SlackApiSettings options)
        {
            // we don't care which instance it is, just set the URL!
            options.WebhookUrl = _service.GetPublicWebhookUrl();
        }

        // This won't be called, but is required for the interface
        public void Configure(SlackApiSettings options) => Configure(Options.DefaultName, options);
    }
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
