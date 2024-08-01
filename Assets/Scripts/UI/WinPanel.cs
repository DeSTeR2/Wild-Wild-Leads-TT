using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] float _duration;
    [SerializeField] Ease _ease;
    [SerializeField] Transform _startPositon;
    [SerializeField] Transform _targetPosition;

    [Space]
    [SerializeField] TextMeshProUGUI _coinNumber;

    public void OnEnable() {
        transform.DOMove(_startPositon.position, 0);
        transform.DOMove(_targetPosition.position, _duration).SetEase(_ease);

        _coinNumber.text = LevelManager.instance.GetCoins().ToString();
    }

    private void OnDisable() {
        transform.DOMove(_startPositon.position, 0);
    }
}
