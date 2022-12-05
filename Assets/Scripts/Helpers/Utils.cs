using System;
using UnityEngine;

namespace Helpers
{
	public static class Utils
	{
		public static (float, float) GetScreenSize()
		{
			var height = Camera.main.orthographicSize * 2.0f;
			var wight = height * Screen.width / Screen.height;

			return (wight, height);
		}

		public static int GetSecondsFromStart(DateTime startTime)
		{
			var timerTime = DateTime.Now - startTime;
			int hours = Math.Max(0, Convert.ToInt32(Math.Floor(timerTime.TotalHours)));
			var minutes = 60 * hours + timerTime.Minutes;
			var seconds = 60 * minutes + timerTime.Seconds;

			return seconds;
		}
	}
}
