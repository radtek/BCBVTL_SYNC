using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Web.Models
{
    public class ProcessModel
    {
        public string Module { get; set; }//A, PA, Perso
        public string Code { get; set; }
        public string Name { get; set; }
        public int TimeLoop { get; set; }
        public bool Active { get; set; } = true;


    }
}