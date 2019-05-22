using IoT.Lib.DTO;
using System;
using System.IO;
using System.Linq;

namespace IoT.Lib.ECS
{
    public class Sam100Reader : ISam100Reader
    {
        private readonly byte[] NvramRequestHeader = new byte[] { 0x00, 0x10, 0x02 };
        private readonly byte[] NvramResponseHeader = new byte[] { 0x10, 0x1F };

        private readonly byte[] VramRequestHeader = new byte[] { 0x00, 0x70, 0x02 };
        private readonly byte[] VramResponseHeader = new byte[] { 0x70, 0x7F };

        private readonly ISamController Controller = null;

        public Sam100Reader(ISamController cntrlr)
        {
            Controller = cntrlr ?? throw new ArgumentNullException($"{nameof(cntrlr)} must be specified.");
        }

        /// <summary>
        /// Reads contiguous Non-volatile RAM block.
        /// </summary>
        /// <param name="startAddress">The address of the starting byte in  the NVRAM block.</param>
        /// <param name="endAddress">The address of the ending byte in the NVRAM block.</param>
        /// <returns><see cref="byte[]" /> containing just the data bytes.</returns>
        private byte[] ReadNvram(ushort startAddress, ushort endAddress)
        {
            return ReadSam100(NvramRequestHeader, NvramResponseHeader, startAddress, endAddress);
        }

        private byte[] ReadSam100(byte[] requestHeader, byte[] responseHeader, ushort startAddress, ushort endAddress)
        {
            byte[] result = null;

            var numberOfBytesRequested = (byte)(endAddress - startAddress + 1);
            var commandBlock = new byte[7];

            Buffer.BlockCopy(requestHeader, 0, commandBlock, 0, requestHeader.Length);

            commandBlock[3] = Convert.ToByte(startAddress);
            commandBlock[4] = numberOfBytesRequested;

            var reqChecksum = commandBlock.Sum(y => y);
            var reqMsb = (reqChecksum >> 8) & byte.MaxValue;
            var reqLsb = reqChecksum & byte.MaxValue;

            commandBlock[5] = Convert.ToByte(reqMsb);
            commandBlock[6] = Convert.ToByte(reqLsb);

            result = Controller.Execute(commandBlock, numberOfBytesRequested, responseHeader);

            if (result?.Length != numberOfBytesRequested)
            {
                throw new InvalidDataException("Unexpected or no data was received from the SAM100 device.");
            }

            return result;
        }

        /// <summary>
        /// Reads contiguous Volatile RAM block.
        /// </summary>
        /// <param name="startAddress">The address of the starting byte in  the NVRAM block.</param>
        /// <param name="endAddress">The address of the ending byte in the NVRAM block.</param>
        /// <returns><see cref="byte[]" /> containing just the data bytes.</returns>
        private byte[] ReadVram(ushort startAddress, ushort endAddress)
        {
            return ReadSam100(VramRequestHeader, VramResponseHeader, startAddress, endAddress);
        }

        public ISamDto GetAnalogValues()
        {
            var result = new Sam100AnalogValuesDto();

            var analogData = ReadVram(105, 110);

            result.OutsideTemp = (short)(analogData[0] - 128);
            result.Aircon1Temperature = (short)(analogData[1] - 128);
            result.Aircon2Temperature = (short)(analogData[2] - 128);
            result.RoomTemperature = (short)(analogData[3] - 128);
            result.RoomHumidity = analogData[4];
            result.OutsideHumidity = analogData[5];
            result.Aircon1TotalRuntime = null;
            result.MultiAirconRun = null;
            result.Aircon2TotalRuntime = null;
            result.RecTime = null;
            result.TotalAirconRuntime = null;
            result.SwapCycle = null;

            return result;
        }

        public ISamConfig GetConfig()
        {
            var result = new Sam100ConfigDto();

            byte[] configData = null;

            configData = ReadNvram(177, 208);

            result.AirconSwapTimeLowLimit = configData[0];
            result.AirconSwapTimeHighLimit = configData[1];
            result.AirconSwapTimeDefault = configData[2];
            result.AirconSwapTimeSetpoint = configData[3];
            result.MultiRunLowLimit = configData[4];
            result.MultiRunHighLimit = configData[5];
            result.MultiRunDefault = configData[6];
            result.MultiRunSetpoint = configData[7];
            result.MultiRunOffHysteresis = configData[8];
            result.AirconRestTime = configData[9];
            result.AirconTurnOnDelay = configData[10];
            result.HighTempAlarmSetpoint = configData[11];
            result.AirconEnableSetpoint = configData[12];
            result.AirconDisableHysteresis = configData[13];
            result.FailTemaperature = configData[14];
            result.AirconAlarmClearTemp = configData[15];
            result.TemperatureTestDelay = configData[16];
            result.FanOnSetpoint = configData[17];
            result.RegulateSetpoint = configData[25];
            result.RegulateHysteresis = configData[26];
            result.FanCoolingEnableTempDiff = configData[27];
            result.FanToAirconSwitchoverTemp = configData[28];
            result.CoolingTimeout = configData[31];

            return result;
        }

        public ISamDto GetAlarms()
        {
            var result = new Sam100AlarmsDto();

            var alarmData = ReadVram(96, 104);

            result.AccessControl = (alarmData[4] & 1) == 1;
            result.AcPhaseFail = (alarmData[1] & 1) == 1;
            result.Aircon1Fail = (alarmData[1] & 32) == 32;
            result.Aircon1On = (alarmData[7] & 1) == 1;
            result.Aircon1Power = (alarmData[3] & 4) == 4;
            result.Aircon2Fail = (alarmData[1] & 64) == 64;
            result.Aircon2On = (alarmData[7] & 2) == 2;
            result.Aircon2Power = (alarmData[1] & 8) == 8;
            result.Aircon3Fail = (alarmData[1] & 128) == 128;
            result.Aircon3On = (alarmData[7] & 4) == 4;
            result.Aircon3Power = (alarmData[1] & 16) == 16;
            result.AircraftLights = (alarmData[1] & 8) == 8;
            result.Aux1Alarm = (alarmData[2] & 2) == 2;
            result.Aux2Alarm = (alarmData[2] & 4) == 4;
            result.Aux3Alarm = (alarmData[2] & 8) == 8;
            result.Aux4Alarm = (alarmData[2] & 16) == 16;
            result.Aux5Alarm = (alarmData[2] & 32) == 32;
            result.Aux6Alarm = (alarmData[2] & 64) == 64;
            result.Aux7Alarm = (alarmData[2] & 128) == 128;
            result.Aux8Alarm = (alarmData[3] & 1) == 1; //!!!
            result.AuxiliaryPower = (alarmData[3] & 128) == 128;
            result.BatteryIntegrity = (alarmData[1] & 2) == 2;
            result.BatteryLow = (alarmData[0] & 128) == 128;
            result.ConfigCorrupted = (alarmData[4] & 2) == 2;
            result.EscomSupply = (alarmData[1] & 4) == 4;
            result.DcFanPower = (alarmData[3] & 32) == 32;
            result.DcFanOn = (alarmData[7] & 16) == 16;
            result.FreeCoolingOn = (alarmData[8] & 1) == 1;
            result.HighHumidity = (alarmData[4] & 16) == 16;
            result.HighTemperature = (alarmData[0] & 4) == 4;
            result.HumidityProbe1 = (alarmData[4] & 4) == 4;
            result.HumidityProbe2 = (alarmData[8] & 8) == 8;
            result.Intruder = (alarmData[0] & 1) == 1;
            result.MainsFail = (alarmData[0] & 64) == 64;
            result.MovementRoom = (alarmData[0] & 16) == 16;
            result.MovementYard = (alarmData[3] & 2) == 2;
            result.MultiAirconRun = (alarmData[1] & 16) == 16;
            result.PanicAlarm = (alarmData[3] & 64) == 64;
            result.Pix1 = (alarmData[5] & 1) == 1;
            result.Pix10 = (alarmData[6] & 2) == 2;
            result.Pix11 = (alarmData[6] & 4) == 4;
            result.Pix12 = (alarmData[6] & 8) == 8;
            result.Pix13 = (alarmData[6] & 16) == 16;
            result.Pix14 = (alarmData[6] & 32) == 32;
            result.Pix15 = (alarmData[6] & 64) == 64;
            result.Pix16 = (alarmData[6] & 128) == 128;
            result.Pix2 = (alarmData[5] & 2) == 2;
            result.Pix3 = (alarmData[5] & 4) == 4;
            result.Pix4 = (alarmData[5] & 8) == 8;
            result.Pix5 = (alarmData[5] & 16) == 16;
            result.Pix6 = (alarmData[5] & 32) == 32;
            result.Pix7 = (alarmData[5] & 64) == 64;
            result.Pix8 = (alarmData[5] & 128) == 128;
            result.Pix9 = (alarmData[6] & 1) == 1;
            result.RectiferModule = (alarmData[0] & 8) == 8;
            result.RectiferSystem = (alarmData[0] & 32) == 32;
            result.SmokeAlarm = (alarmData[0] & 2) == 2;
            result.TmaVswrFail = (alarmData[2] & 1) == 1;

            

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
