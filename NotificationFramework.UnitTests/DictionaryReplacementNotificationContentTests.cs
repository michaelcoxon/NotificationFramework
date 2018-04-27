using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NotificationFramework.UnitTests
{
    public class DictionaryReplacementNotificationContentTests
    {
        [Fact]
        public async Task SimpleTemplate_Test()
        {
            var configuration = DictionaryReplacementNotificationContent.DefaultConfiguration;

            var tokenStart = configuration.TokenStart;
            var tokenEnd = configuration.TokenEnd;

            var template = new StringNotificationContent($"Hi {tokenStart}firstName{tokenEnd},\nYou have a thing due on {tokenStart}dueDate{tokenEnd}.");

            var replacements = new Dictionary<string, string>
            {
                ["firstName"] = "Joe",
                ["dueDate"] = "20/4/2018",
            };

            var expected = "Hi Joe,\nYou have a thing due on 20/4/2018.";

            var content = new DictionaryReplacementNotificationContent(configuration, template, replacements);

            using (var tw = new StringWriter())
            {
                await content.ExecuteAsync(tw);
                var actual = tw.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task NestedTemplate_Test()
        {
            var configuration = DictionaryReplacementNotificationContent.DefaultConfiguration;

            var tokenStart = configuration.TokenStart;
            var tokenEnd = configuration.TokenEnd;

            var template = new StringNotificationContent($"Hi {tokenStart}name{tokenEnd},\nYou have a thing due on {tokenStart}dueDate{tokenEnd}.");

            var replacements = new Dictionary<string, string>
            {
                ["name"] = $"{tokenStart}currentUserFirstName{tokenEnd} {tokenStart}currentUserSurname{tokenEnd}",
                ["dueDate"] = "20/4/2018",
                ["currentUserFirstName"] = "Joe",
                ["currentUserSurname"] = "Bloggs",
            };

            var expected = "Hi Joe Bloggs,\nYou have a thing due on 20/4/2018.";

            var content = new DictionaryReplacementNotificationContent(configuration, template, replacements);

            using (var tw = new StringWriter())
            {
                await content.ExecuteAsync(tw);
                var actual = tw.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task MissingToken_Test()
        {
            var configuration = DictionaryReplacementNotificationContent.DefaultConfiguration;

            var tokenStart = configuration.TokenStart;
            var tokenEnd = configuration.TokenEnd;

            var template = new StringNotificationContent($"Hi {tokenStart}firstName{tokenEnd},\nYou have a thing due on {tokenStart}dueDate{tokenEnd}.");

            var replacements = new Dictionary<string, string>
            {
                ["dueDate"] = "20/4/2018",
            };

            var expected = "Hi [INVALID_TOKEN:firstName],\nYou have a thing due on 20/4/2018.";

            var content = new DictionaryReplacementNotificationContent(configuration, template, replacements);

            using (var tw = new StringWriter())
            {
                await content.ExecuteAsync(tw);
                var actual = tw.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task LotsOfSimpleTemplate_Test()
        {
            var configuration = DictionaryReplacementNotificationContent.DefaultConfiguration;

            var templateResult = this.Get_LotsOfSimpleTemplate_Test_Template(configuration);

            var template = new StringNotificationContent(templateResult.Item1);
            var replacements = templateResult.Item2;
            var expected = templateResult.Item3;

            var content = new DictionaryReplacementNotificationContent(configuration, template, replacements);

            using (var tw = new StringWriter())
            {
                await content.ExecuteAsync(tw);
                var actual = tw.ToString();

                Assert.Equal(expected, actual);
            }
        }

        private (string, Dictionary<string, string>, string) Get_LotsOfSimpleTemplate_Test_Template(IDictionaryReplacementNotificationContentConfiguration configuration)
        {
            var replacements = new Dictionary<string, string>();

            var tokenStart = configuration.TokenStart;
            var tokenEnd = configuration.TokenEnd;

            var template = new StringBuilder();
            var expected = new StringBuilder();

            for (var i = 0; i < 1000000; i++)
            {
                var replacement = this.CreateReplacement(replacements);
                var token = replacement.Item1;
                var value = replacement.Item2;

                template.Append($"{tokenStart}{token}{tokenEnd}");
                replacements[token] = value;
                expected.Append(value);
            }

            return (template.ToString(), replacements, expected.ToString());
        }

        private (string, string) CreateReplacement(Dictionary<string, string> replacements)
        {
            var token = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            replacements.Add(token, value);

            return (token, value);
        }
    }
}
