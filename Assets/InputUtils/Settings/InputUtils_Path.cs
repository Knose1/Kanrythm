using System.IO;

namespace Com.Github.Knose1.InputUtils.Settings {
	public static class InputUtils_Path
	{
		public const string MENU_ITEM_ROOT_NAME = "Tools/InputUtils/";
		public const string ASSET_NAME = "InputUtils_Settings";
		public const string START_FOLDER = "InputUtils";
		public const string RESOURCES_FOLDER = "Resources";
		public const string ASSETS_FOLDER = "Assets";


		public static string GetAssetRessourcePath()
		{
			return Path.Combine(START_FOLDER,ASSET_NAME.Replace(" ", "_"));
		}
		public static string GetAssetPath()
		{
			return Path.Combine(ASSETS_FOLDER, RESOURCES_FOLDER, START_FOLDER, ASSET_NAME.Replace(" ", "_") + ".asset");
		}
	}
}