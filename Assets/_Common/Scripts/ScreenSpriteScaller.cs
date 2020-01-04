///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 03/01/2020 19:28
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.Github.Knose1.Common {
	[RequireComponent(typeof(SpriteRenderer))]
	public class ScreenSpriteScaller : MonoBehaviour {

		public Vector2 canvasSize;

		protected SpriteRenderer spriteRenderer;
		public SpriteRenderer SpriteRenderer { 
			get { 
				return spriteRenderer = spriteRenderer ? spriteRenderer : GetComponent<SpriteRenderer>();
			} 
		}
		public Sprite Sprite => SpriteRenderer.sprite;
		public Texture2D Texture => Sprite.texture;


		protected void Update () {
			bool isHorizontal = Texture.width > Texture.height;
			float widthRatio;
			float heightRatio;
			if (canvasSize == default)
			{
				widthRatio  = (float)Screen.width  / (Texture.width);
				heightRatio = (float)Screen.height / (Texture.height);
			}
			else
			{
				widthRatio  = canvasSize.x / (Texture.width);
				heightRatio = canvasSize.y / (Texture.height);
			}

			float ratio = isHorizontal ? heightRatio : widthRatio;

			gameObject.transform.localScale = new Vector3(ratio, ratio, 1);
		}

		private const int DEFAULT_PIXEL_PER_UNIT = 100;

		public static ScreenSpriteScaller GenerateFillTexture(Texture2D texture, Transform parent = null, int pixelPerUnit = DEFAULT_PIXEL_PER_UNIT, string name = nameof(ScreenSpriteScaller))
		{
			GameObject gameObject = new GameObject(name);
			gameObject.transform.parent = parent;

			Sprite sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height), new Vector2(1 / 2f, 1 / 2f), pixelPerUnit);

			SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
			renderer.sprite = sprite;

			return gameObject.AddComponent<ScreenSpriteScaller>();
		}
	}
}