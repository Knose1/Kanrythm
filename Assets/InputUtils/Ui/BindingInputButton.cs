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
using Com.Github.Knose1.InputUtils.InputController;
using UnityEngine.UI;
using UnityEditor;
using Com.Github.Knose1.InputUtils.Utils;
using Com.Github.Knose1.InputUtils.Settings;

namespace Com.Github.Knose1.InputUtils.Ui {
	[AddComponentMenu(InputUtils_Path.MENU_ITEM_ROOT_NAME+nameof(BindingInputButton))]
	public class BindingInputButton : UIBehaviour
	{
		[SerializeField] public Text text;
		[SerializeField] public Text placeOlder;

		[SerializeField] private RebindStartEvent startRebind = default;
		public RebindStartEvent StartRebind { get => startRebind; }

		[SerializeField] private RebindEndEvent endRebind = default;
		public RebindEndEvent EndRebind { get => endRebind; }

		[SerializeField, HideInInspector] protected int rebindedFunction;
		public int RebindedFuction { get => rebindedFunction; set => rebindedFunction = value; }

		protected bool _isRebinding;
		protected bool _isLeftButton;

		protected override void Start()
		{
			SetTextAsActiveText();
			SetKeyInputText(Controller.Instance.RebindingFunctions[rebindedFunction].defaultKeyName);
		}

		private void Update()
		{
			if (!_isRebinding && Input.GetMouseButtonUp(0) && RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, Camera.main))
			{
				_isRebinding = true;
				startRebind.Invoke();
				SetPlaceOlderAsActiveText();
				Controller.OnRebindEnd += Controller_OnRebindEnd;
				Controller.Instance.RebindingFunctions[rebindedFunction].function();
			}

			else if (_isLeftButton ? Input.GetMouseButtonUp(0) : !Controller.IsRebinding)
			{
				_isRebinding = false;
				_isLeftButton = false;
			}
		}

		private void Controller_OnRebindEnd(InputControl obj)
		{
			if (!_isRebinding) return;

			if (Input.GetMouseButton(0))
			{
				_isLeftButton = true;
			}

			SetTextAsActiveText();
			SetKeyInputText(obj);
			endRebind.Invoke(obj);
		}

		private void SetKeyInputText(InputControl control) => SetKeyInputText(InputSystemUtils.GetName(control));
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
		[Obsolete("This function can ONLY be used by Unity to generate a new KeyInputButton", false)]
		[MenuItem("GameObject/Input/"+nameof(BindingInputButton), false, 11)]
		[MenuItem(InputUtils_Path.MENU_ITEM_ROOT_NAME+"Create " +nameof(BindingInputButton)+" Object", false, 2)]
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

			RectTransform text_transform = text.transform as RectTransform;
			text_transform.anchorMin = Vector2.zero;
			text_transform.pivot = Vector2.up;
			text_transform.anchorMax = Vector2.one;
			text_transform.localPosition = new Vector2(12, -12);
			text_transform.sizeDelta = new Vector2(100 - 12 * 2, 100 - 12 * 2);



			//PlaceOlder
			GameObject placeOlder = new GameObject("Placeolder");
			Text placeOlder_text = placeOlder.AddComponent<Text>();
			placeOlder_text.text = "Press a key...";
			placeOlder_text.fontStyle = FontStyle.Italic;
			placeOlder_text.raycastTarget = false;
			textColor.a = 0.5f;
			placeOlder_text.color = textColor;

			RectTransform placeOlder_transform = placeOlder.transform as RectTransform;
			placeOlder_transform.anchorMin = Vector2.zero;
			placeOlder_transform.pivot = Vector2.up;
			placeOlder_transform.anchorMax = Vector2.one;
			placeOlder_transform.localPosition = new Vector2(12, -12);
			placeOlder_transform.sizeDelta = new Vector2(100 - 12 * 2, 100 - 12 * 2);



			//Root GameObject
			GameObject gameObject = new GameObject(nameof(BindingInputButton));
			gameObject.layer = 5;
			gameObject.AddComponent<RectTransform>();

			BindingInputButton gameObject_keyInputButton = gameObject.AddComponent<BindingInputButton>();
			gameObject_keyInputButton.text = text_text;
			gameObject_keyInputButton.placeOlder = placeOlder_text;

			Image gameObject_image = gameObject.AddComponent<Image>();
			gameObject_image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd");
			gameObject_image.color = Color.white;
			gameObject_image.raycastTarget = true;
			gameObject_image.type = Image.Type.Sliced;
			gameObject_image.fillCenter = true;

			(gameObject.transform as RectTransform).pivot = Vector2.up;



			text.transform.SetParent(gameObject.transform, true);
			placeOlder.transform.SetParent(gameObject.transform, true);



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