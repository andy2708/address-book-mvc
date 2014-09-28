using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AddressBook
{
    [XmlRoot("AddressInfo")]
    public class AddressInfo
    {
        public class Address
        {
            /// <summary>
            /// No need to populate this field as it will be overwritten by class library logic
            /// </summary>
            public int uniqueId { get; set; }
            /// <summary>
            /// The users First Name
            /// </summary>
            public string firstName { get; set; }
            /// <summary>
            /// The users Last Name
            /// </summary>
            public string lastName { get; set; }
            /// <summary>
            /// The uses Phone Number
            /// </summary>
            public string phoneNumber { get; set; }
            /// <summary>
            /// The type of phone number: Desk, Cell, Home
            /// </summary>
            public string phoneType { get; set; } //Possibly use an Enum here to provide strong typing. Maybe unnecessary depending on front end.
        }
        [XmlElement("Address")]
        public List<Address> address { get; set; }
     
     }

}
