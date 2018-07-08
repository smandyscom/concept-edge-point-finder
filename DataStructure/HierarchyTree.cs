using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    /// <summary>
    /// https://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp
    /// Non-direction graph
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HierarchyTreeNode<T>
        where T : System.IEquatable<T>
    {
        internal HierarchyTreeNode<T> __parent;
        internal List<HierarchyTreeNode<T>> __children;

        internal T __data;

        public HierarchyTreeNode(T data, HierarchyTreeNode<T> parent)
        {
            __data = data;
            __parent = parent;
        }

        void AddChild(T child)
        {
            __children.Add(new HierarchyTreeNode<T>(child, this));
        }
        /// <summary>
        /// Output:
        /// Last(source end
        /// First(destnation end
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        LinkedList<HierarchyTreeNode<T>> Path(HierarchyTreeNode<T> source, HierarchyTreeNode<T> destination)
        {
            ///collect ancients ,find intersection
            LinkedList<HierarchyTreeNode<T>> _sourceAncientList = new LinkedList<HierarchyTreeNode<T>>();
            LinkedList<HierarchyTreeNode<T>> _destinationAncientList = new LinkedList<HierarchyTreeNode<T>>();

            CollectAncients(source, _sourceAncientList);
            CollectAncients(destination, _destinationAncientList);

            //find out common ancient
            LinkedListNode<HierarchyTreeNode<T>> _commonAncientNodeOnSource = _sourceAncientList.Last;
            while (!_destinationAncientList.Contains(_commonAncientNodeOnSource.Value))
            {
                _sourceAncientList.RemoveLast(); //trim out source list as well
                _commonAncientNodeOnSource = _sourceAncientList.Last;
            }

            //attach destionation list to source list (merge
            LinkedListNode<HierarchyTreeNode<T>> _nextAttachingNode = 
                _destinationAncientList.Find(_commonAncientNodeOnSource.Value).Previous;

            while (_nextAttachingNode!=null)
            {
                _sourceAncientList.AddLast(_nextAttachingNode);
                _nextAttachingNode = _nextAttachingNode.Previous;//iterate
            }

            return _sourceAncientList;
        }

        /// <summary>
        /// Head(First : younger
        /// End(Last : older
        /// </summary>
        /// <param name="start"></param>
        /// <param name="list"></param>
        static void CollectAncients(HierarchyTreeNode<T> start, LinkedList<HierarchyTreeNode<T>> list)
        {
            if (start != null)
            {
                list.AddLast(start);
                CollectAncients(start.__parent, list);
            }
            else
            {
                return;
            }
        }
    }
}
