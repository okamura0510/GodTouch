using UnityEngine;

namespace GodTouches
{
	/// <summary>
	/// デモシーン
	/// </summary>
	public class Demo : MonoBehaviour
	{
		public Transform Move;
		public ClickLongPressDrag ClickLongPressDrag;

		Vector3 startPos;

		void Start()
		{
			startPos = Move.position;
		}

		void Update()
		{
			if (ClickLongPressDrag.IsRunning) return; // 他のサンプルが動作してる時は無効

			// タッチを検出して動かす
			var phase = GodTouch.GetPhase ();
            if (phase == GodPhase.Began) 
			{
				startPos = Move.position;
			}
            else if (phase == GodPhase.Moved) 
			{
				Move.position = GodTouch.GetPosition();
//				Move.position += GodTouch.GetDeltaPosition(); 
			}
            else if (phase == GodPhase.Ended) 
			{
				Move.position = startPos;
			}
		}
	}
}