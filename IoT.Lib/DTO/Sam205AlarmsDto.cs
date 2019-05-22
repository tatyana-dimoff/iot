using System.Collections.Generic;

namespace IoT.Lib.DTO
{
    public class Sam205AlarmsDto : SamDtoBase
    {
        private const string Category = "Alarm";

        /// <summary>
        /// 43 - Aircon 1 Fail
        /// </summary>
        public bool? Aircon1Fail { get; set; }

        /// <summary>
        /// 44 - Aircon 2 Fail
        /// </summary>
        public bool? Aircon2Fail { get; set; }

        /// <summary>
        /// 35 - Aircon 1 Power
        /// </summary>
        public bool? Aircon1Power { get; set; }

        /// <summary>
        /// 36 - Aircon 2 Power
        /// </summary>
        public bool? Aircon2Power { get; set; }

        /// <summary>
        /// 39 - DC Fan Power Alm
        /// </summary>
        public bool? FanPowerAlarm { get; set; }

        /// <summary>
        /// 47 - High Temp
        /// </summary>
        public bool? HighTemperature { get; set; }

        /// <summary>
        /// 63 - Humidity High
        /// </summary>
        public bool? HumidityHigh { get; set; }

        /// <summary>
        /// 62 - Humidity Probe
        /// </summary>
        public bool? HumidityProbe { get; set; }

        /// <summary>
        /// 49 - Multi Aircon Run
        /// </summary>
        public bool? MultiAirconRun { get; set; }

        /// <summary>
        /// 73 - A/C Service Due
        /// </summary>
        public bool? ServiceDue { get; set; }

        /// <summary>
        /// 48 - Very High Temp
        /// </summary>
        public bool? VeryHighTemperature { get; set; }

        /// <summary>
        /// 51 - Config Corrupt
        /// </summary>
        public bool? ConfigCorrupt { get; set; }

        /// <summary>
        /// 30 - BRT/RBS Door
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
            var result = new List<(string, string, object)>();

            if (Aircon1Fail.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon1Fail)}", Aircon1Fail));
            }

            if (Aircon2Fail.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon2Fail)}", Aircon2Fail));
            }

            if (Aircon1Power.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon1Power)}", Aircon1Power));
            }

            if (Aircon2Power.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon2Power)}", Aircon2Power));
            }

            if (FanPowerAlarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FanPowerAlarm)}", FanPowerAlarm));
            }

            if (HighTemperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HighTemperature)}", HighTemperature));
            }

            if (HumidityHigh.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HumidityHigh)}", HumidityHigh));
            }

            if (HumidityProbe.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HumidityProbe)}", HumidityProbe));
            }

            if (MultiAirconRun.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiAirconRun)}", MultiAirconRun));
            }

            if (ServiceDue.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(ServiceDue)}", ServiceDue));
            }

            if (VeryHighTemperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(VeryHighTemperature)}", VeryHighTemperature));
            }

            if (ConfigCorrupt.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(ConfigCorrupt)}", ConfigCorrupt));
            }

            if (DoorsAlarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(DoorsAlarm)}", DoorsAlarm));
            }

            if (FailAlarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FailAlarm)}", FailAlarm));
            }

            if (ModemFail.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(ModemFail)}", ModemFail));
            }

            if (Movement.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Movement)}", Movement));
            }

            if (NavLights.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(NavLights)}", NavLights));
            }

            if (PanicAlarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(PanicAlarm)}", PanicAlarm));
            }

            if (SmokeAlarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(SmokeAlarm)}", SmokeAlarm));
            }

            if (SmokeDetector.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(SmokeDetector)}", SmokeDetector));
            }

            if (Spare1.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Spare1)}", Spare1));
            }

            if (Spare2.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Spare2)}", Spare2));
            }

            if (Spare3.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Spare3)}", Spare3));
            }

            if (Spare4.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Spare4)}", Spare4));
            }

            if (Spare5.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Spare5)}", Spare5));
            }

            if (Spare6.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Spare6)}", Spare6));
            }

            result.Add((deviceId, $"[{Category}] {nameof(Timestamp)}", Timestamp));

            return result;
        }
    }
}
