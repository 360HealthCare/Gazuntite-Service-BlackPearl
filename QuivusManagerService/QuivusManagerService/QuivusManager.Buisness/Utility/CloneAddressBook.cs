using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuivusManager.Buisness.Utility
{
    public class CloneAddressBook
    {
        #region Convert Manager AddressBook TO AB Book
        public static void CloneManagerAdressBookTOMainAB(IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook
           , IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook)
        {

            #region Copy Main Properties
            CopyUtilities.CloneObjectIgnoreList(ManagerAddressBook, ref ABAddressBook);
            #endregion

            #region Copy Addresses List
            CloneManagerAddressToABAddress(ManagerAddressBook, ref ABAddressBook);
            #endregion

            #region Copy Phones List
            CloneManagerPhoneToABPhone(ManagerAddressBook.PhoneDTOlist, ABAddressBook);
            #endregion

            #region Copy Deleted Address
            CloneManagerDeletedAddressToABDeletedAddress(ManagerAddressBook, ref  ABAddressBook);
            #endregion

            #region Copy Deleted Phone
            CloneManagerDeletedPhoneToABDeletedPhone(ManagerAddressBook, ref  ABAddressBook);
            #endregion

        }

       

        public static void CloneManagerDeletedPhoneToABDeletedPhone(IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook, ref IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook)
        {
            if (ManagerAddressBook.DeletedPhoneIds != null)
            {
                if (ABAddressBook.DeletedPhoneIds == null)
                {
                    ABAddressBook.DeletedPhoneIds = new List<int>();
                }
                foreach (int src in ManagerAddressBook.DeletedPhoneIds)
                {
                    int dest = 0;
                    CopyUtilities.CloneObjectIgnoreList(src, ref dest);
                    ABAddressBook.DeletedPhoneIds.Add(dest);
                }
            }
        }

        public static void CloneManagerDeletedAddressToABDeletedAddress(IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook, ref IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook)
        {
            if (ManagerAddressBook.DeletedAddressIds != null)
            {
                if (ABAddressBook.DeletedAddressIds == null)
                {
                    ABAddressBook.DeletedAddressIds = new List<int>();
                }
                foreach (int src in ManagerAddressBook.DeletedAddressIds)
                {
                    int dest = 0;
                    CopyUtilities.CloneObjectIgnoreList(src, ref dest);
                    ABAddressBook.DeletedAddressIds.Add(dest);
                }
            }
        }

        public static void CloneManagerAddressToABAddress(IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook, ref IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook)
        {
            if (ManagerAddressBook.AddressDTOlist != null)
            {
                if (ABAddressBook.AddressDTOlist == null)
                {
                    ABAddressBook.AddressDTOlist = new List<IntegratedServies.AddressBookService.AddressDTO>();
                }
                foreach (IntegratedServies.UserManagerService.AddressDTO src in ManagerAddressBook.AddressDTOlist)
                {
                    IntegratedServies.AddressBookService.AddressDTO dest = new IntegratedServies.AddressBookService.AddressDTO();

                    CopyUtilities.CloneObjectIgnoreList(src, ref dest);
                    ABAddressBook.AddressDTOlist.Add(dest);
                }
            }
        }


        public static void CloneManagerPhoneToABPhone(List<IntegratedServies.UserManagerService.PhoneDTO> ManagerPhone, List<IntegratedServies.AddressBookService.PhoneDTO> ABPhone)
        {
            if (ManagerPhone != null)
            {
                if (ABPhone == null)
                {
                    ABPhone = new List<IntegratedServies.AddressBookService.PhoneDTO>();
                }
                foreach (IntegratedServies.UserManagerService.PhoneDTO src in ManagerPhone)
                {
                    IntegratedServies.AddressBookService.PhoneDTO dest = new IntegratedServies.AddressBookService.PhoneDTO();
                    CopyUtilities.CloneObjectIgnoreList(src, ref dest);
                    ABPhone.Add(dest);
                }
            }
        }

        private static void CloneManagerPhoneToABPhone(List<IntegratedServies.UserManagerService.PhoneDTO> ManagerPhone, IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook)
        {
            if (ManagerPhone != null && ABAddressBook != null)
            {
                if (ABAddressBook.PhoneDTOlist == null)
                {
                    ABAddressBook.PhoneDTOlist = new List<IntegratedServies.AddressBookService.PhoneDTO>();
                }
                foreach (IntegratedServies.UserManagerService.PhoneDTO src in ManagerPhone)
                {
                    IntegratedServies.AddressBookService.PhoneDTO dest = new IntegratedServies.AddressBookService.PhoneDTO();
                    CopyUtilities.CloneObjectIgnoreList(src, ref dest);
                    ABAddressBook.PhoneDTOlist.Add(dest);
                }
            }
        } // End OF Method
        #endregion

        public static void CloneMainABTOManagerAdressBook(IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook
            , IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook)
        {

            #region Copy Main Properties
            CopyUtilities.CloneObjectIgnoreList(ABAddressBook,  ref ManagerAddressBook);
            #endregion

            #region Copy Addresses List
            CloneABAddressToManagerAddress(ABAddressBook,  ManagerAddressBook);
            #endregion

            #region Copy Phones List
            CloneABPhoneManagerPhone(ABAddressBook.PhoneDTOlist,  ManagerAddressBook);
            #endregion

            #region Copy Deleted Address
            CloneABDeletedAddressToManagerDeletedAddress(ABAddressBook,  ManagerAddressBook);
            #endregion

            #region Copy Deleted Phone
            CloneABDeletedPhoneToManagerDeletedPhone(ABAddressBook, ManagerAddressBook);
            #endregion

        } // End OF Method

        public static void CloneABDeletedPhoneToManagerDeletedPhone(IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook,
            IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook)
        {
            if (ABAddressBook.DeletedPhoneIds != null)
            {
                if (ManagerAddressBook.DeletedPhoneIds == null)
                {
                    ManagerAddressBook.DeletedPhoneIds = new List<int>();
                }
                foreach (int src in ABAddressBook.DeletedPhoneIds)
                {
                    int dest = 0;
                    CopyUtilities.CloneObjectIgnoreList(src, ref dest);
                    ManagerAddressBook.DeletedPhoneIds.Add(dest);
                }
            }
        }

        public static void CloneABDeletedAddressToManagerDeletedAddress(IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook,
            IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook)
        {
            if (ABAddressBook.DeletedAddressIds != null)
            {
                if (ManagerAddressBook.DeletedAddressIds == null)
                {
                    ManagerAddressBook.DeletedAddressIds = new List<int>();
                }
                foreach (int src in ABAddressBook.DeletedAddressIds)
                {
                    int dest = 0;
                    CopyUtilities.CloneObjectIgnoreList(src, ref dest);
                    ManagerAddressBook.DeletedAddressIds.Add(dest);
                }
            }
        }

        public static void CloneABAddressToManagerAddress(IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook,
            IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook)
        {
            if (ABAddressBook.AddressDTOlist != null)
            {
                if (ManagerAddressBook.AddressDTOlist == null)
                {
                    ManagerAddressBook.AddressDTOlist = new List<IntegratedServies.UserManagerService.AddressDTO>();
                }
                foreach (IntegratedServies.AddressBookService.AddressDTO src in ABAddressBook.AddressDTOlist)
                {
                    IntegratedServies.UserManagerService.AddressDTO dest = new IntegratedServies.UserManagerService.AddressDTO();

                    CopyUtilities.CloneObjectIgnoreList(src, ref dest);
                    ManagerAddressBook.AddressDTOlist.Add(dest);
                }
            }
        }

        public static void CloneABPhoneManagerPhone(List<IntegratedServies.AddressBookService.PhoneDTO> ABPhone
            , IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook )
        {
            if (ABPhone != null)
            {
                if (ManagerAddressBook.PhoneDTOlist == null)
                {
                    ManagerAddressBook.PhoneDTOlist = new List<IntegratedServies.UserManagerService.PhoneDTO>();
                }
                foreach (IntegratedServies.AddressBookService.PhoneDTO src in ABPhone)
                {
                    IntegratedServies.UserManagerService.PhoneDTO dest = new IntegratedServies.UserManagerService.PhoneDTO();
                    CopyUtilities.CloneObjectIgnoreList(src, ref dest);
                    ManagerAddressBook.PhoneDTOlist.Add(dest);
                }
            }
        }


        internal static void CloneABPhoneManagerPhone(IntegratedServies.AddressBookService.PhoneDTO phoneDTO, IntegratedServies.UserManagerService.PhoneDTO ManagerPhoneAB)
        {
            if (phoneDTO != null)
            {
                if (ManagerPhoneAB == null)
                {
                    ManagerPhoneAB = new IntegratedServies.UserManagerService.PhoneDTO();
                }
                CopyUtilities.CloneObjectIgnoreList(phoneDTO, ref ManagerPhoneAB);
            }
        }

    }
}
