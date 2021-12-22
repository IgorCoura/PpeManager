using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Domain.Events
{
    public class SetValidityToPpePossession: INotification
    {
        public SetValidityToPpePossession(int ppePossessionId, int ppeCertificationId)
        {
            PpePossessionId = ppePossessionId;
            PpeCertificationId = ppeCertificationId;
        }

        public int PpePossessionId { get; private set; }
        public int PpeCertificationId { get; set; }

    }
}
