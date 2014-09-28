using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook
{
    public class AddressAPI
    {
        public void AddAddressToAddressBook(AddressInfo.Address address)
        {
            AddAddress addAddress = new AddAddress();
            addAddress.AddEntryToAddressBook(address);
        }
        public void EditEntryInAddressBook(AddressInfo.Address address)
        {
            EditAddress editAddress = new EditAddress();
            editAddress.EditEntryInAddressBook(address);
        }
        public void DeleteEntryInAddressBook(int uniqueId)
        {
            DeleteAddress deleteAddress = new DeleteAddress();
            deleteAddress.DeleteEntryFromAddressBook(uniqueId);
        }
        public void DeleteEntryInAddressBook(AddressInfo.Address address)
        {
            DeleteAddress deleteAddress = new DeleteAddress();
            deleteAddress.DeleteEntryFromAddressBook(address);
        }
        public AddressInfo.Address GetAddressById(int id)
        {
            return AddressBook.GetAddressById.GetAddressInfoById(id);
        }
        public List<AddressInfo.Address> GetAddressesOrderedByFirstName()
        {
            ManipulateAddress manip = new ManipulateAddress();
            return manip.OrderAddressByFirstName();
        }
        public List<AddressInfo.Address> GetAddressesOrderedByDescendingFirstName()
        {
            ManipulateAddress manip = new ManipulateAddress();
            return manip.OrderAddressDescendingByFirstName();
        }
        public List<AddressInfo.Address> GetAddressesOrderedByLastName()
        {
            ManipulateAddress manip = new ManipulateAddress();
            return manip.OrderAddressByLastName();
        }
        public List<AddressInfo.Address> GetAddressesOrderedByDescendingLastName()
        {
            ManipulateAddress manip = new ManipulateAddress();
            return manip.OrderAddressDescendingByLastName();
        }

    }
}
