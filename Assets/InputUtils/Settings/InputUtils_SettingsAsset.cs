using System;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;

namespace Com.Github.Knose1.InputUtils.Settings {
	public enum FieldContent
	{
		PATH,
		DISPLAY_NAME,
		SHORT_DISPLAY_NAME,
		NAME
	}

	[Serializable]
	public class ValueChangeEvent : UnityEvent<InputUtils_SettingsAsset> {}

	public class InputUtils_SettingsAsset : ScriptableObject
	{
		private static InputUtils_SettingsAsset _instance;
		public static InputUtils_SettingsAsset Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = AssetDatabase.LoadAssetAtPath<InputUtils_SettingsAsset>(InputUtils_Path.GetAssetPath());
					if (_instance == null)
					{
						_instance = CreateInstance<InputUtils_SettingsAsset>();
						_instance.name = InputUtils_Path.ASSET_NAME;

						string path = InputUtils_Path.GetAssetPath();

						AssetDatabase.CreateAsset(_instance, path);
						AssetDatabase.SaveAssets();
					}
				}

				return _instance;
			}
		}

		private void OnValidate()
		{
			OnValueChange.Invoke(this);
		}

		[SerializeField] private FieldContent m_fieldContent;
		public FieldContent fieldContent { get => m_fieldContent; set => m_fieldContent = value; }


		//Event
		[SerializeField] private ValueChangeEvent m_OnValueChange = default;
		public ValueChangeEvent OnValueChange { get => m_OnValueChange; }
	}
}