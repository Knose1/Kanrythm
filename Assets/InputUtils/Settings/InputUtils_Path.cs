namespace Com.Github.Knose1.InputUtils.Settings {
	public static class InputUtils_Path
	{
		public const string MENU_ITEM_ROOT_NAME = "Tools/InputUtils/";
		public const string ASSET_NAME = "InputUtils Settings";
		public const string START_FOLDER = "Assets/InputUtils/";

		public static string GetAssetPath()
		{
			return START_FOLDER + ASSET_NAME.Replace(" ", "_") + ".asset";
		}
	}
}