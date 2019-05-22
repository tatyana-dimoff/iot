using IoT.Lib.DTO;
using System;
using System.IO;
using System.Linq;

namespace IoT.Lib.ECS
{
    public class Sam205Reader : ISam205Reader
    {
        private readonly byte[] NvramRequestHeader = new byte[] { 0x0D, 0x0B, 0x3C, 0x10, 0x03 };
        private readonly byte[] NvramResponseHeader = new byte[] { 0x0B, 0x0D, 0x00, 0x1F };

        private readonly byte[] VramRequestHeader = new byte[] { 0x0D, 0x0B, 0x3C, 0x70, 0x03 };
        private readonly byte[] VramResponseHeader = new byte[] { 0x0B, 0x0D, 0x00, 0x7F };

        private readonly ISamController Controller = null;

        /// <summary>
        /// Initialize Sam205Reader.
        /// </summary>
        /// <param name="cntrlr">The controller abstraction.</param>
        public Sam205Reader(ISamController cntrlr)
        {
            Controller = cntrlr ?? throw new ArgumentNullException(nameof(cntrlr));
        }

        /// <summary>
        /// Read Non-volatile RAM block.
        /// </summary>
        /// <param name="startAddress">The address of the starting byte in  the NVRAM block.</param>
        /// <param name="endAddress">The address of the ending byte in the NVRAM block.</param>
        /// <returns><see cref="byte[]" /> containing just the data bytes.</returns>
        private byte[] ReadNvram(ushort startAddress, ushort endAddress)
        {
            return ReadSam205(NvramRequestHeader, NvramResponseHeader, startAddress, endAddress);
        }

        private byte[] ReadSam205(byte[] requestHeader, byte[] responseHeader, ushort startAddress, ushort endAddress)
        {
            var addressMsb = startAddress >> 8;
            var addressLsb = startAddress & byte.MaxValue;
            var numberOfBytesRequested = (byte)(endAddress - startAddress + 1);
            var commandBlock = new byte[10];

            Buffer.BlockCopy(requestHeader, 0, commandBlock, 0, requestHeader.Length);

            commandBlock[5] = Convert.ToByte(addressMsb);
            commandBlock[6] = Convert.ToByte(addressLsb);
            commandBlock[7] = Convert.ToByte(numberOfBytesRequested);

            var reqChecksum = commandBlock.Sum(y => y);
            var reqMsb = (reqChecksum >> 8) & byte.MaxValue;
            var reqLsb = reqChecksum & byte.MaxValue;

            commandBlock[8] = Convert.ToByte(reqMsb);
            commandBlock[9] = Convert.ToByte(reqLsb);

            var result = Controller.Execute(commandBlock, numberOfBytesRequested, responseHeader);

            return result;
        }

        private byte[] ReadVram(ushort startAddress, ushort endAddress)
        {
            return ReadSam205(VramRequestHeader, VramResponseHeader, startAddress, endAddress);
        }

        /// <summary>
        /// Gets address of the remote machine which hosts the SAM205 device.
        /// </summary>
        public string HostAddress { get; private set; }

        public ISamDto GetAnalogValues()
        {
            var result = new Sam205AnalogValuesDto();

            var analogData = ReadVram(230, 236);

            if (analogData?.Length != 7)
            {
                throw new InvalidDataException("Unexpected or no data was received from the device.");
            }

            result.Ac1Temperature = unchecked((short)(analogData[1] - 128));
            result.Ac2Temperature = unchecked((short)(analogData[2] - 128));
            result.Ac3Temperature = unchecked((short)(analogData[3] - 128));
            result.Ac4Temperature = unchecked((short)(analogData[4] - 128));
            result.Humidity = analogData[5];
            result.RoomTemperature = unchecked((short)(analogData[6] - 128));

            analogData = ReadVram(254, 259);

            if (analogData?.Length != 6)
            {
                throw new InvalidDataException("Unexpected or no data was received from the device.");
            }

            result.BatteryChargeCurrent = unchecked((ushort)((analogData[3] << 8) | analogData[2]));
            result.BatteryDischargeCurrent = unchecked((ushort)((analogData[5] << 8) | analogData[4]));
            result.BatteryVoltage = unchecked((ushort)((analogData[1] << 8) | analogData[0]));

            analogData = ReadVram(12, 19);

            if (analogData?.Length != 8)
            {
                throw new InvalidDataException("Unexpected no data was received from the device.");
            }

            result.Ac1TotalRuntime = unchecked((ushort)((analogData[1] << 8) | analogData[0]));
            result.Ac1Rtss = unchecked((ushort)((analogData[3] << 8) | analogData[2])); 
            result.Ac2TotalRuntime = unchecked((ushort)((analogData[5] << 8) | analogData[4]));
            result.Ac2Rtss = unchecked((ushort)((analogData[7] << 8) | analogData[6]));

            return result;
        }

        public ISamConfig GetConfig()
        {
            var result = new Sam205ConfigDto();

            byte[] samConfigData = null;

            samConfigData = ReadNvram(458, 485);

            if (samConfigData?.Length != 28)
            {
                throw new InvalidDataException("Unexpected or no data was received from the device.");
            }

            result.AirconSwapTime = samConfigData[0];
            result.MultiAirconRun = samConfigData[1];
            result.MultiAirconRunOffHysteresis = samConfigData[2];
            result.AirconRestTime = samConfigData[3];
            result.AirconTurnOnDelay = samConfigData[4];
            result.HighTempAlarm = samConfigData[5];
            result.VeryHighTempAlarm = samConfigData[6];
            result.AirconsEnable = samConfigData[7];
            result.AirconsDisableHysteresis = samConfigData[8];
            result.AirconFailTemp = samConfigData[9];
            result.AirconAlarmClearThreshold = samConfigData[10];
            result.AirconTempTestDelay = samConfigData[11];
            result.EmergencyFanOn = samConfigData[12];
            result.HumidityAlarmTriggerTheshold = samConfigData[13];
            result.HumidityAlarmClearTheshold = samConfigData[14];
            result.PiggybackFailTemp = samConfigData[15];
            result.PiggybackClearThreshold = samConfigData[16];
            result.PiggybackClearTimeConst = samConfigData[17];
            result.RoomTempRegulate = samConfigData[18];
            result.RoomTempRegulateHysteresis = samConfigData[19];
            result.FanCoolingEnableTempDiff = samConfigData[20];
            result.AirconsHumidityOn = samConfigData[21];
            result.AirconsHumidityOff = samConfigData[22];
            result.FanToAirconSwitchoverTemp = samConfigData[23];
            result.FanEnableHumidityDiff = samConfigData[24];
            result.HybridAirconRunTime = samConfigData[25];
            result.AirconServiceInterval = (ushort)((samConfigData[27] << 8) | samConfigData[26]);

            return result;
        }

        public ISamDto GetAlarms()
        {
            var result = new Sam205AlarmsDto();

            var alarmData = ReadVram(0, 9);

            if (alarmData?.Length != 10)
            {
                throw new InvalidDataException("Unexpected or no data was received from the device.");
            }

            result.Aircon1Power = (alarmData[4] & 4) == 4;
            result.Aircon2Power = (alarmData[4] & 8) == 8;
            result.Aircon1Fail = (alarmData[5] & 4) == 4;
            result.Aircon2Fail = (alarmData[5] & 8) == 8;
            result.HighTemperature = (alarmData[5] & 64) == 64;
            result.VeryHighTemperature = (alarmData[5] & 128) == 128;
            result.MultiAirconRun = (alarmData[6] & 1) == 1;
            result.HumidityHigh = (alarmData[7] & 64) == 64;
            result.HumidityProbe = (alarmData[7] & 128) == 128;
            result.FanPowerAlarm = (alarmData[4] & 128) == 128;
            result.ServiceDue = (alarmData[9] & 1) == 1;

            // TODO: Read GENERAL alarms as well
            result.SmokeAlarm = (alarmData[5] & 1) == 1;
            result.SmokeDetector = (alarmData[5] & 2) == 2;
            result.FailAlarm = (alarmData[6] & 2) == 2;
            result.ConfigCorrupt = (alarmData[6] & 4) == 4;
            result.ModemFail = (alarmData[7] & 128) == 128;
            result.DoorsAlarm = null;
            result.Movement = null;
            result.NavLights = null;
            result.PanicAlarm = null;
            result.Spare1 = null;
            result.Spare2 = null;
            result.Spare3 = null;
            result.Spare4 = null;
            result.Spare5 = null;
            result.Spare6 = null;
            

            return result;
        }

        public ISamDto GetFuelReadings()
        {
            throw new NotImplementedException();
        }

        public ISamDto GetGeneratorReadings()
        {
            throw new NotImplementedException();
        }

        public ISamDto GetPowerReadings()
        {
            throw new NotImplementedException();
        }

    }
}
