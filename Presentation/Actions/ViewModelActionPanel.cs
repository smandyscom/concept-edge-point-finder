using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Actions
{
    /// <summary>
    /// Composite all actions and order context
    /// As data source to let command bar binding
    /// TODO , what if other need to bind the same instance of this type? how to bind to the same reference?
    /// </summary>
    public class ViewModelActionPanel
    {
        public OrderContext Context { get => m_context; }
        public ActionPointFree ActionPointFree { get => m_pointFree; }
        public ActionLineFree ActionLineFree { get => m_lineFree; }

        internal OrderContext m_context = new OrderContext();

        /// <summary>
        /// Actions
        /// </summary>
        internal ActionPointFree m_pointFree = new ActionPointFree();
        internal ActionLineFree m_lineFree = new ActionLineFree();
    }
}
