using System;
using System.Collections;
using UnityEngine;
using Views;
using Controllers;
using UnityEngine.UI;
using SpawnElements;

public class GameManager : MonoBehaviour
{
	[Header("Bubble Template")]
	[SerializeField] private Bubble _bubbleTamplate = default;

	[Header("Options")]
	[SerializeField] private float _sessionTimer = default;
	[SerializeField] private Vector2 _bubbleSizeFromTo = default;
	[SerializeField] private Vector2 _spawnTimeRange = default;
	[SerializeField] private float _screenOffset = default;

	[Header("UI")]
	[SerializeField] private Button _restartButton = default;
	[SerializeField] private GameObject _backgroundGO = default;
	[SerializeField] private TimerView _timerView = default;
	[SerializeField] private GameObject _timerViewGO = default;
	[SerializeField] private ScoreView _scoreView = default;
	[SerializeField] private GameObject _scoreViewGO = default;
	[SerializeField] private ScoreView _scoreViewResult = default;
	[SerializeField] private GameObject _scoreViewGOResult = default;

	private SpawnController _spawnController;
	private BubbleManager _bubbleManager;

	private DateTime _startTime;
	private DateTime _timeToEnd;

	private int _score;

	private void Awake()
	{
		Init();
		StartFlow();
	}

	private void Init()
	{
		_spawnController = new SpawnController(_bubbleTamplate, _screenOffset, _bubbleSizeFromTo, _sessionTimer);
		_bubbleManager = new BubbleManager(_spawnController);

		_bubbleManager.OnBubbleClick += OnBubbleClicked;
		_restartButton.onClick.AddListener(RestartGame);
	}

	private void StartFlow()
	{
		_startTime = DateTime.Now;
		_timeToEnd = DateTime.Now.AddSeconds(_sessionTimer);

		_timerViewGO.SetActive(true);
		_scoreViewGO.SetActive(true);

		StartCoroutine(GenerateBubbles());

		_bubbleManager.ResetLock();
	}

	private void OnBubbleClicked(object sender, int e)
	{
		_score += e;
		_scoreView.SetScore(_score);
	}

	private void RestartGame()
	{
		_bubbleManager.Clear();

		_score = 0;
		_scoreView.SetScore(_score);

		_restartButton.gameObject.SetActive(false);
		_backgroundGO.SetActive(false);
		_scoreViewGOResult.SetActive(false);

		StartFlow();
	}

	private void Update()
	{
		if (DateTime.Now > _timeToEnd)
		{
			StopFlow();
		}
		else
		{
			UpdateIternal();
		}
	}

	private void StopFlow()
	{
		_bubbleManager.SetLock();

		StopAllCoroutines();

		_timerViewGO.SetActive(false);
		_scoreViewGO.SetActive(false);

		_backgroundGO.SetActive(true);
		_scoreViewResult.SetScore(_score);
		_scoreViewGOResult.SetActive(true);
		_restartButton.gameObject.SetActive(true);
	}

	private void UpdateIternal()
	{
		_timerView.SetTime(_timeToEnd - DateTime.Now);
	}

	private IEnumerator GenerateBubbles()
	{
		yield return null;

		_spawnController.Spawn(_startTime);

		while (true)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(_spawnTimeRange.x, _spawnTimeRange.y));

			_spawnController.Spawn(_startTime);
		}
	}
}
