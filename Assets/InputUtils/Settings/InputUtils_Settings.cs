#if UNITY_EDITOR
using UnityEngine;
#endif
using UnityEditor;

namespace Com.Github.Knose1.InputUtils.Settings {
	public static class InputUtils_Settings
	{
		#if UNITY_EDITOR
		// Open Project Settings
		[MenuItem(InputUtils_Path.MENU_ITEM_ROOT_NAME+"Open KeyInput Settings", false, 300)]
		public static void SelectProjectTextSettings()
		{
			InputUtils_SettingsAsset asset = InputUtils_SettingsAsset.Instance;

			Selection.activeObject = asset;

			EditorUtility.FocusProjectWindow();
			EditorGUIUtility.PingObject(asset);			
		}
		#endif
		
		public static InputUtils_SettingsAsset GetAsset() => InputUtils_SettingsAsset.Instance;
	}
}