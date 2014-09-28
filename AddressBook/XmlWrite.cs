using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace AddressBook
{
    internal static class XmlWrite
    {
        internal static void WriteXML(AddressInfo addressInfo)
        {
            //Create a new Serializer
            XmlSerializer xmlWriter = new XmlSerializer(addressInfo.GetType());
            //Add an empty namespace so we dont get weird tags
            //These can throw exceptions if not handled correctly so better to creeate the file correctly first time.
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("","");
            try
            {
                using (StreamWriter file = new StreamWriter(@"c:\temp\addressInfo.xml"))
                {
                    //Serialize and write the file to disk
                    xmlWriter.Serialize(file, addressInfo, ns);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error writing the file " + ex.Message);
            }
           
            
           
        }
    }
}
