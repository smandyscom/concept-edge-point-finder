using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Arch;
namespace WindowsFormsApp2.Derived
{
    /// <summary>
    /// 
    /// </summary>
    public class PositionEdge : PositionBase
    {

        //Graphic reference

        /// <summary>
        /// Dependencies : start/end point
        /// </summary>
        /// <param name="list"></param>
        public PositionEdge(List<ElementBase> list) : base(list)
        {
        }
    }
}
