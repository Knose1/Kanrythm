///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 23/12/2019 00:45
///-----------------------------------------------------------------

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Com.Github.Knose1.InputUtils.InputController;

namespace Com.Github.Knose1.InputUtils.Ui {
	[CustomEditor(typeof(BindingInputButton))]
	public class BindingInputButtonEditor : Editor {

		private BindingInputButton script;

		public override void OnInspectorGUI()
		{
			script = target as BindingInputButton;

			Controller controller = FindObjectOfType<Controller>();

			if (controller)
			{
				DrawDefaultInspector();

				if (controller.RebindingFunctionsEditor == null) controller.GenerateRebindingFunctionsEditor();

				List<RebindingFunction> rebindingFunctions = controller.RebindingFunctionsEditor;
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
#endif