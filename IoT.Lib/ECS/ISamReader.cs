using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.Lib.ECS
{
    public interface ISamReader
    {
        ISamDto GetAnalogValues();

        ISamConfig GetConfig();

        ISamDto GetFuelReadings();

        ISamDto GetAlarms();

        ISamDto GetGeneratorReadings();

        ISamDto GetPowerReadings();
    }
}
