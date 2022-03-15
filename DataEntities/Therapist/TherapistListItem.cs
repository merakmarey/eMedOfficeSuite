using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Therapist
{
    public class TherapistListItem
    {
        public int therapistId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string phone { get; set; }
        public int pendingRequirements { get; set; }

    }
}
