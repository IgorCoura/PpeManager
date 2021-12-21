using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class PpePossession
    {
        public Worker Worker { get; private set; }
        private int WorkerID;      
        public PpeCertification PpeCertification { get; private set; }
        private int PpeCertificationId;
        public DateOnly DeliveryDate { get; private set; }
        public DateOnly Validity { get; private set; }
        public bool Confirmation { get; private set; }
        public string SupportingDocument { get; set; }
        public int Quantity { get; set; }
    }
}
