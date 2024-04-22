namespace MR.Gestures
{
	public static class ElementExtensions
	{
		/// <summary>
		/// Finds the first parent Element of type T.
		/// </summary>
		/// <typeparam name="T">The type of the parent element you want to find.</typeparam>
		/// <param name="element"></param>
		/// <returns></returns>
		public static T FindParent<T>(this Element element) where T : Element
		{
			element = element.Parent;
			while (element != null && !(element is T))
				element = element.Parent;
			return element as T;
		}

		/// <summary>
		/// Returns whether the given parent is a parent of this Element.
		/// </summary>
		/// <param name="element"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static bool HasParent(this Element element, Element parent)
		{
			element = element.Parent;
			while (element != null && element != parent)
				element = element.Parent;
			return element == parent;
		}

		/// <summary>
		/// Returns a printable hierarchy path for this element.
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static string TreeHierarchy(this Element element)
		{
			string rc = element.GetType().Name;
			if (element.Parent != null)
				rc = $"{element.Parent.TreeHierarchy()} / {rc}";
			return rc;
		}

		public static T FindParent<T>(this IGestureAwareControl element) where T : Element
		{
			return ((Element)element).FindParent<T>();
		}

		public static bool HasParent(this IGestureAwareControl element, Element parent)
		{
			return ((Element)element).HasParent(parent);
		}

	}
}
