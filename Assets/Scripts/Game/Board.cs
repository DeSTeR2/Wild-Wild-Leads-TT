using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] Cell _cellPrefab;

    [Header("Colors")]
    [SerializeField] Color _blackColor;
    [SerializeField] Color _whiteColor;

    GridLayoutGroup _layout;

    int _boardSize;
    int _paitTarget, _currentPaint;
    List<List<Cell>> _board;
    List<List<int>> _status;
    Stack<Cell> _cellPainted;

    public static Action OnBoardInitComplete;

    private void Start() {
        _layout = GetComponent<GridLayoutGroup>();
    }

    public void StartLevel(int boradSize, float blackPercent = 0.05f) {
        _boardSize = boradSize;
        _paitTarget = 0;
        _currentPaint = 0;

        if (_layout == null) {
            _layout = GetComponent<GridLayoutGroup>();
        }

        _layout.constraintCount = boradSize;

        _board = new List<List<Cell>>();
        _status = new List<List<int>>();
        _cellPainted = new Stack<Cell>();

        for (int i=0; i<_boardSize;i++) {
            List<Cell> list = new List<Cell>(); 
            List<int> status = new List<int>();

            for (int j=0; j<_boardSize;j++) {
                float change = UnityEngine.Random.Range(0f, 1f);
                int status_ = 0;
                Cell cell = Instantiate(_cellPrefab, transform);
                cell.name = $"{i}, {j}";

                if (change <=  blackPercent) {

                    int x = i;
                    int y = j;

                    bool canSpawn = true;

                    for (int k=-2; k < 0; k++) {
                        for (int p=-2; p < 0; p++) {
                            if ((x+k<0 || y+p<0) || _board[x + k][y+p].CanStep() == false) {
                                canSpawn = false;
                            }
                        }
                    }

                    if (canSpawn == false) {
                        cell.Init(_whiteColor);
                        _paitTarget++;

                    } else {
                        cell.Init(_blackColor, false);
                        status_ = 1;
                    }
                } else {
                    cell.Init(_whiteColor);
                    _paitTarget++;
                }

                status.Add(status_);
                list.Add(cell);

            }

            _board.Add(list);
            _status.Add(status);
        }

        if (ShopManager.instance != null) _board[0][0].ReColor(ShopManager.instance.GetColor());

        StartCoroutine(Complete());
        
        IEnumerator Complete() {
            yield return new WaitForNextFrameUnit();
            OnBoardInitComplete?.Invoke();
        }
    }

    public int GetBoardSize() {
        if (_layout == null) {
            _layout = GetComponent<GridLayoutGroup>();
        }

        return (int)_layout.cellSize.x;
    }

    private bool CheckLevel() {
        return true;
    }

    public Vector3 GetNewPlayerPosition(int x, int y, Direction dir, Color playerColor) {
        switch (dir) {
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

        if (x < 0 || y < 0 || x >= _boardSize || y >= _boardSize) return Vector3.zero;

        if (_board[x][y].CanStep()) {
            _board[x][y].ReColor(playerColor);
            _cellPainted.Push(_board[x][y]);
            _currentPaint++;

            LevelManager.instance.CheckWin(_paitTarget, _currentPaint);

            return _board[x][y].transform.position;
        }

        return Vector3.zero;
    }

    public void Undo() {
        if (_cellPainted.Count == 0) return;

        _cellPainted.Pop().Reset();
        _currentPaint--;
    }

    public Vector3 GetStartPosition() {
        return _board[0][0].transform.position;
    }
}
