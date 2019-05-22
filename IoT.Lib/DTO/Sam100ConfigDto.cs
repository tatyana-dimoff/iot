using System.Collections.Generic;

namespace IoT.Lib.DTO
{
    /// <summary>
    /// SAM100 unit configuration data storage.
    /// </summary>
    public class Sam100ConfigDto : SamDtoBase, ISamConfig
    {
        private const string Category = "Config";

        /// <summary> 
        /// Aircon Alarm Clear Threshold
        /// <para>Drop in temperature (relative to room temperature and measured at aircon outlet vent) required to CLEAR aircon alarm.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? AirconAlarmClearTemp { get; set; }

        /// <summary> 
        /// Aircons DISABLE hysteresis
        /// <para>Temperature below Aircons threshold, at which aircons will be inactive (disabled) - overrides aircon thermostat setting.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? AirconDisableHysteresis { get; set; }

        /// <summary> 
        /// Aircons ENABLE setpoint
        /// <para>Temperature at or above which aircons wil be active (enable) - overrides aircon thermostat setting.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? AirconEnableSetpoint { get; set; }

        /// <summary> 
        /// Aircon rest-time
        /// <para>Time to elapse since switch-off, before switch-on of same aircon will be allowed.</para>
        /// <para>Measurement units: Seconds</para>
        /// </summary>  
        public ushort? AirconRestTime { get; set; }

        /// <summary> 
        /// Aircon Swap Time Default
        /// <para>Setting used when A/C RESET is held in for 5 seconds (default restored).</para>
        /// <para>Measurement units: Hours</para>
        /// </summary>  
        public ushort? AirconSwapTimeDefault { get; set; }

        /// <summary> 
        /// Aircon Swap Time High Limit
        /// <para>Highest possible setting allowed by keypad adjust.</para>
        /// <para>Measurement units: Hours</para>
        /// </summary>  
        public ushort? AirconSwapTimeHighLimit { get; set; }

        /// <summary> 
        /// Aircon Swap Time Low Limit
        /// <para>Lowest possible setting allowed by keypad adjust.</para>
        /// <para>Measurement units: Hours</para>
        /// </summary>  
        public ushort? AirconSwapTimeLowLimit { get; set; }

        /// <summary> 
        /// Aircon Swap Time Setpoint
        /// <para>Time interval (in hours) between aircon SWAP events.</para>
        /// <para>Measurement units: Hours</para>
        /// </summary>  
        public ushort? AirconSwapTimeSetpoint { get; set; }

        /// <summary> 
        /// Aircon Turn-on delay
        /// <para>Time to elapse since first aircon switched on, before second aircon is allowed to turn on.</para>
        /// <para>Measurement units: Minutes</para>
        /// </summary>  
        public ushort? AirconTurnOnDelay { get; set; }

        /// <summary> 
        /// REC Cooling A/C cool Timeout
        /// <para>Max time allowed for A/C to bring room temp down for REC cooling to take over, after which REC will be attempted anyway - in minutes.</para>
        /// <para>Measurement units: Minutes</para>
        /// </summary>  
        public ushort? CoolingTimeout { get; set; }

        /// <summary> 
        /// A/C Fail Temperature
        /// <para>Vent Temperature above which aircon alarm will be ACTIVE.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? FailTemaperature { get; set; }

        /// <summary> 
        /// Fan Cooling Enable Temp Diff
        /// <para>Minimum value that Outside Humidity must be lower that Room Humidity in order for fan to attempt cooling during "Reduced Energy Cooling"</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public ushort? FanCoolingEnableTempDiff { get; set; }

        /// <summary> 
        /// DC fan on setpoint
        /// <para>Temperature above multi A/C run setpoint at which DC fan will turn-on.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? FanOnSetpoint { get; set; }

        /// <summary> 
        /// Fan To Aircon Switchover Temp
        /// <para>Temperature above which Aircon takes over from DC fan, if the fan does not succeed in keeping temperature beloe this lavel during "Reduced Energy Cooling". </para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public ushort? FanToAirconSwitchoverTemp { get; set; }

        /// <summary> 
        /// High temp alarm setpoint
        /// <para>Room Temperature setpoint at or above which High Temp alarm  will be set. </para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? HighTempAlarmSetpoint { get; set; }

        /// <summary> 
        /// Multi A/C Run Default
        /// <para>Setting used when A/C RESET is held in for 5 seconds (default restored).</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? MultiRunDefault { get; set; }

        /// <summary> 
        /// Multi A/C Run High limit
        /// <para>Highest possible setting allowed by keypad adjust.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? MultiRunHighLimit { get; set; }

        /// <summary> 
        /// Multi A/C Run Low limit
        /// <para>Lowest possible setting allowed by keypad adjust.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? MultiRunLowLimit { get; set; }

        /// <summary> 
        /// Multi A/C Run Off hysteresis
        /// <para>No. Of degrees below Multi A/C setpoint, at which Multi A/C function is canceled.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? MultiRunOffHysteresis { get; set; }

        /// <summary> 
        /// Multi A/C Run Setpoint
        /// <para>Room temperature setpoint at or above which all aircons will turn ON.</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public short? MultiRunSetpoint { get; set; }

        /// <summary> 
        /// REC Regulate Hysteresis
        /// <para>Value below "Regulate Setpoint" at which DC Fan OR Aircons will turn off (only used when fan speed control is not enabled) during EPCC or "free cooling".</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public ushort? RegulateHysteresis { get; set; }

        /// <summary> 
        /// REC Regulate Setpoint
        /// <para>Value around which DC Fan OR Aircon will attempt to regulate Room Temperature during EPCC or "Free Cooling"</para>
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>  
        public ushort? RegulateSetpoint { get; set; }

        /// <summary> 
        /// A/C Temperature Test Delay
        /// <para>Time delay from A/C turn-on until vent temperature checking starts.</para>
        /// <para>Measurement units: Minutes</para>
        /// </summary>  
        public ushort? TemperatureTestDelay { get; set; }


        public string SiteName { get; set; }

        public override IEnumerable<(string, string, object)> ToPortalParameters(string deviceId)
        {

            var result = new List<(string, string, object)>();

            if (AirconAlarmClearTemp.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconAlarmClearTemp)}", AirconAlarmClearTemp));
            }

            if (AirconDisableHysteresis.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconDisableHysteresis)}", AirconDisableHysteresis));
            }

            if (AirconEnableSetpoint.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconEnableSetpoint)}", AirconEnableSetpoint));
            }

            if (AirconRestTime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconRestTime)}", AirconRestTime));
            }

            if (AirconSwapTimeDefault.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconSwapTimeDefault)}", AirconSwapTimeDefault));
            }

            if (AirconSwapTimeHighLimit.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconSwapTimeHighLimit)}", AirconSwapTimeHighLimit));
            }

            if (AirconSwapTimeLowLimit.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconSwapTimeLowLimit)}", AirconSwapTimeLowLimit));
            }

            if (AirconSwapTimeSetpoint.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconSwapTimeSetpoint)}", AirconSwapTimeSetpoint));
            }

            if (AirconTurnOnDelay.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(AirconTurnOnDelay)}", AirconTurnOnDelay));
            }

            if (CoolingTimeout.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(CoolingTimeout)}", CoolingTimeout));
            }

            if (FailTemaperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FailTemaperature)}", FailTemaperature));
            }

            if (FanCoolingEnableTempDiff.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FanCoolingEnableTempDiff)}", FanCoolingEnableTempDiff));
            }

            if (FanOnSetpoint.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FanOnSetpoint)}", FanOnSetpoint));
            }

            if (FanToAirconSwitchoverTemp.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(FanToAirconSwitchoverTemp)}", FanToAirconSwitchoverTemp));
            }

            if (HighTempAlarmSetpoint.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(HighTempAlarmSetpoint)}", HighTempAlarmSetpoint));
            }

            if (MultiRunDefault.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiRunDefault)}", MultiRunDefault));
            }

            if (MultiRunHighLimit.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiRunHighLimit)}", MultiRunHighLimit));
            }

            if (MultiRunLowLimit.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiRunLowLimit)}", MultiRunLowLimit));
            }

            if (MultiRunOffHysteresis.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiRunOffHysteresis)}", MultiRunOffHysteresis));
            }

            if (MultiRunSetpoint.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiRunSetpoint)}", MultiRunSetpoint));
            }

            if (RegulateHysteresis.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RegulateHysteresis)}", RegulateHysteresis));
            }

            if (RegulateSetpoint.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RegulateSetpoint)}", RegulateSetpoint));
            }

            if (TemperatureTestDelay.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(TemperatureTestDelay)}", TemperatureTestDelay));
            }

            result.Add((deviceId, $"[{Category}] {nameof(Timestamp)}", Timestamp));

            return result;
        }
    }
}
