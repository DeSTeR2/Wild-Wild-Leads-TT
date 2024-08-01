using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Board _board;
    [SerializeField] GameObject _winPanel;
    [SerializeField] TextMeshProUGUI _levelText;

    [Space]
    [SerializeField] Level[] _levels;
    [SerializeField] float _waitTimeToWin;

    public static LevelManager instance;
    public static Action OnWin;
    public static Action OnCoinCollect;

    private int _currentLevel;
    private int _collectedCoin = 0;

    private string _saveLevel = "Level";

    private void Awake() {
        instance = this;
    }

    private void Start() {
        _collectedCoin = 0;
        _currentLevel = PlayerPrefs.GetInt(_saveLevel, 0);

        StartLevel();
    }

    public void CollectCoin() {
        _collectedCoin++;
        SoundManager.instance.PlaySound(SoundType.CoinCollect);
    }

    public void UndoCollectCoin() {
        _collectedCoin--;
    }

    public int GetCoins() {
        return _collectedCoin;
    }

    public void CheckWin(int target, int current) {
        if (target == current) {
            StartCoroutine(WaitTime());
            ShopManager.instance.AddBalance(_collectedCoin);
            SoundManager.instance.PlaySound(SoundType.Win);
            OnWin?.Invoke();

            _currentLevel++;
            if (_currentLevel == _levels.Length) {
                _currentLevel = 0;
            }

            PlayerPrefs.SetInt(_saveLevel, _currentLevel);
        }

        IEnumerator WaitTime() {
            yield return new WaitForSeconds(_waitTimeToWin);
            _winPanel.SetActive(true);
        }
    }

    public void StartLevel() {
        _collectedCoin = 0;
        _levelText.text = $"Level {_currentLevel + 1}";

        Level curLevel = _levels[_currentLevel];
        int boardSize = curLevel.GetBoardSize();

        LevelLayout matrix = curLevel.GetLevelLayout();

        _board.StartLevel(boardSize, matrix);
    }

    public void NewLevel() {
        StartLevel();
    }
}
