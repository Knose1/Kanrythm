using System.Collections.Generic;

namespace Com.Github.Knose1.Common.Utils.AsyncCaller {
	class CallerList : List<CallerBase>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns>Return true if a function has been called</returns>
		public bool CallNext()
		{
			if (this.Count == 0) return false;
			this[0].Call();
			RemoveAt(0);

			return true;
		}
	}
}
