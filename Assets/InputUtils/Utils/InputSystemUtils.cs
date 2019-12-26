///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 23/12/2019 16:12
///-----------------------------------------------------------------

using Com.Github.Knose1.InputUtils.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Github.Knose1.InputUtils.Utils {
	public static class InputSystemUtils {

		public static string GetName(InputAction action, int index = 0)
		{
			return GetName(action.controls[index]);
		}
		public static string GetName(InputControl control)
		{
			switch (InputUtils_SettingsAsset.Instance.fieldContent)
			{
				case FieldContent.PATH:
					return control.path;
				case FieldContent.DISPLAY_NAME:
					return control.displayName;
				case FieldContent.SHORT_DISPLAY_NAME:
					return control.shortDisplayName;
				case FieldContent.NAME:
					return control.name;
			}

			return "";
		}
		
	}
}