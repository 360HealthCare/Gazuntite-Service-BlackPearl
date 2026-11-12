using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestClient.ELabManagerService;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ELabManagerSoapServiceClient client = new ELabManagerSoapServiceClient();
            LabDTO dto = new LabDTO();
            dto.CreatedAt = DateTime.Now;
            dto.CreatedBy = 482;
            dto.Description = "Desv";
            ICDCodeDTO icd = new ICDCodeDTO();
            
        }
    }
}
