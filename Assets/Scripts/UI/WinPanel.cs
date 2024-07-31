using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] float _duration;
    [SerializeField] Ease _ease;
    [SerializeField] Transform _startPositon;
    [SerializeField] Transform _targetPosition;

    public void OnEnable() {
        transform.DOMove(_startPositon.position, 0);
        transform.DOMove(_targetPosition.position, _duration).SetEase(_ease);

    }

    private void OnDisable() {
        transform.DOMove(_startPositon.position, 0);
    }
}
