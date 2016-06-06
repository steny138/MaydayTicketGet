using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaydayTicketGet
{
    public class Facebook
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class Ticket
    {
        public string date { get; set; }
        public int num { get; set; }
        public string seat { get; set; }
    }

    public class TicketSetting
    {
        public Facebook facebook { get; set; }
        public List<Ticket> tickets { get; set; }
    }
}
