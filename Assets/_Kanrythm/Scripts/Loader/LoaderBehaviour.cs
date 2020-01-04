///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 02/01/2020 16:20
///-----------------------------------------------------------------

using Com.Github.Knose1.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Loader {
	public class LoaderBehaviour : StateMachine
	{
		public event Action OnFinish;

		internal IEnumerator enumerator;

		public void LoadStart()
		{
			doAction = DoActionNormal;
		}

		protected override void DoActionNormal()
		{
			base.DoActionNormal();
			if (enumerator.MoveNext()) return;

			OnFinish?.Invoke();
			OnFinish = null;
			doAction = DoActionVoid;
			Destroy(gameObject);
		}

		private void OnDestroy()
		{
			doAction = DoActionVoid;
			OnFinish = null;
		}


	}
}