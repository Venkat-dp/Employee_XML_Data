using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Employee_XML_Data.Models
{
    [DataContract]
    //[XmlRoot("Request")]
    public  class Employee
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public int MgrId { get; set; }

        [ForeignKey("MgrId")]
        [DataMember]
        public virtual Manager Manager { get; set; }
    }
}
