using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.Lib.DTO
{
    class Sam100OutputsDto : SamDtoBase
    {
        /// <summary>
        /// Aircon 1 On
        /// </summary>
        public ushort? Aircon1On { get; set; }

        /// <summary>
        /// Aircon 2 On
        /// </summary>
        public ushort? Aircon2On { get; set; }

        /// <summary>
        /// Aircon 3 On
        /// </summary>
        public ushort? Aircon3On { get; set; }

        /// <summary>
        /// Access Relay
        /// </summary>
        public ushort? AccessRelay { get; set; }

        /// <summary>
        /// DC fan On
        /// </summary>
        public ushort? FanOn { get; set; }

        /// <summary>
        /// Freecool on
        /// </summary>
        public ushort? Freecool { get; set; }

        /// <summary>
        /// Siren On
        /// </summary>
        public ushort? SirenOn { get; set; }

        /// <summary>
        /// Strobe On
        /// </summary>
        public ushort? StrobeOn { get; set; }

        public override IEnumerable<(string, string, object)> ToPortalParameters(string deviceId)
        {
            throw new NotImplementedException();
        }
    }
}
