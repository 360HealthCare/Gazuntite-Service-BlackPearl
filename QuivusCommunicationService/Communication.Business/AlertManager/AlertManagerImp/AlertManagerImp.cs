using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Communication.AlertsDTO;
using Quivus.Utility.Factory;
namespace Communication.Business.AlertManager.AlertManagerImp
{
   public class AlertManagerImp:IAlertManager
    {
       CommunicationDataAccess.AlertDAO oAlertDAO = null;
        public string SaveAlert(AlertDTO oAlertDTO)
        {
            try
            {
                if (oAlertDAO == null)
                {
                    oAlertDAO = (CommunicationDataAccess.AlertDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.AlertDAO));
                }
              return   oAlertDAO.SaveAlert(oAlertDTO).ToString();
            }
            catch (Exception ex)
            {
                
                //throw;
            }
            return "";

        }
    }
}
