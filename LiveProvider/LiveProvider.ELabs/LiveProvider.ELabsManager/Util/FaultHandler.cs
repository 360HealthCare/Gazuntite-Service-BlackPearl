using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiveProvider.Model.Response.LabManagement;
using LiveProvider.Utility.Constant.ProjectConstant;

namespace LiveProvider.ELabsManager.Util
{
    public static class FaultHandler
    {
        public static ServiceResponse HandleFault(string msg, bool isError)
        {
            ServiceResponse response = new ServiceResponse();
            if (isError)
            {
                response.ResponseType = ELabContant.ERROR_MSG;
                response.ResponseMessage = msg;
            }
            else
            {
                response.ResponseType = ELabContant.SUCESS_MSG;
                response.ResponseMessage = msg;
            }
            return response;
        }
    }
}