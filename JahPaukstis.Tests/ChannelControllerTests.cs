using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using Xunit;

namespace JahPaukstis.Tests
{
    public class ChannelControllerTests
    {
        [Fact]
        public void GetReturnsHttpOk()
        {
            var sut = CreateSut(new MessageStorageMock());
            var result = sut.Get("");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void GetReturnsMessagesFromStorage()
        {
          throw new NotImplementedException();     
        }


        private static ChannelsController CreateSut(IChannelMessageStorage storage)
        {
            var sut = new ChannelsController(storage);
            sut.Request = new HttpRequestMessage();
            sut.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return sut;
        }
    }

    public class MessageStorageMock : IChannelMessageStorage
    {
        private static Dictionary<string, List<ChatMessage>> messagesReceived = new Dictionary<string, List<ChatMessage>>();

        public void Store(string channelId, ChatMessage message)
        {
            if (!messagesReceived.ContainsKey(channelId))
            {
                messagesReceived.Add(channelId, new List<ChatMessage>());
            }
            messagesReceived[channelId].Add(message);
        }

        public IEnumerable<ChatMessage> GetChannelMessages(string channelId)
        {
            IEnumerable<ChatMessage> channelMessages = new ChatMessage[0];
            if (messagesReceived.ContainsKey(channelId))
            {
                channelMessages = messagesReceived[channelId];
            }
            return channelMessages;
        }

    }
}
