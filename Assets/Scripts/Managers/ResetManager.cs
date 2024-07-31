using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    [SerializeField] Board _board;
    [SerializeField] Player _player;
    public void UndoMove() {
        _board.Undo();
        _player.Undo();
    }
}
