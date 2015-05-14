using System.Collections.Generic;

namespace JahPaukstis
{
    public class InMemoryChannelMessageStorage : IChannelMessageStorage
    {
        private static Dictionary<string,List<ChatMessage>> messagesReceived = new Dictionary<string, List<ChatMessage>>();

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