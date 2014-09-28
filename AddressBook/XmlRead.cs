using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace AddressBook
{
    internal static class XmlRead
    {
        internal static AddressInfo ReadXML()
        {
            //Create an instance to hold the data we are returning
            AddressInfo addressInfo = new AddressInfo();
            //Set the type of deserializer 
            XmlSerializer reader = new XmlSerializer(addressInfo.GetType());
            try
            {
                //Deserialize from the file
                using (StreamReader stream = new StreamReader(@"c:\temp\addressInfo.xml"))
                {
                    addressInfo = (AddressInfo)reader.Deserialize(stream);
                }

            }
            catch (InvalidOperationException ex)
            {
                throw new ApplicationException("There was an error reading from the file " + ex.Message);
            }
            return addressInfo;

        }

    }
}
