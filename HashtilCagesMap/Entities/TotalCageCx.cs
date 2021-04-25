using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilCagesMap.Entities
{
    public class TotalCageCx
    {
        [Display(Name = "מזהה לקוח")]
        public int CxId { get; set; }

        [Display(Name = "לקוח")]
        public string CxName { get; set; }

        [Display(Name = "כלובים בשטח")]
        public int TotalCages { get; set; }



    }
}
