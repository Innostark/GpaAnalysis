using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMD.Web.Models
{
    public class PaypalPaymentModel
    {
        public string PayerId { get; set; }

        public string TxnString { get; set; }
        public string  AmountPaid { get; set; }

    }
}