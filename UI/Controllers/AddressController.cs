using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class AddressController : Controller
    {
        //Declare this out here as we use it everywhere
        AddressBook.AddressAPI api = new AddressBook.AddressAPI();

        //This is the default landing page
        public ActionResult Index()
        {
            //set the sort direction and column here
            ViewBag.sort = "LastName";
            ViewBag.direction = "asc";
            return View(api.GetAddressesOrderedByLastName());
        }
        //Post request to handle refreshing the list of address's
        [HttpPost]
        public ActionResult updateAddressList()
        {
            return PartialView("_addressDetails", api.GetAddressesOrderedByLastName());
        }
        //Post request to handle adding an entry.
        //this is fired when the user clicks add from the dialog window
        [HttpPost]
        public JsonResult addEntry(string firstName, string lastName, string phoneType, string phoneNumber)
        {
            //make and address object from the data passed thru
            AddressBook.AddressInfo.Address address = makeAddress(firstName, lastName, phoneType, phoneNumber, null);
            try
            {
                api.AddAddressToAddressBook(address);
                //Return some Json so the end user know what is going on
                return Json(new
                    {
                        message = "Success",
                        data = "Successfully added user to the address book"
                    });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = "Error",
                    data = ex.Message
                });
            }
        }
        /// <summary>
        /// This gets the address details of an object when specifiying and id
        /// It is used to populate the dialog when the user clicks edit or delete
        /// </summary>
        /// <param name="id">the id of the address to look up</param>
        /// <returns>json details of the address</returns>
        [HttpPost]
        public JsonResult getAddressDetails(int id)
        {
            AddressBook.AddressInfo.Address addressToEdit = api.GetAddressById(id);
            //return the address as a json object for easy use on the client
            return Json(new
            {
                firstName = addressToEdit.firstName,
                lastName = addressToEdit.lastName,
                phoneType = addressToEdit.phoneType,
                phoneNumber = addressToEdit.phoneNumber
            });
        }
        /// <summary>
        /// This post request is called when the user clicks Save Changes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneType"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>json result letting the user know whether the edit worked or not</returns>
        [HttpPost]
        public JsonResult editEntry(int id, string firstName, string lastName, string phoneType, string phoneNumber)
        {
            AddressBook.AddressInfo.Address address = makeAddress(firstName, lastName, phoneType, phoneNumber, id);
            try
            {
                api.EditEntryInAddressBook(address);
                return Json(new
                {
                    message = "Success",
                    data = "Successully Updated record"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = "Error",
                    data = ex.Message
                });
            }
           
        }

        /// <summary>
        /// THis post request is called when the user clicks really delte
        /// </summary>
        /// <param name="id">The id that we want to delete</param>
        /// <returns>json result letting the user know whether the delete worked or not</returns>
        [HttpPost]
        public JsonResult deleteEntry(int id)
        {
            try
            {
                api.DeleteEntryInAddressBook(id);
                return Json(new
                {
                    message = "Success",
                    data = "Successfully Deleted address"
                });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = "Error",
                    data = ex.Message
                });
            }
        }
        /// <summary>
        /// This post request handles sorting.
        /// It is used when the user clicks the header above first name ore last name
        /// </summary>
        /// <param name="column">the column on the UI that was clicked.</param>
        /// <param name="direction">the new direction we will sort by</param>
        /// <returns>a partial view with the data sorted as we like</returns>
        [HttpPost]
        public ActionResult sortList(string column, string direction)
        {
            ViewBag.sort = column; //set the sort column so next time the user gets the right sort data
            ViewBag.direction = direction;//set the direction. this ensures the next sort will still work as desired
            
            if (column == "FirstName")
            {
                if (direction == "asc")
                {
                    return PartialView("_addressDetails", api.GetAddressesOrderedByFirstName());
                }
                else
                {
                    return PartialView("_addressDetails", api.GetAddressesOrderedByDescendingFirstName());    
                }
            }
            else if(column == "LastName")
            {
                if( direction == "asc")
                {
                    return PartialView("_addressDetails", api.GetAddressesOrderedByLastName());
                }
                else
                {
                    return PartialView("_addressDetails", api.GetAddressesOrderedByDescendingLastName());
                }
            }
            else
                return PartialView("_addressDetails", api.GetAddressesOrderedByFirstName());
        }

        /// <summary>
        /// This builds an address object for us from the variables passed thru a post request
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneType"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="id">a nullable value sometimes we dont know the id (when we create a new address for example)</param>
        /// <returns></returns>
        private AddressBook.AddressInfo.Address makeAddress(string firstName, string lastName, string phoneType, string phoneNumber, int? id)
        {
            AddressBook.AddressInfo.Address address = new AddressBook.AddressInfo.Address();
            address.firstName = firstName;
            address.lastName = lastName;
            address.phoneType = phoneType;
            address.phoneNumber = phoneNumber;
            if (id.HasValue)
                address.uniqueId = id.Value;
            return address;
        }

    }
}
