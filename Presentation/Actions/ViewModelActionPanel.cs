using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Presentation.ViewModels;

namespace Presentation.Actions
{
    /// <summary>
    /// Composite all actions and order context
    /// As data source to let command bar binding
    /// TODO , what if other need to bind the same instance of this type? how to bind to the same reference?
    /// </summary>
    public class ViewModelActionPanel
    {
        /// <summary>
        /// Properties
        /// </summary>
        public OrderContext Context { get => m_context; }
        public ActionPointFree ActionPointFree { get => m_pointFree; }
        public ActionLineFree ActionLineFree { get => m_lineFree; }
        public ActionCoordinate ActionCoordinate { get => m_coordinate; }

        internal OrderContext m_context = new OrderContext();

        /// <summary>
        /// Actions
        /// </summary>
        internal ActionPointFree m_pointFree = new ActionPointFree();
        internal ActionLineFree m_lineFree = new ActionLineFree();
        internal ActionCoordinate m_coordinate = new ActionCoordinate();

        public ViewModelActionPanel()
        {
            m_coordinate.ViewModelCreated += OnCoordinateCreated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnCoordinateCreated(Object sender,EventArgs e)
        {
            var coord = sender as ViewModelCoordinate;
            m_context.AvailableCoordinates.Add(coord);
            m_context.CurrentCoordinate = coord;
        }
    }
}
