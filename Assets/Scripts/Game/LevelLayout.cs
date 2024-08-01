using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelLayout
{
    public Matrix matrix;

    public Matrix GetMatrix() {
        return matrix;
    }
}

[Serializable]
public class Matrix {
    public List<Row> matrix;
}

[Serializable]
public class Row {
    public List<int> row;
}