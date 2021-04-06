using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHATwitterProject.Objects.Interfaces
{
    /// <summary>
    /// This interface is used to act as the middle ground between the processors and the UI
    /// </summary>
    /// <scope>internal</scope>
    /// <type>interface</type>
    public interface IBusinessLayer
    {
        /// <summary>
        /// This method will kick off a business layer process
        /// </summary>
        void Run();

        /// <summary>
        /// This will return a view in the form of a <see cref="DataTable"/>
        /// </summary>
        /// <returns><see cref="DataTable"/></returns>
        DataTable GetView();
    }
}
