using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook
{
    internal class AddAddress
    {
        
        internal void AddEntryToAddressBook(AddressInfo.Address addressInfo)
        {
            //Get all the current addresses from the file
            AddressInfo currentAddresses = XmlRead.ReadXML();
            int newUniqueId;
            //Get the last uniqueId from the xml file and add one to 1. This allows for easier access for editing
            if (currentAddresses.address.Count != 0)
                newUniqueId = (currentAddresses.address.OrderByDescending(x => x.uniqueId).First().uniqueId) + 1;
            else
                newUniqueId = 1;
            addressInfo.uniqueId = newUniqueId;
            //Add the new entry to list of current addresses
            currentAddresses.address.Add(addressInfo);
            XmlWrite.WriteXML(currentAddresses);

        }
    }
}
