using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace MR.Gestures.WinUI
{
	public static class DependencyObjectExtensions
	{
		/// <summary>
		/// Find any child of type T and Name childname.
		/// Searches down the tree first and then the siblings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parent"></param>
		/// <param name="childName"></param>
		/// <returns></returns>
		public static T FindChild<T>(this DependencyObject parent, string childName = null) where T : DependencyObject
		{
			// Confirm parent and childName are valid. 
			if (parent == null) return null;

			T foundChild = null;

			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				// If the child is not of the request child type child
				T childType = child as T;
				if (childType == null)
				{
					// recursively drill down the tree
					foundChild = child.FindChild<T>(childName);

					// If the child is found, break so we do not overwrite the found child. 
					if (foundChild != null) break;
				}
				else if (!string.IsNullOrEmpty(childName))
				{
					var frameworkElement = child as FrameworkElement;
					// If the child's name is set for search
					if (frameworkElement != null && frameworkElement.Name == childName)
					{
						// if the child's name is of the request name
						foundChild = (T)child;
						break;
					}
				}
				else
				{
					// child element found.
					foundChild = (T)child;
					break;
				}
			}

			return foundChild;
		}

		/// <summary>
		/// Finds all children of type T.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parent"></param>
		/// <param name="depth"></param>
		/// <returns></returns>
		public static IEnumerable<T> FindAllChildren<T>(this DependencyObject parent, int depth = 0) where T : DependencyObject
		{
			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				//Print(depth, child.GetType().FullName);

				T childType = child as T;
				if (childType != null)			// if it's a T
					yield return childType;		// then return it
				else
				{
					// recursively drill down the tree
					foreach(T c in child.FindAllChildren<T>(depth+1))
						yield return c;
				}
			}
		}

        /// <summary>
        /// Returns the top most <see cref="Microsoft.UI.Xaml.Controls.Page"/> or root <see cref="FrameworkElement"/>.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static FrameworkElement GetRoot(this FrameworkElement view)
        {
            Microsoft.UI.Xaml.Controls.Page rc = null;

            var parent = view;
			while(parent != null)
            {
				view = parent;
                if (view is Microsoft.UI.Xaml.Controls.Page p)
                    rc = p;
                parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
            }

            return rc ?? view;
        }

        //private static void Print(int depth, string s)
        //{
        //    Debug.WriteLine("{0}{1}", new string(' ', depth * 2), s);
        //}
    }
}
