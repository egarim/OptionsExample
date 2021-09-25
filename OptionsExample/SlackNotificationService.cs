using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace OptionsExample
{
    public class SlackNotificationService
    {
        public SlackNotificationService(IOptionsSnapshot<SlackApiSettings> options)
        {
            // fetch the settings for each channel
            SlackApiSettings devSettings = options.Get("Dev");
            SlackApiSettings generalSettings = options.Get("General");
            SlackApiSettings publicSettings = options.Get("Public");

            // fetch the default unnamed options
            SlackApiSettings defaultSettings = options.Value;
        }
    }
}
