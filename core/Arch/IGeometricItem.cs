using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
namespace Core.Arch
{
    interface IGeometricItem
    {
        Mat Coefficient();
    }
}
