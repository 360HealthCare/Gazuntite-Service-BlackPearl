using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Labs;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.Physical;

namespace Quivus.Model.QuickInput.MainQuickInput.Physical
{
    [DataContract]
    public class PhysicalDTO : QuivusBaseDTO
    {
        [DataMember]
        public VitalDTO Vitals { get; set; }

        [DataMember]
        public List<HPI_CCDTO> HPI_CC { get; set; }

        [DataMember]
        public List<LabDTO> Labs { get; set; }

        [DataMember]
        public List<ROSDTO> ReviewOfSystem { get; set; }

        [DataMember]
        public List<PhysicalsDTO> Physicals { get; set; }

        [DataMember]
        public List<InHouseLabDTO> InHouseLabs { get; set; }
    }
}
