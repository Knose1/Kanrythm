using System;

namespace Com.Github.Knose1.Common.Utils.AsyncCaller {
	abstract class CallerBase
	{
		protected object[] args;

		virtual public object Call() { return null; }
	}

	class Caller : CallerBase
	{
		private Action function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action function, object[] args)
		{
			this.function = function;
			this.args = args;
		}
	}

	class Caller<T1> : CallerBase
	{
		private Action<T1> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2> : CallerBase
	{
		private Action<T1, T2> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3> : CallerBase
	{
		private Action<T1, T2, T3> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4> : CallerBase
	{
		private Action<T1, T2, T3, T4> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6, T7> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6, T7, T8> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7, T8> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7, T8> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6, T7, T8, T9> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}

	class Caller<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}


	class Caller<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : CallerBase
	{
		private Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> function;

		override public object Call() { return function.DynamicInvoke(args); }

		public Caller(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> function, object[] args)
		{
			this.function = function;
			this.args = args;
		}


	}
}
