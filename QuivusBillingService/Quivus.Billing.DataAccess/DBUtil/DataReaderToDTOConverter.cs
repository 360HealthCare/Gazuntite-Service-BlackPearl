using System;
using System.Collections.Generic;
using QuivusModel.Billing.Claim;
using System.Data;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using QuivusUtility.ProjectConstants;
using Quivus.Utility.ProjectConstants;


namespace Quivus.Billing.DataAccess.DBUtil
{
    /// <summary>
    /// Author:     Shahid Munir
    /// Date:       12/16/2011 
    /// </summary>
    public class DataReaderToDTOConverter
    {
        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to populate Claims DTO by reading object reader 
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<ClaimSearchDTO> ClaimSearchResultDTOConverter(IDataReader objReader)
        {
            List<ClaimSearchDTO> LoadClaimsList = null;
            try
            {
                if (objReader != null)
                {
                    LoadClaimsList = new List<ClaimSearchDTO>();
                    {
                        while (objReader.Read())
                        {
                            ClaimSearchDTO LoadClaimsObj = new ClaimSearchDTO();

                            LoadClaimsObj.ClaimID = int.Parse(objReader["ClaimId"].ToString());

                            LoadClaimsObj.PatientID = int.Parse(objReader["PatientID"].ToString());
                            
                            //LoadClaimsObj.PatientName = from user service by given int.Parse(objReader["PatientID"].ToString());

                            LoadClaimsObj.QuivusID = objReader["QuivusId"].ToString();

                            if (string.IsNullOrEmpty(objReader["Carrier1"].ToString()) == false)
                            {
                                LoadClaimsObj.Carrier1 = objReader["Carrier1"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Status1"].ToString()) == false)
                            {
                                LoadClaimsObj.Status1 = objReader["Status1"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Media1"].ToString()) == false)
                            {
                                LoadClaimsObj.Media1 = objReader["Media1"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Batch1"].ToString()) == false)
                            {
                                LoadClaimsObj.Batch1 = int.Parse(objReader["Batch1"].ToString());
                            }

                            if (string.IsNullOrEmpty(objReader["BillDate1"].ToString()) == false)
                            {
                                LoadClaimsObj.BillDate1 = objReader["BillDate1"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["EDIReceiver1"].ToString()) == false)
                            {
                                LoadClaimsObj.EDIReceiver1 = objReader["EDIReceiver1"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Carrier2"].ToString()) == false)
                            {
                                LoadClaimsObj.Carrier2 = objReader["Carrier2"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Status2"].ToString()) == false)
                            {
                                LoadClaimsObj.Status2 = objReader["Status2"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Media2"].ToString()) == false)
                            {
                                LoadClaimsObj.Media2 = objReader["Media2"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Batch2"].ToString()) == false)
                            {
                                LoadClaimsObj.Batch2 = int.Parse(objReader["Batch2"].ToString());
                            }

                            if (string.IsNullOrEmpty(objReader["BillDate2"].ToString()) == false)
                            {
                                LoadClaimsObj.BillDate2 = objReader["BillDate2"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["EDIReceiver2"].ToString()) == false)
                            {
                                LoadClaimsObj.EDIReceiver2 = objReader["EDIReceiver2"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Carrier3"].ToString()) == false)
                            {
                                LoadClaimsObj.Carrier3 = objReader["Carrier3"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Status3"].ToString()) == false)
                            {
                                LoadClaimsObj.Status3 = objReader["Status3"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Media3"].ToString()) == false)
                            {
                                LoadClaimsObj.Media3 = objReader["Media3"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["Batch3"].ToString()) == false)
                            {
                                LoadClaimsObj.Batch3 = int.Parse(objReader["Batch3"].ToString());
                            }

                            if (string.IsNullOrEmpty(objReader["BillDate3"].ToString()) == false)
                            {
                                LoadClaimsObj.BillDate3 = objReader["BillDate3"].ToString();
                            }

                            if (string.IsNullOrEmpty(objReader["EDIReceiver3"].ToString()) == false)
                            {
                                LoadClaimsObj.EDIReceiver3 = objReader["EDIReceiver3"].ToString();
                            }

                            LoadClaimsObj.ProviderId = int.Parse(objReader["ProviderID"].ToString());

                            //LoadClaimsObj.ProviderName = from user service by given int.Parse(objReader["ProviderID"].ToString());

                            LoadClaimsObj.DateCreated = objReader["CreatedAt"].ToString();

                            LoadClaimsList.Add(LoadClaimsObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(BillingConstant.CLAIM_DTO_CONVERSION_ERROR_MSG + ".ClaimSearchResultDTOConverter", ErrorCodes.DTO_CONVERSION_ERROR_PREFIX.ToString());
            }
            return LoadClaimsList;
        }
    }
}
