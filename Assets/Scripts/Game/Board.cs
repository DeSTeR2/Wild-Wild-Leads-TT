using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] Cell _cellPrefab;

    [Header("Colors")]
    [SerializeField] Color _blackColor;
    [SerializeField] Color _whiteColor;

    [Space]
    [SerializeField] float _coinProbability;

    GridLayoutGroup _layout;

    int _boardSize;
    int _paintTarget, _currentPaint;
    List<List<Cell>> _board;
    List<List<int>> _status;
    Stack<Cell> _cellPainted;

    public static Action OnBoardInitComplete;

    private void Start() {
        _layout = GetComponent<GridLayoutGroup>();
    }

    public void StartLevel(int boradSize, LevelLayout layout) {
        Matrix matrix = layout.GetMatrix();
        
        _boardSize = boradSize;
        _paintTarget = 0;
        _currentPaint = 1;

        if (_layout == null) {
            _layout = GetComponent<GridLayoutGroup>();
        }

        if (_board != null) {
            for (int i=0; i<_board.Count; i++) {
                for (int j=0; j < _board[i].Count; j++) {
                    _board[i][j].gameObject.SetActive(false);
                }
            }
        }

        _layout.constraintCount = boradSize;

        _board = new List<List<Cell>>();
        _status = new List<List<int>>();
        _cellPainted = new Stack<Cell>();

        for (int i=0; i<_boardSize;i++) {
            List<Cell> list = new List<Cell>(); 
            List<int> status = new List<int>();

            Row row = matrix.matrix[i];

            for (int j=0; j<_boardSize;j++) {
                float chanse = UnityEngine.Random.Range(0f, 1f);
                int status_ = 0;
                Cell cell = Instantiate(_cellPrefab, transform);
                cell.name = $"{i}, {j}";

                cell.Init(_whiteColor);
                _paintTarget++;

                int singleItem = row.row[j];

                if (singleItem == 1) {
                        cell.Init(_blackColor, false);
                        status_ = 1;
                        _paintTarget--;
                    
                } else if (chanse <=  + _coinProbability && !(i==0 && j==0)) {
                    cell.SetCoin();
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

            LevelManager.instance.CheckWin(_paintTarget, _currentPaint);

            _board[x][y].CollectCoin();
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
