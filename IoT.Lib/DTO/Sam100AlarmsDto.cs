using System.Collections.Generic;

namespace IoT.Lib.DTO
{
    public class Sam100AlarmsDto : SamDtoBase
    {
        protected const string Category = "Alarm";

        /// <summary>
        /// Access Control
        /// </summary>
        public bool? AccessControl { get; set; }

        /// <summary>
        /// AC Phase fail
        /// </summary>
        public bool? AcPhaseFail { get; set; }

        /// <summary>
        /// Aircon 1 fail
        /// </summary>
        public bool? Aircon1Fail { get; set; }

        /// <summary>
        /// Aircon 2 fail
        /// </summary>
        public bool? Aircon2Fail { get; set; }

        /// <summary>
        /// Aircon 3 fail
        /// </summary>
        public bool? Aircon3Fail { get; set; }

        /// <summary>
        /// Aircon 1 On
        /// </summary>
        public bool? Aircon1On { get; set; }

        /// <summary>
        /// Aircon 2 On
        /// </summary>
        public bool? Aircon2On { get; set; }

        /// <summary>
        /// Aircon 3 On
        /// </summary>
        public bool? Aircon3On { get; set; }

        /// <summary>
        /// A/C 1 Power
        /// </summary>
        public bool? Aircon1Power { get; set; }

        /// <summary>
        /// A/C 2 Power
        /// </summary>
        public bool? Aircon2Power { get; set; }

        /// <summary>
        /// A/C 3 Power
        /// </summary>
        public bool? Aircon3Power { get; set; }

        /// <summary>
        /// AircraftLights
        /// </summary>
        public bool? AircraftLights { get; set; }

        /// <summary>
        /// TMA /VSWR fail
        /// </summary>
        public bool? TmaVswrFail { get; set; }

        /// <summary>
        /// Aux 1 Alarm
        /// </summary>
        public bool? Aux1Alarm { get; set; }

        /// <summary>
        /// Aux 2 Alarm
        /// </summary>
        public bool? Aux2Alarm { get; set; }

        /// <summary>
        /// Aux 2 Alarm
        /// </summary>
        public bool? Aux3Alarm { get; set; }

        /// <summary>
        /// Aux 4 Alarm
        /// </summary>
        public bool? Aux4Alarm { get; set; }

        /// <summary>
        /// Aux 5 Alarm
        /// </summary>
        public bool? Aux5Alarm { get; set; }

        /// <summary>
        /// Aux 6 Alarm
        /// </summary>
        public bool? Aux6Alarm { get; set; }

        /// <summary>
        /// Aux 7 Alarm
        /// </summary>
        public bool? Aux7Alarm { get; set; }

        /// <summary>
        /// Aux 8 Alarm
        /// </summary>
        public bool? Aux8Alarm { get; set; }

        /// <summary>
        /// Auxiliary Power
        /// </summary>
        public bool? AuxiliaryPower { get; set; }

        /// <summary>
        /// Batt. Intergrity
        /// </summary>
        public bool? BatteryIntegrity { get; set; }

        /// <summary>
        /// Battery Low
        /// </summary>
        public bool? BatteryLow { get; set; }

        /// <summary>
        /// Config Corrupted
        /// </summary>
        public bool? ConfigCorrupted { get; set; }

        /// <summary>
        /// DC Fan Power
        /// </summary>
        public bool? DcFanPower { get; set; }

        /// <summary>
        /// DC Fan On
        /// </summary>
        public bool? DcFanOn { get; set; }

        /// <summary>
        /// Free cooling On
        /// </summary>
        public bool? FreeCoolingOn { get; set; }

        /// <summary>
        /// Escom Supply
        /// </summary>
        public bool? EscomSupply { get; set; }

        /// <summary>
        /// High Humidity
        /// </summary>
        public bool? HighHumidity { get; set; }

        /// <summary>
        /// High Temperature
        /// </summary>
        public bool? HighTemperature { get; set; }

        /// <summary>
        /// Humidity Probe 1
        /// </summary>
        public bool? HumidityProbe1 { get; set; }

        /// <summary>
        /// Humidity Probe 2
        /// </summary>
        public bool? HumidityProbe2 { get; set; }

        /// <summary>
        /// Intruder (room)
        /// </summary>
        public bool? Intruder { get; set; }

        /// <summary>
        /// AC Mains fail
        /// </summary>
        public bool? MainsFail { get; set; }

        /// <summary>
        /// Movement (room)
        /// </summary>
        public bool? MovementRoom { get; set; }

        /// <summary>
        /// Movement (yard)
        /// </summary>
        public bool? MovementYard { get; set; }

        /// <summary>
        /// Multi-A/C Run
        /// </summary>
        public bool? MultiAirconRun { get; set; }

        /// <summary>
        /// Panic Alarm
        /// </summary>
        public bool? PanicAlarm { get; set; }

        /// <summary>
        /// Pix 1
        /// </summary>
        public bool? Pix1 { get; set; }

        /// <summary>
        /// Pix 2
        /// </summary>
        public bool? Pix2 { get; set; }

        /// <summary>
        /// Pix 3
        /// </summary>
        public bool? Pix3 { get; set; }

        /// <summary>
        /// Pix 4
        /// </summary>
        public bool? Pix4 { get; set; }

        /// <summary>
        /// Pix 5
        /// </summary>
        public bool? Pix5 { get; set; }

        /// <summary>
        /// Pix 6
        /// </summary>
        public bool? Pix6 { get; set; }

        /// <summary>
        /// Pix 7
        /// </summary>
        public bool? Pix7 { get; set; }

        /// <summary>
        /// Pix 8
        /// </summary>
        public bool? Pix8 { get; set; }

        /// <summary>
        /// Pix 9
        /// </summary>
        public bool? Pix9 { get; set; }

        /// <summary>
        /// Pix 10
        /// </summary>
        public bool? Pix10 { get; set; }

        /// <summary>
        /// Pix 11
        /// </summary>
        public bool? Pix11 { get; set; }

        /// <summary>
        /// Pix 12
        /// </summary>
        public bool? Pix12 { get; set; }

        /// <summary>
        /// Pix 13
        /// </summary>
        public bool? Pix13 { get; set; }

        /// <summary>
        /// Pix 14
        /// </summary>
        public bool? Pix14 { get; set; }

        /// <summary>
        /// Pix 15
        /// </summary>
        public bool? Pix15 { get; set; }

        /// <summary>
        /// Pix 16
        /// </summary>
        public bool? Pix16 { get; set; }

        /// <summary>
        /// Rectifer Module
        /// </summary>
        public bool? RectiferModule { get; set; }

        /// <summary>
        /// Rectifer System
        /// </summary>
        public bool? RectiferSystem { get; set; }

        /// <summary>
        /// Smoke Alarm
        /// </summary>
        public bool? SmokeAlarm { get; set; }

        public override IEnumerable<(string, string, object)> ToPortalParameters(string deviceId)
        {
            var result = new List<(string, string, object)>();

            if (AccessControl.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AccessControl)}", AccessControl));
            }

            if (AcPhaseFail.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AcPhaseFail)}", AcPhaseFail));
            }

            if (Aircon1Fail.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon1Fail)}", Aircon1Fail));
            }

            if (Aircon2Fail.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon2Fail)}", Aircon2Fail));
            }

            if (Aircon3Fail.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon3Fail)}", Aircon3Fail));
            }

            if (Aircon1On.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon1On)}", Aircon1On));
            }

            if (Aircon2On.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon2On)}", Aircon2On));
            }

            if (Aircon3On.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon3On)}", Aircon3On));
            }

            if (AircraftLights.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AircraftLights)}", AircraftLights));
            }

            if (Aircon1Power.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon1Power)}", Aircon1Power));
            }

            if (Aircon2Power.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon2Power)}", Aircon2Power));
            }

            if (Aircon3Power.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon3Power)}", Aircon3Power));
            }

            if (TmaVswrFail.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(TmaVswrFail)}", TmaVswrFail));
            }

            if (Aux1Alarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aux1Alarm)}", Aux1Alarm));
            }

            if (Aux2Alarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aux2Alarm)}", Aux2Alarm));
            }

            if (Aux3Alarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aux3Alarm)}", Aux3Alarm));
            }

            if (Aux4Alarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aux4Alarm)}", Aux4Alarm));
            }

            if (Aux5Alarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aux5Alarm)}", Aux5Alarm));
            }

            if (Aux6Alarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aux6Alarm)}", Aux6Alarm));
            }

            if (Aux7Alarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aux7Alarm)}", Aux7Alarm));
            }

            if (Aux8Alarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aux8Alarm)}", Aux8Alarm));
            }

            if (AuxiliaryPower.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AuxiliaryPower)}", AuxiliaryPower));
            }

            if (BatteryIntegrity.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(BatteryIntegrity)}", BatteryIntegrity));
            }

            if (BatteryLow.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(BatteryLow)}", BatteryLow));
            }

            if (ConfigCorrupted.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(ConfigCorrupted)}", ConfigCorrupted));
            }

            if (DcFanPower.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(DcFanPower)}", DcFanPower));
            }

            if (DcFanOn.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(DcFanOn)}", DcFanOn));
            }

            if (FreeCoolingOn.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FreeCoolingOn)}", FreeCoolingOn));
            }

            if (EscomSupply.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(EscomSupply)}", EscomSupply));
            }

            if (HighHumidity.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HighHumidity)}", HighHumidity));
            }

            if (HighTemperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HighTemperature)}", HighTemperature));
            }

            if (HumidityProbe1.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HumidityProbe1)}", HumidityProbe1));
            }

            if (HumidityProbe2.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HumidityProbe2)}", HumidityProbe2));
            }

            if (Intruder.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Intruder)}", Intruder));
            }

            if (MainsFail.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MainsFail)}", MainsFail));
            }

            if (MovementRoom.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MovementRoom)}", MovementRoom));
            }

            if (MovementYard.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MovementYard)}", MovementYard));
            }

            if (MultiAirconRun.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiAirconRun)}", MultiAirconRun));
            }

            if (PanicAlarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(PanicAlarm)}", PanicAlarm));
            }

            if (Pix1.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix1)}", Pix1));
            }

            if (Pix2.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix2)}", Pix2));
            }

            if (Pix3.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix3)}", Pix3));
            }

            if (Pix4.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix4)}", Pix4));
            }

            if (Pix5.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix5)}", Pix5));
            }

            if (Pix6.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix6)}", Pix6));
            }

            if (Pix7.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix7)}", Pix7));
            }

            if (Pix8.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix8)}", Pix8));
            }

            if (Pix9.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix9)}", Pix9));
            }

            if (Pix10.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix10)}", Pix10));
            }

            if (Pix11.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix11)}", Pix11));
            }

            if (Pix12.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix12)}", Pix12));
            }

            if (Pix13.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix13)}", Pix13));
            }

            if (Pix14.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix14)}", Pix14));
            }

            if (Pix15.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix15)}", Pix15));
            }

            if (Pix16.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Pix16)}", Pix16));
            }

            if (RectiferModule.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RectiferModule)}", RectiferModule));
            }

            if (RectiferSystem.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RectiferSystem)}", RectiferSystem));
            }

            if (SmokeAlarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(SmokeAlarm)}", SmokeAlarm));
            }

            result.Add((deviceId, $"[{Category}] {nameof(Timestamp)}", Timestamp));

            return result;
        }
    }
}
