using System;
using System.Collections.Generic;
using System.Text;

namespace IoTnxt.VodacomEcs.Lib
{
    public class Sam205GeneralAlarmsDto : SamDtoBase
    {
        /// <summary>
        /// 51 - Config Corrupt
        /// </summary>
        public bool? ConfigCorrupt { get; set; }

        /// <summary>
        /// 30 - BRT/RBS Doors
        /// </summary>
        public bool? DoorsAlarm { get; set; }

        /// <summary>
        /// 50 - SAM2 12V DC Fail
        /// </summary>
        public bool? FailAlarm { get; set; }

        /// <summary>
        /// 64 - Modem Fail
        /// </summary>
        public bool? ModemFail { get; set; }

        /// <summary>
        /// 32 - Movement
        /// </summary>
        public bool? Movement { get; set; }

        /// <summary>
        /// 34 - NAV Lights
        /// </summary>
        public bool? NavLights { get; set; }

        /// <summary>
        /// 31 - Panic Alarm
        /// </summary>
        public bool? PanicAlarm { get; set; }

        /// <summary>
        /// 41 - Smoke Alarm
        /// </summary>
        public bool? SmokeAlarm { get; set; }

        /// <summary>
        /// 42 - Smoke Detector
        /// </summary>
        public bool? SmokeDetector { get; set; }

        /// <summary>
        /// 13 - Spare
        /// </summary>
        public bool? Spare1 { get; set; }

        /// <summary>
        /// 14 - Spare
        /// </summary>
        public bool? Spare2 { get; set; }

        /// <summary>
        /// 15 - Spare
        /// </summary>
        public bool? Spare3 { get; set; }

        /// <summary>
        /// 16 - Spare
        /// </summary>
        public bool? Spare4 { get; set; }

        /// <summary>
        /// 17 - Spare
        /// </summary>
        public bool? Spare5 { get; set; }

        /// <summary>
        /// 18 - Spare
        /// </summary>
        public bool? Spare6 { get; set; }

    public override IEnumerable<(string, string, object)> ToPortalParameters(string deviceId)
        {
            throw new NotImplementedException();
        }
    }
}
