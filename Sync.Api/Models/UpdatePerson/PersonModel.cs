using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Api.Models.UpdatePerson
{
    public class PersonModel
    {
        public decimal PERSON_ID { get; set; }
        public string CODE { get; set; }
        public string PERSON_NAME { get; set; }
        public string BASE64 { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATED_NAME { get; set; }
        public bool IS_UPDATE_TTDH { get; set; }
        public bool IS_A { get; set; }
    }
}