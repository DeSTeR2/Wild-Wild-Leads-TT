using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField] int _boardSize;
    
    public int GetBoardSize() {
        return _boardSize;
    }
}
