using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public sealed class DictionaryReplacementNotificationContent : INotificationContent
    {
        private class DefaultDictionaryReplacementNotificationContentConfiguration : IDictionaryReplacementNotificationContentConfiguration
        {
            public char TokenStart => '{';
            public char TokenEnd => '}';
        }

        private readonly INotificationContent _notificationContent;
        private readonly IDictionary<string, string> _replacements;
        private readonly string _defaultValueIfNoMatch;

        public IDictionaryReplacementNotificationContentConfiguration Configuration { get; }

        public DictionaryReplacementNotificationContent(INotificationContent notificationContent, IDictionary<string, string> replacements, string defaultValueIfNoMatch = "[INVALID_TOKEN:{0}]")
            : this(DefaultConfiguration, notificationContent, replacements, defaultValueIfNoMatch)
        { }

        public DictionaryReplacementNotificationContent(IDictionaryReplacementNotificationContentConfiguration dictionaryReplacementNotificationContent, INotificationContent notificationContent, IDictionary<string, string> replacements, string defaultValueIfNoMatch = "[INVALID_TOKEN:{0}]")
        {
            this.Configuration = dictionaryReplacementNotificationContent;
            this._notificationContent = notificationContent;
            this._replacements = replacements;
            this._defaultValueIfNoMatch = defaultValueIfNoMatch;
        }

        public async Task ExecuteAsync(TextWriter textWriter)
        {
            var regex = new Regex($"\\{this.Configuration.TokenStart}([^\\{this.Configuration.TokenEnd}]+)\\{this.Configuration.TokenEnd}");

            using (var tw = new StringWriter())
            {
                await this._notificationContent.ExecuteAsync(tw);
                var result = tw.ToString();

                var hadReplacements = true;

                while (hadReplacements)
                {
                    hadReplacements = false;
                    result = regex.Replace(result, new MatchEvaluator(match =>
                    {
                        if (!hadReplacements)
                        {
                            hadReplacements = true;
                        }

                        var token = match.Groups[1].Value;

                        if (this._replacements.TryGetValue(token, out var replacement))
                        {
                            return replacement;
                        }
                        else
                        {
                            return string.Format(this._defaultValueIfNoMatch, token);
                        }

                    }));
                }
                await textWriter.WriteAsync(result);
            }
        }

        public static IDictionaryReplacementNotificationContentConfiguration DefaultConfiguration
        {
            get => new DefaultDictionaryReplacementNotificationContentConfiguration();
        }
    }
}
