using Helpers;
using System;
using UnityEngine;

namespace SpawnElements
{
    public class Bubble : MonoBehaviour
    {
        public event EventHandler<Bubble> OnClick;
        public event EventHandler<Bubble> OnDesposePositonReach;

        [SerializeField] private float _speedOffset = 1;

        private float _speedSizeOffset = 1;
        private float _disposePosition = default;
        private float _totalSeconds = default;
        private float _timerMultilier = default;

        private DateTime _startTime = default;

        public void Init(float size, float disposePosition, float totalSeconds, DateTime startTime)
        {
            transform.localScale = new Vector3(size, size, size);

            _speedSizeOffset = _speedOffset / transform.localScale.x;
            _disposePosition = disposePosition;
            _totalSeconds = totalSeconds;
            _startTime = startTime;

            gameObject.name = "Bubble_" + Time.realtimeSinceStartup.ToString();
        }

        private void OnMouseDown()
        {
            OnClick?.Invoke(this, this);
        }

        private void Update()
        {
            if (_disposePosition < transform.position.y)
            {
                OnDesposePositonReach?.Invoke(this, this);
            }
            else
            {
                UpdateTimerMultilier(Utils.GetSecondsFromStart(_startTime));

                transform.Translate(Time.deltaTime * GetDirectionSpeed());
            }
        }

        private float GetSpeed()
        {
            return _speedSizeOffset * _timerMultilier;
        }

        private Vector3 GetDirectionSpeed()
        {
            return Vector3.up * GetSpeed();
        }

        private void UpdateTimerMultilier(float time)
        {
            _timerMultilier = 1 + (time / _totalSeconds);
        }
    }
}