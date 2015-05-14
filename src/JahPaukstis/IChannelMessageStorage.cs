using System.Collections.Generic;

namespace JahPaukstis
{
    public interface IChannelMessageStorage
    {
        void Store(string channelId, ChatMessage message);
        IEnumerable<ChatMessage> GetChannelMessages(string channelId);
    }
}