using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace JahPaukstis.AcceptanceTests
{
    public class ChannelTests
    {
        private string apiBaseUrl = "http://localhost/JahPaukstis/";

        [Fact]
        public void ServerReturnsNoMessagesOnNonExistingChannel()
        {
            var channelName = Guid.NewGuid().ToString();
            using (var client = CreateHttpClient())
            {
                var response = client.GetAsync(Urls.Channels(channelName)).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var messages = response.Content.ReadAsAsync<object[]>().Result;
                Assert.Empty(messages);
            }
        }

        [Theory]
        [InlineData("labs")]
        [InlineData("hello")]
        public void ServerReturnPostedMessage(string postMessageContent)
        {
            var channelName = Guid.NewGuid().ToString();
            using (var client = CreateHttpClient())
            {
                var postResponse = client.PostAsJsonAsync(Urls.Channels(channelName), new {message = postMessageContent}).Result;
                var getResponse = client.GetAsync(Urls.Channels(channelName)).Result;
                var messages = getResponse.Content.ReadAsAsync<ChatMessageForTests[]>().Result;

                Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
                Assert.Equal(1, messages.Length);
                Assert.Equal(postMessageContent, messages[0].message);
            }
        }

        private HttpClient CreateHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(apiBaseUrl)
            };
        }

    }

    public static class Urls
    {
        public static string Channels(string channelName = "")
        {
            return "Channels/" + channelName;
        }
    }

    public class ChatMessageForTests
    {
        public string message { get; set; }
    }
}
