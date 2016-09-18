using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GodTouches
{
	/// <summary>
	/// 「クリック」「長押し」「ドラッグ」サンプル
	/// </summary>
	public class ClickLongPressDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		/// <summary>
		/// イベントタイプ
		/// </summary>
		public enum EventType { None, Pressed, Click, LongPress, Drag }

		/// <summary>
		/// クリック・ドラッグ判定距離
		/// </summary>
		public float CheckDistance = 30;
		/// <summary>
		/// 長押し判定時間
		/// </summary>
		public float CheckTime = 0.3f;
		/// <summary>
		/// サンプル表示テキスト
		/// </summary>
		public Text Text;

		/// <summary>
		/// イベントタイプ
		/// </summary>
		EventType type;
		/// <summary>
		/// イベント実行中かどうか
		/// </summary>
		bool isRunning;
		/// <summary>
		/// 押された時の開始ポジション
		/// </summary>
		Vector3 startPos;
		/// <summary>
		/// 押された時の開始時間
		/// </summary>
		float startTime;

		/// <summary>
		/// イベント実行中かどうか
		/// </summary>
		public bool IsRunning { get { return isRunning; } }

		/// <summary>
		/// イベントタイプ設定
		/// </summary>
		/// <param name="type">イベントタイプ</param>
		void SetType(EventType type)
		{
			this.type = type;
			Text.text = type.ToString ();
		}

		/// <summary>
		/// 押された
		/// </summary>
		/// <param name="e">PointerEventData</param>
		public void OnPointerDown (PointerEventData e)
		{
			isRunning = true;
			SetType(EventType.Pressed);
			startPos = GodTouch.GetPosition ();
			startTime = Time.time;
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
			if (type == EventType.Pressed)
			{
				// 押されてる
				var pos = GodTouch.GetPosition ();
				var dx  = Mathf.Abs(pos.x - startPos.x);
				var dy  = Mathf.Abs(pos.y - startPos.y);
				var dt  = Time.time - startTime;
				if(dx > CheckDistance || dy > CheckDistance)
				{
					// 一定距離動いていたらドラッグ実行
					SetType(EventType.Drag);
				}
				else if(dt > CheckTime)
				{
					// 一定時間経過していたら長押し実行
					SetType(EventType.LongPress);
				}
			}
			else if (type == EventType.Drag)
			{
				// ドラッグ中(動かす)
				transform.position = GodTouch.GetPosition();
			}
		}

		/// <summary>
		/// 離された
		/// </summary>
		/// <param name="e">PointerEventData</param>
		public void OnPointerUp (PointerEventData e)
		{
			if (type == EventType.Pressed)
			{
				// 他のイベントが未入力ならクリック実行
				SetType (EventType.Click);
			}
			else
			{
				// イベント初期化
				SetType (EventType.None);
			}
			isRunning = false;
		}
	}
}