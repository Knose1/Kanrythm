using UnityEngine;
using UnityEditor;

namespace Com.Github.Knose1.InputUtils.Settings {
	public static class InputUtils_Settings
	{
		// Open Project Settings
		[MenuItem(InputUtils_Path.MENU_ITEM_ROOT_NAME+"Open KeyInput Settings", false, 300)]
		public static void SelectProjectTextSettings()
		{
			InputUtils_SettingsAsset asset = InputUtils_SettingsAsset.Instance;

			Selection.activeObject = asset;

			EditorUtility.FocusProjectWindow();
			EditorGUIUtility.PingObject(asset);			
		}

		public static InputUtils_SettingsAsset GetAsset() => InputUtils_SettingsAsset.Instance;
	}
}