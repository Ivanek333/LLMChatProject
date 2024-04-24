using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get ; set; }
        public MessageSender Sender { get; set; }

        public Message()
        {
            Text = string.Empty;
        }
        public Message(string text, MessageSender sender)
        {
            Text = text;
            Sender = sender;
        }
    }
}
