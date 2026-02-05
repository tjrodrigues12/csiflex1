using Newtonsoft.Json;
using System;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class Settings
    {
        public string DatabaseServer { get; set; }

        public string CSIFlexUserName { get; set; }

        public string CSFlexDbPort { get; set; }

        public string CSIFlexPassword { get; set; }

        public string ApiUrl { get; set; }

        public string ApiUserName { get; set; }

        public string ApiPassword { get; set; }

        public string ApiCompanyName{ get; set; }

        [JsonIgnore]
        public bool ConfigurationComplete => DatabaseServer.HasValue()
            && CSIFlexUserName.HasValue()
            && CSFlexDbPort.HasValue()
            && CSIFlexPassword.HasValue()
            && DatabaseServer.HasValue()
            && ApiUrl.HasValue()
            && ApiUserName.HasValue()
            && ApiPassword.HasValue()
            && ApiCompanyName.HasValue();
    }
}

