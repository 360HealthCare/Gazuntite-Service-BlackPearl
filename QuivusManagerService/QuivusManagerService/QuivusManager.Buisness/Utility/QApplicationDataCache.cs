using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;

namespace QuivusManager.Buisness.Utility
{
    public static class QApplicationDataCache
    {
        public static List<UserBasicInfoDTO> USER_BASIC_INFO_CACHE_DATA;
        public static List<RelationshipTypeDTO> RELATIONSHIP_INFO_CACHE_DATA;
        public static List<GenderTypeDTO> GENDER_INFO_CACHE_DATA;
        public static List<StationDTO> STATION_CACHE_DATA;
    }
}
