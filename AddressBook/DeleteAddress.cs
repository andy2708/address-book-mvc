using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook
{
    internal class DeleteAddress
    {
        internal void DeleteEntryFromAddressBook(int uniqueId)
        {
            //Read the xml file
            AddressInfo addressInfo = XmlRead.ReadXML();
            //Find the desired address
            //Interestingly i tried to extract this out into GetAddressbyID but then remove doesnt work as it loads a copy of the address rather than the actual one.
            AddressInfo.Address address = addressInfo.address.Where(x => x.uniqueId ==uniqueId).First();
            //remove it from the list
            addressInfo.address.Remove(address);
            //Write the xml back
            
            XmlWrite.WriteXML(addressInfo);
        }
        internal void DeleteEntryFromAddressBook(AddressInfo.Address address)
        {
            DeleteEntryFromAddressBook(address.uniqueId);
        }

    }
}
