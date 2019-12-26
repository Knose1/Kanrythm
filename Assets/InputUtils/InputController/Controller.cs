using Com.Github.Knose1.InputUtils.Settings;
using Com.Github.Knose1.InputUtils.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Github.Knose1.InputUtils.InputController {

	public struct RebindingFunction
	{
		public readonly Action function;
		public readonly string nameInInspector;
		public readonly string defaultKeyName;

		public RebindingFunction(Action function, string nameInInspector, string defaultKeyName)
		{
			this.defaultKeyName = defaultKeyName;
			this.function = function;
			this.nameInInspector = nameInInspector;
		}
	}

	[AddComponentMenu(InputUtils_Path.MENU_ITEM_ROOT_NAME + nameof(Controller))]
	public partial class Controller : MonoBehaviour
	{
		private const string REBINDING_LOG_PREFIX = "[Rebinding] ";
		private static Controller instance;
		public static Controller Instance { get { return instance; } }

		private List<InputAction> rebindListCompare;
		protected List<RebindingFunction> rebindingFunctions;
		protected List<RebindingFunction> rebindingFunctionsEditor;
		public List<RebindingFunction> RebindingFunctions => rebindingFunctions;
		public List<RebindingFunction> RebindingFunctionsEditor => rebindingFunctionsEditor;

		public static bool IsRebinding { get; private set; }

		protected InputActionRebindingExtensions.RebindingOperation currentActionRebinding;

		public static event Action<InputControl> OnRebindEnd;

		private void Awake()
		{
			if (instance)
			{
				throw new Exception("There are two active Controller in the scene");
			}

			InitControllerProject();
			GetRebindingFunctions();
			GetRebindingFunctionsEditor();

			instance = this;
		}

		partial void InitControllerProject();
		partial void GetRebindingFunctions();
		partial void GetRebindingFunctionsEditor();

		public void GenerateRebindingFunctions()
		{
			GetRebindingFunctions();
		}

		public void GenerateRebindingFunctionsEditor()
		{
			GetRebindingFunctionsEditor();
		}

		public void Rebind(InputAction inputAction, List<InputAction> rebindListCompare)
		{
			IsRebinding = true;
			this.rebindListCompare = rebindListCompare;

			if (currentActionRebinding != null) currentActionRebinding.Cancel();

			Debug.Log(REBINDING_LOG_PREFIX + inputAction.name);

			currentActionRebinding = inputAction.PerformInteractiveRebinding()
				.OnPotentialMatch(Rebinding_OnPotentialMatch)
				.OnCancel(Rebinding_OnCancel)
				.OnComplete(Rebinding_OnComplete);
			currentActionRebinding.Start();
		}

		private void Rebinding_OnComplete(InputActionRebindingExtensions.RebindingOperation obj)
		{
			Debug.Log(REBINDING_LOG_PREFIX + "Complete");
			OnRebindEnd(obj.selectedControl);
			currentActionRebinding = null;
			IsRebinding = false;

			obj.Dispose();
		}

		private void Rebinding_OnCancel(InputActionRebindingExtensions.RebindingOperation obj)
		{
			Debug.Log(REBINDING_LOG_PREFIX + "Canceled");
			OnRebindEnd(obj.action.controls[0]);
			currentActionRebinding = null;
			IsRebinding = false;

			obj.Dispose();
		}

		private void Rebinding_OnPotentialMatch(InputActionRebindingExtensions.RebindingOperation obj)
		{
			Debug.Log(REBINDING_LOG_PREFIX + obj.selectedControl.path);

			if (obj.selectedControl == Keyboard.current.escapeKey)
			{
				obj.Cancel();
				return;
			}

			for (int i = rebindListCompare.Count - 1; i >= 0; i--)
			{
				if (obj.selectedControl == rebindListCompare[i].controls[0])
				{
					obj.Cancel();
					return;
				}
			}

			obj.Complete();
		}

		protected void DisposeInstance()
		{
			if (this == instance) instance = null;
		}

		#if UNITY_EDITOR
		[Obsolete("This function can ONLY be used by Unity To generate a new Controller", false)]
		public static void TryCreateController(MenuCommand menu)
		{
			if (!FindObjectOfType<Controller>()) CreateController(menu);
		}

		[Obsolete("This function can ONLY be used by Unity to generate a new Controller", false)]
		[MenuItem("GameObject/Input/Controller", false, 10)]
		[MenuItem(InputUtils_Path.MENU_ITEM_ROOT_NAME+"Create "+nameof(Controller)+" GameObject ", false, 1)]
		public static void CreateController(MenuCommand menu)
		{
			if (FindObjectOfType<Controller>()) throw new Exception("There is already an active Controller in the scene");

			//Root GameObject
			GameObject gameObject = new GameObject("Controller");
			Controller controller = gameObject.AddComponent<Controller>();
		}

		[Obsolete("This function can ONLY be used by Unity to generate a new Controller.cs file", false)]
		[MenuItem("Assets/Create/"+InputUtils_Path.MENU_ITEM_ROOT_NAME+"Create C# " + nameof(Controller), false, 90)]
		[MenuItem(InputUtils_Path.MENU_ITEM_ROOT_NAME+"Create C# " + nameof(Controller), false, 0)]
		public static void CreateControllerFile(MenuCommand menu)
		{
			string path = "";
			UnityEngine.Object selected = Selection.activeObject;
			if (selected == null) path = "Assets";
			else path = AssetDatabase.GetAssetPath(selected.GetInstanceID());
			if (path.Length > 0)
			{
				if (File.Exists(path))
				{
					path = Directory.GetParent(path).FullName.Replace("\\", "/");
				}
			}

			// remove whitespace and minus

			string copyPath = path+"/"+nameof(Controller)+".cs";

			if (!File.Exists(copyPath))
			{
				StreamWriter outfile = new StreamWriter(copyPath);
				outfile.WriteLine("using "+ typeof(InputSystemUtils).Namespace + ";");
				outfile.WriteLine("using System.Collections.Generic;");
				outfile.WriteLine("using UnityEngine.InputSystem;");
				outfile.WriteLine("");
				outfile.WriteLine("namespace "+typeof(Controller).Namespace+" {");
				outfile.WriteLine("	//TODO : Replace 'UNITY_INPUT_SYSTEM' by the C# input system script generated by unity");
				outfile.WriteLine("	");
				outfile.WriteLine("	public partial class "+nameof(Controller));
				outfile.WriteLine("	{");
				outfile.WriteLine("		private UNITY_INPUT_SYSTEM input;");
				outfile.WriteLine("		public UNITY_INPUT_SYSTEM Input { get => input; }");
				outfile.WriteLine("		");
				outfile.WriteLine("		partial void InitControllerProject()");
				outfile.WriteLine("		{");
				outfile.WriteLine("			input = new UNITY_INPUT_SYSTEM();");
				outfile.WriteLine("		}");
				outfile.WriteLine("		");
				outfile.WriteLine("		partial void GetRebindingFunctionsEditor()");
				outfile.WriteLine("		{");
				outfile.WriteLine("			rebindingFunctionsEditor = new List<" + nameof(RebindingFunction)+">();");
				outfile.WriteLine("			rebindingFunctionsEditor.Add(new RebindingFunction(RebindLeft, nameof(RebindLeft), \"\"));");
				outfile.WriteLine("			rebindingFunctionsEditor.Add(new RebindingFunction(RebindRight, nameof(RebindRight), \"\"));");
				outfile.WriteLine("		}");
				outfile.WriteLine("		partial void GetRebindingFunctions()");
				outfile.WriteLine("		{");
				outfile.WriteLine("			rebindingFunctions = new List<"+nameof(RebindingFunction)+">();");
				outfile.WriteLine("			rebindingFunctions.Add(new RebindingFunction(RebindLeft, nameof(RebindLeft), "+nameof(InputSystemUtils)+".GetName(input.Gameplay.Left)));");
				outfile.WriteLine("			rebindingFunctions.Add(new RebindingFunction(RebindRight, nameof(RebindRight), "+nameof(InputSystemUtils)+".GetName(input.Gameplay.Right)));");
				outfile.WriteLine("		}");
				outfile.WriteLine("		");
				outfile.WriteLine("		public void RebindLeft()");
				outfile.WriteLine("		{");
				outfile.WriteLine("			Rebind(input.Gameplay.Left, new List<InputAction>() { input.Gameplay.Right });");
				outfile.WriteLine("		}");
				outfile.WriteLine("		");
				outfile.WriteLine("		public void RebindRight()");
				outfile.WriteLine("		{");
				outfile.WriteLine("			Rebind(input.Gameplay.Right, new List<InputAction>() { input.Gameplay.Left });");
				outfile.WriteLine("		}");
				outfile.WriteLine("		public void OnDestroy()");
				outfile.WriteLine("		{");
				outfile.WriteLine("			DisposeInstance();");			
				outfile.WriteLine("			");
				outfile.WriteLine("			input.Disable(); ");
				outfile.WriteLine("		}");
				outfile.WriteLine("	}");
				outfile.WriteLine("}");

				outfile.Close();
			}
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
		#endif
	}
}