using System;
using TMPro;
using UnityEngine;

namespace Views
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerLabel;

        public void SetTime(TimeSpan timeLeft)
        {
            int hours = Math.Max(0, Convert.ToInt32(Math.Floor(timeLeft.TotalHours)));
            int minutes = Math.Max(0, 60 * hours + timeLeft.Minutes);
            int seconds = Math.Max(0, timeLeft.Seconds);

            _timerLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
