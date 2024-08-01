using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField] int _boardSize;
    [SerializeField] LevelLayout _levelLayout;

    public int GetBoardSize() {
        return _boardSize;
    }

    public LevelLayout GetLevelLayout() {
        return _levelLayout;
    }
}
