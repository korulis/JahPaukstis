using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JahPaukstis
{
    public class ChannelsController: ApiController
    {
        private readonly IChannelMessageStorage _messageStorage;

        public ChannelsController(IChannelMessageStorage messageStorage)
        {
            _messageStorage = messageStorage;
        }

        public HttpResponseMessage Get(string id)
        {
            var channelMessages = _messageStorage.GetChannelMessages(id);
            return Request.CreateResponse(HttpStatusCode.OK, channelMessages);
        }

        public HttpResponseMessage Post(string id, ChatMessage messagePosted)
        {
            _messageStorage.Store(id, messagePosted);
            return Request.CreateResponse();
        }
    }
}