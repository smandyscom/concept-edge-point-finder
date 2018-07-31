using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Arch;

using OpenCvSharp;

namespace WindowsFormsApp2.Derived
{
    /// <summary>
    /// The image node, used by following elements (i.e find edge/further processing ..etc
    /// </summary>
    class ImageNode : ElementBase
    {
        internal Mat _image;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public ImageNode(List<ElementBase> list) : base(list)
        {

        }
    }
}
