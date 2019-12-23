///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 23/12/2019 00:45
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Com.Github.Knose1.Common.InputController;

namespace Com.Github.Knose1.Common.Ui {
	[CustomEditor(typeof(KeyInputButton))]
	public class KeyInputButtonEditor : Editor {

		private KeyInputButton script;

		public override void OnInspectorGUI()
		{
			script = target as KeyInputButton;

			Controller controller = FindObjectOfType<Controller>();

			if (controller)
			{
				DrawDefaultInspector();
				List<RebindingFunction> rebindingFunctions = controller.GetRebindingFunctions();
				List<string> names = new List<string>();
				for (int i = rebindingFunctions.Count - 1; i >= 0; i--)
				{
					names.Insert(0, rebindingFunctions[i].nameInInspector);
				}

				EditorGUI.BeginChangeCheck();
				script.RebindedFuction = EditorGUILayout.Popup("Rebind function", script.RebindedFuction, names.ToArray());
				EditorGUI.EndChangeCheck();
			}
			else
			{
				EditorGUILayout.HelpBox("There is no Controller", MessageType.Error);
				DrawDefaultInspector();
			}

		}

	}
}