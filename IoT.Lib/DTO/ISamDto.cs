using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.Lib
{
    public interface ISamDto
    {
        IEnumerable<ValueTuple<string, string, object>> ToPortalParameters(string deviceId);
    }
}
