using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook
{
    internal static class GetAddressById
    {
        internal static AddressInfo.Address GetAddressInfoById(int id)
        {
            AddressInfo addresses = XmlRead.ReadXML();
            return addresses.address.Where(x => x.uniqueId == id).First();
        }
    }
}
