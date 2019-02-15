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
                return m_value;
            } 
             set
            {
                m_value =value;
            }
        }
        public HierarchyTreeNode<T> Parent
        {
            get
            {
                return m_parent;
            }
            set
            {
                if (m_parent != null)
                {
                    //remove me from previous parent
                    m_parent.m_children.Remove(this);
                }

                m_parent = value; //new parent

                if(value!=null)
                    value.m_children.Add(this);
            }
        }

        internal HierarchyTreeNode<T> m_parent = null;
        /// <summary>
        /// valid when disposing
        /// </summary>
        internal List<HierarchyTreeNode<T>> m_children = new List<HierarchyTreeNode<T>>();

        internal T m_value;

        public HierarchyTreeNode(T data)
        {
            m_value = data;
        }

        void AddChild(T data)
        {
            var node = new HierarchyTreeNode<T>(data);
            node.Parent = this;
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
            var capsuleList =  Path(this, destination);

            var result = new LinkedList<T>();

            var node = capsuleList.First;
            while (node != null)
            {
                result.AddLast(node.Value.Value);
                node = node.Next;
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
            LinkedList<HierarchyTreeNode<T>> sourceAncientList = new LinkedList<HierarchyTreeNode<T>>();
            LinkedList<HierarchyTreeNode<T>> destinationAncientList = new LinkedList<HierarchyTreeNode<T>>();

            CollectAncients(source, sourceAncientList);

            //treat as root 
            if (destination == null)
                destinationAncientList.AddLast(sourceAncientList.Last.Value);
            else
                CollectAncients(destination, destinationAncientList);

            //find out common ancient, search from younger
            LinkedList<HierarchyTreeNode<T>>.Enumerator commonAncientNodeOnSource = 
                sourceAncientList.GetEnumerator();
            do
            {
                //commonAncientNodeOnSource = sourceAncientList.First;



                if (destinationAncientList.Contains(commonAncientNodeOnSource.Current))
                    break;

                //sourceAncientList.RemoveFirst(); //trim out source list as well
            } while (commonAncientNodeOnSource.MoveNext());

            //attach destionation list to source list (merge
            LinkedListNode<HierarchyTreeNode<T>> nextAttachingNode = 
                destinationAncientList.Find(commonAncientNodeOnSource.Current).Previous;

            while (nextAttachingNode!=null)
            {
                sourceAncientList.AddLast(nextAttachingNode.Value); // value copy
                nextAttachingNode = nextAttachingNode.Previous;//iterate
            }

            return sourceAncientList;
        }

        /// <summary>
        /// Head(First : younger
        /// End(Last : older
        /// Search unitl start.m_parent = null
        /// </summary>
        /// <param name="start"></param>
        /// <param name="list"></param>
        static internal void CollectAncients(HierarchyTreeNode<T> start, LinkedList<HierarchyTreeNode<T>> list)
        {
            if (start != null)
            {
                list.AddLast(start);
                CollectAncients(start.m_parent, list);
            }
            else
            {
                //meet null
                return;
            }
        }

        public void Dispose()
        {
            m_parent.m_children.Remove(this); //remove me from my parent
        }
    }
}
