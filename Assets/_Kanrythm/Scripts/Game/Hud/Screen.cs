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
		/// <summary>
		/// Provide a readOnly access to the latest hudContainer
		/// </summary>
		protected HudContainer HudContainer { get => hudContainer; }
		private HudContainer hudContainer;
		
		/// <summary>
		/// Function called by the HudContainer when the screen is added by a hudContainer
		/// </summary>
		/// <param name="hudContainer">The hudContainer which added the screen</param>
		virtual public void OnAddedToHudContainer(HudContainer hudContainer)
		{
			this.hudContainer = hudContainer;
		}

		/// <summary>
		/// Function called by the HudContainer when the screen is removed from a hudContainer
		/// </summary>
		/// <param name="hudContainer">The hudContainer which removed the screen</param>
		virtual public void OnRemovedFromHudContainer(HudContainer hudContainer)
		{
			Destroy(gameObject);
		}

	}
}
