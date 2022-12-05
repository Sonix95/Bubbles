using TMPro;
using UnityEngine;

namespace Views
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreLabel;

        public void SetScore(int score)
        {
            _scoreLabel.text = $"SCORE: {score}";
        }
    }
}
