using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHATwitterProject.Objects.Interfaces
{
    /// <summary>
    /// This interface will allow for streaming patterns
    /// </summary>
    /// <scope>public</scope>
    public interface IStream
    {
        event EventHandler StreamDisconnected;
        void ConsumeStream();
    }
}
