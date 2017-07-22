using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_with_db
{
    public class Message
    {
        public string Body { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

    }
}
