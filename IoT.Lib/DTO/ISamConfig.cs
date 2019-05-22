using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.Lib
{
    public interface ISamConfig : ISamDto
    {
        string SiteName { get; }
    }
}
