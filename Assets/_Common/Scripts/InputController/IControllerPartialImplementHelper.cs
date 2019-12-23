using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Com.Github.Knose1.Common.InputController {
	public interface IControllerPartialImplementHelper<UnityInput>
	{
		UnityInput Input { get; }


		void InitControllerProject();

		List<RebindingFunction> GetRebindingFunctions();
	}
}