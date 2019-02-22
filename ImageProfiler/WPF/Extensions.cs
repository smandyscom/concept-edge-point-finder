using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace WindowsFormsApp2.Extensions
{
    public static class Extensions
    {
		public static void AddRange<T>(this ObservableCollection<T> observableCollection, IList<T> list)
		{
			foreach (var item in list)
				observableCollection.Add(item);
		}

		public static void ForEach<T>(this ObservableCollection<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
		}



	}
}
