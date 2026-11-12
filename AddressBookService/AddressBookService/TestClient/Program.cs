using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestClient.ServiceReference;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference.AddressBookServiceClient client = new ServiceReference.AddressBookServiceClient();
            List<Guid> lst = new List<Guid>();

            lst.Add(new Guid("147395BB-48B1-431D-A394-C645BEE06493"));

            var dsfds = client.GetPartyPrimaryPhoneNoByRegGuid(new Guid("e7d53264-6413-4298-a6c9-f17246e8be40"));

            var vv = client.GetPartyPrimaryPhoneNoByRegGuid(new Guid("37B19DD3-6164-47E3-AF55-E59E7407AA3D"));

            //List<PatientAddressBookInfoDTO> abb = client.GetAddressBookByPartyRegGuid(lst);


            //client.SaveUserAddressBook
        }
    }
}
