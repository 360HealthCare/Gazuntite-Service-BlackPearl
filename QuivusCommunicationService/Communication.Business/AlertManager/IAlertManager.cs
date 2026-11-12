using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel;
using QuivusModel.Communication.AlertsDTO;
namespace Communication.Business.AlertManager
{
    public interface IAlertManager
    {

        
        string SaveAlert(AlertDTO oAlertDTO);
    }
}
