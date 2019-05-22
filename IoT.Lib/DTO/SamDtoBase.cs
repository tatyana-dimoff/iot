using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.Lib.DTO
{
    public abstract class SamDtoBase : ISamDto
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public abstract IEnumerable<ValueTuple<string, string, object>> ToPortalParameters(string deviceId);
    }
}
