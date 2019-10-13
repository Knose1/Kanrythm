using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.Hud
{
	[ExecuteInEditMode]
	public class Screen : MonoBehaviour
	{
		
		private HudContainer hudContainer;
		public HudContainer HudContainer { get => hudContainer; }

		virtual public void OnAddedToHudContainer(HudContainer hudContainer)
		{
			this.hudContainer = hudContainer;
		}
		virtual public void OnRemovedFromHudContainer(HudContainer hudContainer)
		{
			Destroy(gameObject);
		}

	}
}
