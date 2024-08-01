using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Board _board;
    [SerializeField] GameObject _winPanel;

    [Space]
    [SerializeField] float _waitTimeToWin;

    public static LevelManager instance;
    public static Action OnWin;
    public static Action OnCoinCollect;

    private int _collectedCoin = 0;

    private void Awake() {
        instance = this;
    }

    private void Start() {

        _collectedCoin = 0;
        _board.StartLevel(10, 0.3f);
    }

    public void CollectCoin() {
        _collectedCoin++;
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
        }

        IEnumerator WaitTime() {
            yield return new WaitForSeconds(_waitTimeToWin);
            _winPanel.SetActive(true);
        }
    }

    public void NewLevel() {

    }
}
