using System.Collections.Generic;

namespace IoT.Lib.DTO
{
    public class Sam100AnalogValuesDto : SamDtoBase
    {
        private const string Category = "Analog";

        /// <summary>
        /// A/C1 Temp
        /// </summary>
        public short? Aircon1Temperature { get; set; }

        /// <summary>
        /// AC1 Total Tun time
        /// </summary>
        public ushort? Aircon1TotalRuntime { get; set; }

        /// <summary>
        /// A/C 1&2 RUN
        /// </summary>
        public ushort? MultiAirconRun { get; set; }

        /// <summary>
        /// A/C2 Temp
        /// </summary>
        public short? Aircon2Temperature { get; set; }

        /// <summary>
        /// AC 2 Total Run time
        /// </summary>
        public ushort? Aircon2TotalRuntime { get; set; }

        /// <summary>
        /// Outside Humidity
        /// </summary>
        public ushort? OutsideHumidity { get; set; }

        /// <summary>
        /// Outside Temp
        /// </summary>
        public short? OutsideTemp { get; set; }

        /// <summary>
        /// Room Humidity
        /// </summary>
        public ushort? RoomHumidity { get; set; }

        /// <summary>
        /// REC Time
        /// </summary>
        public ushort? RecTime { get; set; }

        /// <summary>
        /// Room Temp
        /// </summary>
        public short? RoomTemperature { get; set; }

        /// <summary>
        /// Total A/C Run Time
        /// </summary>
        public ushort? TotalAirconRuntime { get; set; }

        /// <summary>
        /// SWAP Cycle
        /// </summary>
        public ushort? SwapCycle { get; set; }

        public override IEnumerable<(string, string, object)> ToPortalParameters(string deviceId)
        {
            var result = new List<(string, string, object)>();

            if (Aircon1Temperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon1Temperature)}", Aircon1Temperature));
            }

            if (Aircon1TotalRuntime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon1TotalRuntime)}", Aircon1TotalRuntime));
            }

            if (MultiAirconRun.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(MultiAirconRun)}", MultiAirconRun));
            }

            if (Aircon2Temperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon2Temperature)}", Aircon2Temperature));
            }

            if (Aircon2TotalRuntime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(Aircon2TotalRuntime)}", Aircon2TotalRuntime));
            }

            if (OutsideHumidity.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(OutsideHumidity)}", OutsideHumidity));
            }

            if (OutsideTemp.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(OutsideTemp)}", OutsideTemp));
            }

            if (RoomHumidity.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RoomHumidity)}", RoomHumidity));
            }

            if (RecTime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RecTime)}", RecTime));
            }

            if (RoomTemperature.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(RoomTemperature)}", RoomTemperature));
            }

            if (TotalAirconRuntime.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(TotalAirconRuntime)}", TotalAirconRuntime));
            }

            if (SwapCycle.HasValue)
            {
                result.Add((deviceId, $"[{Category}] {nameof(SwapCycle)}", SwapCycle));
            }

            result.Add((deviceId, $"[{Category}] {nameof(Timestamp)}", Timestamp));

            return result;
        }
    }
}
