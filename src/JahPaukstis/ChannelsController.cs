using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JahPaukstis
{
    public class ChannelsController: ApiController
    {
        private static Dictionary<string,List<ChatMessage>> messagesReceived = new Dictionary<string, List<ChatMessage>>();

        public HttpResponseMessage Post(string id, ChatMessage messagePosted)
        {
            if (!messagesReceived.ContainsKey(id))
            {
                messagesReceived.Add(id,new List<ChatMessage>());
            }
            messagesReceived[id].Add(messagePosted);
            return Request.CreateResponse();
        }

        public HttpResponseMessage Get(string id)
        {
            if (messagesReceived.ContainsKey(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, messagesReceived[id]);
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ChatMessage[0]);
        }
    }
}