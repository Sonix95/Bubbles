using Helpers;
using SpawnElements;
using System;
using UnityEngine;

namespace Controllers
{
	public class SpawnController
	{
		public event EventHandler<Bubble> OnBubbleSpawn;

		private readonly Bubble _bubbleTemplete;
		private readonly float _generationOffset;
		private readonly Vector2 _sizeRange;
		private readonly float _timer;

		private (float wight, float height) _screenSize = default;
		private float _newBubbleZOffset = 0f;

		public SpawnController(Bubble bubbleTemplete, float generationOffset, Vector2 sizeRange, float timer)
		{
			_bubbleTemplete = bubbleTemplete;
			_generationOffset = generationOffset;
			_sizeRange = sizeRange;
			_timer = timer;

			_screenSize = Utils.GetScreenSize();
		}

		public void Spawn(DateTime startTime)
		{
			var bubbleSize = GetBubbleSize();
			var bubbleDisposePosition = _screenSize.height / 2 + _generationOffset + bubbleSize / 2;
			var initPosition = GetInitPosition(bubbleSize / 2, bubbleDisposePosition);

			var bubble = SpawnIternal(bubbleSize, initPosition, bubbleDisposePosition, _timer, startTime);

			OnBubbleSpawn?.Invoke(this, bubble);
		}

		public Bubble SpawnIternal(float size, Vector3 initPosition, float disposePosition, float seconds, DateTime startTime)
		{
			var bubble = GameObject.Instantiate<Bubble>(_bubbleTemplete, initPosition, Quaternion.identity);
			bubble.Init(size, disposePosition, seconds, startTime);
			bubble.gameObject.SetActive(true);

			_newBubbleZOffset += 0.001f;

			return bubble;
		}

		private float GetBubbleSize()
		{
			var randowSize = UnityEngine.Random.Range(_sizeRange.x, _sizeRange.y);
			return _screenSize.wight / randowSize;
		}

		private Vector3 GetInitPosition(float bubbleSize, float bubbleDisposePosition)
		{
			var generateYPos = -bubbleDisposePosition;
			var generateXMaxPos = _screenSize.wight / 2 - bubbleSize;
			var generateXMinPos = -generateXMaxPos;

			var generateXPos = UnityEngine.Random.Range(generateXMinPos, generateXMaxPos);

			return new Vector3(generateXPos, generateYPos, _newBubbleZOffset);
		}
	}
}
