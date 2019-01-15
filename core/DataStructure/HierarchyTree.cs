using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// https://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp
    /// Non-direction graph
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HierarchyTreeNode<T> : IDisposable
        where T : System.IEquatable<T>
    {
        public T Value {
            get
            {
                return __value;
            } 
             set
            {
                __value =value;
            }
        }
        public HierarchyTreeNode<T> Parent
        {
            get
            {
                return __parent;
            }
            set
            {
                if (__parent != null)
                {
                    //remove me from previous parent
                    __parent.__children.Remove(this);
                }

                __parent = value; //new parent
                value.__children.Add(this);
            }
        }

        internal HierarchyTreeNode<T> __parent = null;
        /// <summary>
        /// valid when disposing
        /// </summary>
        internal List<HierarchyTreeNode<T>> __children = new List<HierarchyTreeNode<T>>();

        internal T __value;

        public HierarchyTreeNode(T data)
        {
            __value = data;
        }

        void AddChild(T data)
        {
            var __node = new HierarchyTreeNode<T>(data);
            __node.Parent = this;
        }

        /// <summary>
        /// Member function version
        /// First : source
        /// Last : destination
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public LinkedList<T> Path(HierarchyTreeNode<T> destination)
        {
            var __capsuleList =  Path(this, destination);

            var result = new LinkedList<T>();

            var __node = __capsuleList.First;
            while (__node != null)
            {
                result.AddLast(__node.Value.Value);
                __node = __node.Next;
            }

            return result;
        }
        

        /// <summary>
        /// Output:
        /// Last(source end
        /// First(destnation end
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
       public static LinkedList<HierarchyTreeNode<T>> Path(HierarchyTreeNode<T> source, HierarchyTreeNode<T> destination)
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
                _sourceAncientList.AddLast(_nextAttachingNode.Value); // value copy
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
        static internal void CollectAncients(HierarchyTreeNode<T> start, LinkedList<HierarchyTreeNode<T>> list)
        {
            if (start != null)
            {
                list.AddLast(start);
                CollectAncients(start.__parent, list);
            }
            else
            {
                //meet null
                return;
            }
        }

        public void Dispose()
        {
            __parent.__children.Remove(this); //remove me from my parent
        }
    }
}
