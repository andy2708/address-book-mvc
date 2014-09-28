using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook
{
    internal class EditAddress
    {
      
        internal void EditEntryInAddressBook(AddressInfo.Address newAddress)
        {
            AddressInfo addressInfo = XmlRead.ReadXML();
            //Select the old address and store that value so we can remove it later on.
            //Interestingly i tried to extract this out into GetAddressbyID but then remove doesnt work as it loads a copy of the address rather than the actual one.
            AddressInfo.Address originalAddress = addressInfo.address.Where(x => x.uniqueId == newAddress.uniqueId).First(); 
            addressInfo.address.Remove(originalAddress);
            //Add the new updated address to the list
            addressInfo.address.Add(newAddress);
            //Remove the old Address from list
           
            //Write back to the file
            XmlWrite.WriteXML(addressInfo);
        }
        
    }
}
