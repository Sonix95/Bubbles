using System.Collections.Generic;
using System;
using UnityEngine;
using SpawnElements;

namespace Controllers
{
	public class BubbleManager
	{
		public event EventHandler<int> OnBubbleClick;

		private readonly SpawnController _spawnController;

		private List<Bubble> _bubbles;

		private bool _lock;

		private void AddBubble(Bubble bubble)
		{
			_bubbles.Add(bubble);
		}

		private void RemoveBubble(Bubble bubble)
        {
			_bubbles.Remove(bubble);
		}

		public BubbleManager(SpawnController spawnController)
		{
			_spawnController = spawnController;
			_spawnController.OnBubbleSpawn += OnBubbleSpawned;

			_bubbles = new List<Bubble>();
		}

		public void SetLock()
		{
			_lock = true;
		}

		public void ResetLock()
		{
			_lock = false;
		}

		public void Clear()
		{
			foreach (var bubble in _bubbles)
			{
				DestroyBubbleGO(bubble);
			}

			_bubbles.Clear();
		}

		private void OnBubbleSpawned(object sender, Bubble e)
		{
			e.OnDesposePositonReach += OnDesposePositonReached;
			e.OnClick += OnBubbleClicked;

			AddBubble(e);
		}

		private void OnDesposePositonReached(object sender, Bubble bubble)
		{
			DestroyBubbleGO(bubble);
			RemoveBubble(bubble);
		}

		private void OnBubbleClicked(object sender, Bubble bubble)
		{
			if (!_lock)
			{
				var bubbleSize = bubble.transform.localScale.x;
				OnBubbleClick?.Invoke(this, (int)(100 / bubbleSize));
				DestroyBubbleGO(bubble);
				RemoveBubble(bubble);
			}
		}

		private void DestroyBubbleGO(Bubble bubble)
		{
			bubble.OnDesposePositonReach -= OnDesposePositonReached;
			bubble.OnClick -= OnBubbleClicked;

			GameObject.Destroy(bubble.gameObject);
		}
	}
}
