using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntities.Client
{
    public class ClientListItem
    {
        public int clientId { get; set; }
        public string clientFirstname { get; set; }
        public string clientMiddleName { get; set; }
        public string clientLastName { get; set; }
        public string clientPhone { get; set; }
    }
}