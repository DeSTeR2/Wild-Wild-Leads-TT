using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Board _board;

    [Header("Animation settings")]
    [SerializeField] float _duration;
    [SerializeField] Ease _ease;

    [Space]
    [SerializeField, Range(0,1)] float _playerSize;

    int x = 0, y = 0;
    Image _playerImage;

    bool _canMove = true;

    Stack<Vector3> _movePositoins = new Stack<Vector3>();
    Stack<Tuple<int,int>> _boardPosions = new Stack<Tuple<int, int>>();

    private void Start() {
        Board.OnBoardInitComplete += Init;
        _playerImage = GetComponent<Image>();

        if (ShopManager.instance != null) _playerImage.color = ShopManager.instance.GetColor();
    }

    private void Init() {
        transform.position = _board.GetStartPosition();

        float size = _board.GetBoardSize();
        RectTransform rect = GetComponent<RectTransform>(); 

        rect.sizeDelta = new Vector2 (size * _playerSize, size * _playerSize);
    }

    public void Move(Direction move) {
        if (_canMove == false) return;
        _canMove = false;

        Vector3 targetPosition = _board.GetNewPlayerPosition(x, y, move, _playerImage.color);

        if (targetPosition == Vector3.zero) {
            _canMove = true;
            return;
        } 

        _movePositoins.Push(transform.position);
        _boardPosions.Push(new Tuple<int, int>(x, y));

        switch (move) {
            case Direction.Up:
                x--;
                break;
            case Direction.Down:
                x++;
                break;
            case Direction.Left:
                y--;
                break;
            case Direction.Right:
                y++;
                break;
        }

        transform.DOMove(targetPosition, _duration).SetEase(_ease);
        StartCoroutine(EnableMove());
    }

    public void Undo() {
        if (_movePositoins.Count == 0) return;

        Tuple<int, int> pos = _boardPosions.Pop();

        x = pos.Item1;
        y = pos.Item2;

        Vector3 target = _movePositoins.Pop();
        transform.DOMove(target, _duration).SetEase(_ease);
        _canMove = false;

        StartCoroutine(EnableMove());
    }

    private IEnumerator EnableMove() {
        yield return new WaitForSeconds(_duration * 1.2f);
        _canMove = true;
    }

    private void OnDestroy() {
        Board.OnBoardInitComplete -= Init;
    }
}
