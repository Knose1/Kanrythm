using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.IO;

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
					_instance = Resources.Load<InputUtils_SettingsAsset>(InputUtils_Path.GetAssetRessourcePath());
					
					if (_instance == null)
					{
						#if UNITY_EDITOR
						_instance = CreateInstance<InputUtils_SettingsAsset>();
						_instance.name = InputUtils_Path.ASSET_NAME;

						if (!AssetDatabase.IsValidFolder(Path.Combine(InputUtils_Path.ASSETS_FOLDER, InputUtils_Path.RESOURCES_FOLDER)))
						{
							AssetDatabase.CreateFolder(InputUtils_Path.ASSETS_FOLDER, InputUtils_Path.RESOURCES_FOLDER);
						}
						if (!AssetDatabase.IsValidFolder(Path.Combine(InputUtils_Path.ASSETS_FOLDER, InputUtils_Path.RESOURCES_FOLDER, InputUtils_Path.START_FOLDER)))
						{
							AssetDatabase.CreateFolder(Path.Combine(InputUtils_Path.ASSETS_FOLDER, InputUtils_Path.RESOURCES_FOLDER), InputUtils_Path.START_FOLDER);
						}

						AssetDatabase.CreateAsset(_instance, InputUtils_Path.GetAssetPath());
						AssetDatabase.SaveAssets();
						#else
						throw new Exception("["+nameof(InputUtils_SettingsAsset)+"] _instance is null");
						#endif
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