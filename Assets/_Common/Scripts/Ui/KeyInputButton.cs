///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 22/12/2019 23:44
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Com.Github.Knose1.Common.InputController;
using UnityEngine.UI;
using UnityEditor;

namespace Com.Github.Knose1.Common.Ui {
	[AddComponentMenu("UI/Input/KeyInputButton")]
	public class KeyInputButton : UIBehaviour
	{
		[SerializeField] private Text text;
		[SerializeField] private Text placeOlder;
		[SerializeField] private RebindStartEvent startRebind;

		public RebindStartEvent StartRebind { get => startRebind; }

		[SerializeField] private RebindEndEvent endRebind;
		public RebindEndEvent EndRebind { get => endRebind; }

		[SerializeField, HideInInspector] protected int rebindedFunction;
		private bool isRebinding;

		internal int RebindedFuction { get => rebindedFunction; set => rebindedFunction = value; }

		protected override void Start()
		{
			SetTextAsActiveText();
			SetKeyInputText(Controller.Instance.RebindingFunctions[rebindedFunction].defaultKeyName);
		}

		private void Update()
		{
			if (!isRebinding && Input.GetMouseButtonUp(0) && RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, Camera.main))
			{
				isRebinding = true;
				startRebind.Invoke();
				SetPlaceOlderAsActiveText();
				Controller.OnRebindEnd += Controller_OnRebindEnd;
				Controller.Instance.RebindingFunctions[rebindedFunction].function();
			}

			else if (Input.GetMouseButtonUp(0))
			{
				isRebinding = false;
			}
		}

		private void Controller_OnRebindEnd(InputControl obj)
		{
			SetTextAsActiveText();
			SetKeyInputText(obj);
			endRebind.Invoke(obj);
		}

		private void SetKeyInputText(InputControl control) => SetKeyInputText(control.name);
		private void SetKeyInputText(string text)
		{
			this.text.text = text;
		}

		public void SetPlaceOlderAsActiveText()
		{
			placeOlder.gameObject.SetActive(true);
			text.gameObject.SetActive(false);
		}

		public void SetTextAsActiveText()
		{
			placeOlder.gameObject.SetActive(false);
			text.gameObject.SetActive(true);
		}

		#if UNITY_EDITOR
		[Obsolete("This function can ONLY be used by Unity to generate a new KeyInputButton", false), MenuItem("GameObject/UI/KeyInputButton", false, 0)]
		public static void CreateKeyInputButton(MenuCommand menu)
		{
			Controller.TryCreateController(menu);
			Color textColor = new Color(50/255, 50/255, 50/255);

			//Text
			GameObject text = new GameObject("Text");
			Text text_text = text.AddComponent<Text>();
			text_text.color = textColor;
			text_text.supportRichText = false;
			text_text.raycastTarget = false;
			text_text.horizontalOverflow = HorizontalWrapMode.Overflow;



			//PlaceOlder
			GameObject placeOlder = new GameObject("Placeolder");
			Text placeOlder_text = placeOlder.AddComponent<Text>();
			placeOlder_text.text = "Press a key...";
			placeOlder_text.fontStyle = FontStyle.Italic;
			placeOlder_text.raycastTarget = false;
			textColor.a = 0.5f;
			placeOlder_text.color = textColor;




			//Root GameObject
			GameObject gameObject = new GameObject("KeyInputButton");
			gameObject.layer = 5;
			gameObject.AddComponent<RectTransform>();

			KeyInputButton gameObject_keyInputButton = gameObject.AddComponent<KeyInputButton>();
			gameObject_keyInputButton.text = text_text;
			gameObject_keyInputButton.placeOlder = placeOlder_text;

			Image gameObject_image = gameObject.AddComponent<Image>();
			gameObject_image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd");
			gameObject_image.color = Color.white;
			gameObject_image.raycastTarget = true;
			gameObject_image.type = Image.Type.Sliced;
			gameObject_image.fillCenter = true;


			text.transform.SetParent(gameObject.transform);
			placeOlder.transform.SetParent(gameObject.transform);


			GameObjectUtility.SetParentAndAlign(gameObject, menu.context as GameObject);
			GameObjectUtility.EnsureUniqueNameForSibling(gameObject);
			Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
			Selection.activeObject = gameObject;

			if (gameObject.transform.parent)
				gameObject.layer = gameObject.transform.parent.gameObject.layer;
		}
		#endif
	}


	[Serializable]
	public class RebindStartEvent : UnityEvent { }
	[Serializable]
	public class RebindEndEvent : UnityEvent<InputControl> { }
}