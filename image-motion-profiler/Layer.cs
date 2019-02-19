using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using WindowsFormsApp2.DrawObjects;
using WindowsFormsApp2.Interface;
using WindowsFormsApp2.Extensions;

namespace WindowsFormsApp2
{
    public class Layer : INotifyPropertyChanged
    {
        public bool Visible { get; set; } = true;
        public ObservableCollection<Idraw> drawObjects { get; set; }= new ObservableCollection<Idraw>();

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
		{
			PropertyChanged?.Invoke(this, eventArgs);
		}

		public void Add(Idraw obj)
        {
            drawObjects.Add(obj);
            
            PointF intersection;
			List<Idraw> needAdd = new List<Idraw>();
            foreach (Idraw shape in drawObjects)
            {
                if ((intersection = Utils.GetIntersectPoint(obj, shape)) != PointF.Empty)
                {
                    Idraw isp = new InterSectPoint(intersection, obj, shape);
                    needAdd.Add(isp);
                    

                }
            }
					
			drawObjects.AddRange(needAdd);
        }
        public void DrawAllObject(Graphics graphics)
        {
			drawObjects.ForEach(instance =>
			{
                
				//if (!instance.GetType().IsSubclassOf(typeof(SnapBase)))
					instance.draw(graphics);//intersection is not 
				}
			);
        }

        public Idraw GetHitObject(PointF hit)
        {
            List<SnapBase> candidates = new List<SnapBase>();

            drawObjects.ForEach(obj => candidates.AddRange(obj.GetSnapPoints().FindAll(p => p.isHitObject(hit))));
            if (candidates.Count > 0)
            {
                candidates.OrderBy(p => p.Distance2(hit));
                double dis = candidates.First().Distance2(hit);

                // find the min distance and no upstream snapPoint
                List<SnapBase> min = candidates.FindAll(p => p.Distance2(hit) == dis);
                List<SnapBase> isolate = min.FindAll(p => (p as SnapBase).upstream == null);
                return min.Union(isolate).First();
            }

            return drawObjects.FirstOrDefault(obj => obj.isHitObject(hit));
        }

    }
}
