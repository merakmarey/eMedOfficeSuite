using DataEntities.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataServices
{
    public partial class client
    {
        public Client ToClient()
        {
            return new Client(this);
        }
        public void FromForm(FormCollection model)
        {
            
        }
    }
}