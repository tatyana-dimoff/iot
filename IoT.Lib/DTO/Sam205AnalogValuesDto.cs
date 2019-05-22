using System.Collections.Generic;

namespace IoT.Lib.DTO
{
    public class Sam205AnalogValuesDto : SamDtoBase
    {
        private const string Category = "Analog";

        /// <summary>
        /// Battery Voltage 
        /// <para>Measurement units: Decivolts</para>
        /// </summary>
        public ushort? BatteryVoltage { get; set; }

        /// <summary>
        /// Charge Current
        /// <para>Measurement units: Milliamperes</para>
        /// </summary>
        public ushort? BatteryChargeCurrent { get; set; }

        /// <summary>
        /// Discharge Current
        /// <para>Measurement units: Milliamperes</para>
        /// </summary>
        public ushort? BatteryDischargeCurrent { get; set; }

        /// <summary>
        /// Humidity
        /// <para>Measurement units: Percents</para>
        /// </summary>
        public ushort? Humidity { get; set; }

        /// <summary>
        /// Temperature 1
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public short? Ac1Temperature { get; set; }

        /// <summary>
        /// Temperature 2
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public short? Ac2Temperature { get; set; }

        /// <summary>
        /// Temperature 3
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public short? Ac3Temperature { get; set; }

        /// <summary>
        /// Temperature 4
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public short? Ac4Temperature { get; set; }

        /// <summary>
        /// A/C 1 Total RT 
        /// <para>Measurement units: Hours</para>
        /// </summary>
        public ushort? Ac1TotalRuntime { get; set; }

        /// <summary>
        /// A/C 2 Total RT 
        /// <para>Measurement units: Hours</para>
        /// </summary>
        public ushort? Ac2TotalRuntime { get; set; }

        /// <summary>
        /// Room Temp
        /// <para>Measurement units: Celsius Degrees</para>
        /// </summary>
        public short? RoomTemperature { get; set; }

        /// <summary>
        /// A/C 1 Runtime Since Service 
        /// <para>Measurement units: Hours</para>
        /// </summary>
        public ushort? Ac1Rtss { get; set; }

        /// <summary>
        /// A/C 2 Runtime Since Service
        /// <para>Measurement units: Hours</para>
        /// </summary>
        public ushort? Ac2Rtss { get; set; }

        public override IEnumerable<(string, string, object)> ToPortalParameters(string deviceId)
        {
            var result = new List<(string, string, object)>(13);

            if (BatteryVoltage.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(BatteryVoltage)}", BatteryVoltage));
            }

            if (BatteryChargeCurrent.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(BatteryChargeCurrent)}", BatteryChargeCurrent));
            }

            if (BatteryDischargeCurrent.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(BatteryDischargeCurrent)}", BatteryDischargeCurrent));
            }

            if (Humidity.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Humidity)}", Humidity));
            }

            if (Ac1Temperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Ac1Temperature)}", Ac1Temperature));
            }

            if (Ac2Temperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Ac2Temperature)}", Ac2Temperature));
            }

            if (Ac3Temperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Ac3Temperature)}", Ac3Temperature));
            }

            if (Ac4Temperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Ac4Temperature)}", Ac4Temperature));
            }

            if (Ac1TotalRuntime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Ac1TotalRuntime)}", Ac1TotalRuntime));
            }

            if (Ac2TotalRuntime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Ac2TotalRuntime)}", Ac2TotalRuntime));
            }

            if (RoomTemperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RoomTemperature)}", RoomTemperature));
            }

            if (Ac1Rtss.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Ac1Rtss)}", Ac1Rtss));
            }

            if (Ac2Rtss.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Ac2Rtss)}", Ac2Rtss));
            }

            result.Add((deviceId, $"[{Category}] {nameof(Timestamp)}", Timestamp));

            return result;
        }
    }
}
