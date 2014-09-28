using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook
{
    internal class ManipulateAddress
    {
        internal List<AddressInfo.Address> OrderAddressByFirstName()
        {
            AddressInfo addressInfo = XmlRead.ReadXML();
            return addressInfo.address.OrderBy(x => x.firstName).ToList();
        }
        internal List<AddressInfo.Address> OrderAddressDescendingByFirstName()
        {
            AddressInfo addressInfo = XmlRead.ReadXML();
            return addressInfo.address.OrderByDescending(x => x.firstName).ToList();
        }
        internal List<AddressInfo.Address> OrderAddressByLastName()
        {
            AddressInfo addressInfo = XmlRead.ReadXML();
            return addressInfo.address.OrderBy(x => x.lastName).ToList();
        }
        internal List<AddressInfo.Address> OrderAddressDescendingByLastName()
        {
            AddressInfo addressInfo = XmlRead.ReadXML();
            return addressInfo.address.OrderByDescending(x => x.lastName).ToList();
        }
        
    }
}
