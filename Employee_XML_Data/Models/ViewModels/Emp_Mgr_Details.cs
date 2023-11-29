using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Employee_XML_Data.Models.ViewModels
{
    [DataContract(Namespace = "EmployeeDetails")]
    //[XmlRoot("Request")]
    public class Emp_Mgr_Details
    {
        [DataMember]
        public int EmployeeId { get; set; }
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
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string EmailId { get; set; }
        [DataMember]
        public int Salary { get; set; }
        [DataMember]
        public string Location { get; set; }

    }
}
