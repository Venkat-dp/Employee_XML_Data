using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Employee_XML_Data.Models
{
    public class Manager
    {
        [Key]
        [DataMember]
        public  int ManagerId { get; set; }
        [DataMember]
        public  string Name { get; set; }
        [DataMember]
        public  string EmailId { get; set; }
        [DataMember]
        public  int Salary { get; set; }
        [DataMember]
        public  string Location { get; set; }
    }
}
