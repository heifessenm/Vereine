using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formular2Spg.Model.Source
{
    public class RootEntry
    {
        public string Browser { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Created_At { get; set; }
        public string Currency { get; set; }
        public string Device { get; set; }
        public string Form_Id { get; set; }
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Is_Favourite { get; set; }
        public string Payment_Method { get; set; }
        public string Payment_Status { get; set; }
        public string Payment_Total { get; set; }
        public string Payment_Type { get; set; }
        public FormularEntryResponse Response { get; set; }
        public string Serial_Number { get; set; }
        public string Source_Url { get; set; }
        public string Status { get; set; }
        public string Total_Paid { get; set; }
        public DateTime Updated_At { get; set; }
        public string User_Id { get; set; }
        public User_Inputs User_Inputs { get; set; }
    }

}
