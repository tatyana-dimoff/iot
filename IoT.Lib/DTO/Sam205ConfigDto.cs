using System.Collections.Generic;

namespace IoT.Lib.DTO
{
    /// <summary>
    /// SAM203 unit configuration data storage.
    /// </summary>
    public class Sam205ConfigDto : SamDtoBase, ISamConfig
    {
        private const string Category = "Config";

        /// <summary>
        /// Aircon Alarm Clear Threshold
        /// <para>Drop in temperature (relative to room temperature and measured at aircon outlet vent) required to CLEAR aircon alarm.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? AirconAlarmClearThreshold { get; set; }

        /// <summary>
        /// Aircon Fail Temp
        /// <para>Vent Temperature setpoint at or above which aircon alarm will be ACTIVE.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? AirconFailTemp { get; set; }

        /// <summary>
        /// Aircon Rest-Time
        /// <para>Time to elapse since switch-off, before switch-on of same aircon will be allowed.</para>
        /// <para>Measurement units: Minutes</para>
        /// </summary>
        public ushort? AirconRestTime { get; set; }

        /// <summary>
        /// Aircons DISABLE Hysteresis
        /// <para>Temperature below Aircons threshold, at which aircons will be inactive (disabled) - overrides aircon thermostat setting.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? AirconsDisableHysteresis { get; set; }

        /// <summary>
        /// Aircons ENABLE Setpoint
        /// <para>Room Temperature setpoint at or above which aircons wil be active (enable) - overrides aircon thermostat setting.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? AirconsEnable { get; set; }

        /// <summary>
        /// Aircon Humidity OFF Setpoint
        /// <para>Relative Humidity below which the DC FAN will stop after starting to reduce humidity (only used when fan speed control is not enabled), during EPCC or "free cooling".</para>
        /// <para>Measurement units: %RH</para>
        /// </summary>
        public ushort? AirconsHumidityOff { get; set; }

        /// <summary>
        /// Aircon Humidity ON Setpoint
        /// <para>Relative Humidity below which the DC FAN will start to run and attempt to keep humidity below this threshold, during EPCC or "free cooling".</para>
        /// <para>Measurement units: %RH</para>
        /// </summary>
        public ushort? AirconsHumidityOn { get; set; }

        /// <summary>
        /// Aircon Service interval
        /// <para>Measurement units: HOURS</para>
        /// </summary>
        public ushort? AirconServiceInterval { get; set; }

        /// <summary>
        /// Aircon Swap Time Setpoint
        /// <para>Time interval (in hours) between aircon SWAP events.</para> 
        /// <para>Measurement units: HOURS</para>
        /// </summary>
        public ushort? AirconSwapTime { get; set; }

        /// <summary>
        /// Aircon Temp Test Delay
        /// <para>Time delay from A/C turn-on until vent temperature checking starts. In MINUTES.</para>
        /// <para>Measurement units: Minutes</para>
        /// </summary>
        public ushort? AirconTempTestDelay { get; set; }

        /// <summary>
        /// Aircon Turn-On Delay
        /// <para>Time to elipse since first aircon switched on, before second sircon is allowed to turn on.</para>
        /// <para>Measurement units: Minutes</para>
        /// </summary>
        public ushort? AirconTurnOnDelay { get; set; }

        /// <summary>
        /// Emergency Fan On Setpoint
        /// <para>Room Temperature setpoint at ot above which DC fan will turn on.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? EmergencyFanOn { get; set; }

        /// <summary>
        /// Fan Cooling Enable Temp Diff
        /// <para>Minimum value that Outside Temp must be lower that Room Temp in order for fan to attempt cooling during EPCC ot "free cooling"</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? FanCoolingEnableTempDiff { get; set; }

        /// <summary>
        /// DC Fan REC Enable Hamidity Diff
        /// <para>Minimum value that Outside Humidity must be lower that Room Humidity in order for fan to attempt cooling during EPCC ot "free cooling"</para>
        /// <para>Measurement units: %RH</para>
        /// </summary>
        public ushort? FanEnableHumidityDiff { get; set; }

        /// <summary>
        /// Fan To Aircon Switchover Temp
        /// <para>Temperature above which Aircon takes over from DC fan, if the fan doess not succeed in keeping temperaature below this level during EPCC ot "free cooling".</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? FanToAirconSwitchoverTemp { get; set; }

        /// <summary>
        /// High Temp Alarm Setpoint
        /// <para>sRoom Temperature setpoint at or above which Hight Temp alarm will be set.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? HighTempAlarm { get; set; }

        /// <summary>
        /// Humidity Clear Trigger Theshold
        /// <para>Humidity (%RH) above which Humidity alarm will be set.</para>
        /// <para>Measurement units: %RH</para>
        /// </summary>
        public ushort? HumidityAlarmClearTheshold { get; set; }

        /// <summary>
        /// Humidity Alarm Trigger Theshold
        /// <para>Humidity (%RH) above which Humidity alarm will be cleared.</para>
        /// <para>Measurement units: %RH</para>
        /// </summary>
        public ushort? HumidityAlarmTriggerTheshold { get; set; }

        /// <summary>
        /// Hybrid Aircon Run Time
        /// <para>Minimum time that aircon will run before turning off during EPCC or "free cooling". </para>
        /// <para>Measurement units: Minutes</para>
        /// </summary>
        public ushort? HybridAirconRunTime { get; set; }

        /// <summary>
        /// Multi A/C Run Setpoint
        /// <para>Room temperature setpoint at or above which all aircons will turn ON.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public int? MultiAirconRun { get; set; }

        /// <summary>
        /// Multi A/C Run Off Hysteresis
        /// <para>No. Of degrees below "Run All" setpoint,at which "Run All" function is canceled. </para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public int? MultiAirconRunOffHysteresis { get; set; }

        // <summary>
        /// Room Temp Regulate Hysteresis
        /// <para>Value below "Regulate Setpoint" at which DC Fan OR Aircons will turn off (only used when fan speed control is not enabled) during EPCC or "free cooling".</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? RoomTempRegulateHysteresis { get; set; }

        public ushort? PiggybackClearThreshold { get; set; }

        public ushort? PiggybackClearTimeConst { get; set; }

        public ushort? PiggybackFailTemp { get; set; }

        /// <summary>
        /// Room Temp Regulate Setpoint
        /// <para>Value around which DC Fan OR Aircon will attempt to regulate Room Temperature during EPCC or "Free Cooling"</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? RoomTempRegulate { get; set; }

        public string SiteName { get; set; }

        /// <summary>
        /// Very High Temp Alarm Setpoint
        /// <para>Room Temperrature setpoint at or above which Very High Temp alarm will be set.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public ushort? VeryHighTempAlarm { get; set; }

        public override IEnumerable<(string, string, object)> ToPortalParameters(string deviceId)
        {
            var result = new List<(string, string, object)>(24);

            if (AirconAlarmClearThreshold.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconAlarmClearThreshold)}", AirconAlarmClearThreshold));
            }

            if (AirconFailTemp.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconFailTemp)}", AirconFailTemp));
            }

            if (AirconRestTime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconRestTime)}", AirconRestTime));
            }

            if (AirconsDisableHysteresis.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconsDisableHysteresis)}", AirconsDisableHysteresis));
            }

            if (AirconsEnable.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconsEnable)}", AirconsEnable));
            }

            if (AirconsHumidityOff.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconsHumidityOff)}", AirconsHumidityOff));
            }

            if (AirconsHumidityOn.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconsHumidityOn)}", AirconsHumidityOn));
            }

            if (AirconServiceInterval.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconServiceInterval)}", AirconServiceInterval));
            }

            if (AirconSwapTime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconSwapTime)}", AirconSwapTime));
            }

            if (AirconTempTestDelay.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconTempTestDelay)}", AirconTempTestDelay));
            }

            if (AirconTurnOnDelay.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconTurnOnDelay)}", AirconTurnOnDelay));
            }

            if (EmergencyFanOn.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(EmergencyFanOn)}", EmergencyFanOn));
            }

            if (FanCoolingEnableTempDiff.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FanCoolingEnableTempDiff)}", FanCoolingEnableTempDiff));
            }

            if (FanEnableHumidityDiff.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FanEnableHumidityDiff)}", FanEnableHumidityDiff));
            }

            if (FanToAirconSwitchoverTemp.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FanToAirconSwitchoverTemp)}", FanToAirconSwitchoverTemp));
            }

            if (HighTempAlarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HighTempAlarm)}", HighTempAlarm));
            }

            if (HumidityAlarmClearTheshold.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HumidityAlarmClearTheshold)}", HumidityAlarmClearTheshold));
            }

            if (HumidityAlarmTriggerTheshold.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HumidityAlarmTriggerTheshold)}", HumidityAlarmTriggerTheshold));
            }

            if (HybridAirconRunTime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HybridAirconRunTime)}", HybridAirconRunTime));
            }

            if (MultiAirconRun.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiAirconRun)}", MultiAirconRun));
            }

            if (MultiAirconRunOffHysteresis.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiAirconRunOffHysteresis)}", MultiAirconRunOffHysteresis));
            }

            if (RoomTempRegulateHysteresis.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RoomTempRegulateHysteresis)}", RoomTempRegulateHysteresis));
            }

            if (RoomTempRegulate.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RoomTempRegulate)}", RoomTempRegulate));
            }

            if (!string.IsNullOrEmpty(SiteName))
            {
                result.Add((deviceId, $"[{Category}] {nameof(SiteName)}", SiteName));
            }

            if (VeryHighTempAlarm.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(VeryHighTempAlarm)}", VeryHighTempAlarm));
            }

            result.Add((deviceId, $"[{Category}] {nameof(Timestamp)}", Timestamp));

            return result;
        }
    }
}
