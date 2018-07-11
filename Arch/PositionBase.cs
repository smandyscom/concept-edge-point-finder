using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace WindowsFormsApp2.Arch
{
    /// <summary>
    /// Or point base
    /// </summary>
    public class PositionBase : 
        ElementBase
    {
        Mat _position = Mat.Ones((int)DefinitionDimension.DIM_2D,1,MatType.CV_64FC1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencies"></param>
        public PositionBase(List<ElementBase> dependencies) : base(dependencies)
        {
            
        }

        public override void OnDependeciesValueChanged(object sender, EventArgs args)
        {
            //derived type implement this
        }

        #region "operator overload"
        public static PositionBase operator *(CoordinateBase coord, PositionBase position)
        {
            //reference upward
            return new PositionBase(null) {
                _coordinateReference = coord._coordinateReference,
                _position = coord.Transformation * position._position};
        }
        public static PositionBase operator +(PositionBase position1, PositionBase position2)
        {
            return new PositionBase(null)
            {
                _coordinateReference = position1._coordinateReference,
                _position = position1._position + position2._position
            };
        }
        public static PositionBase operator -(PositionBase position1, PositionBase position2)
        {
            return new PositionBase(null)
            {
                _coordinateReference = position1._coordinateReference,
                _position = position1._position - position2._position
            };
        }
        #endregion
    }
}
