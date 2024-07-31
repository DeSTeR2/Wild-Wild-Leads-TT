using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Shop", menuName = "Shop/Shop", order = 0)]
public class ShopScriptable : ScriptableObject
{
    [SerializeField] Color[] _refColors;
    Dictionary<int, Color> _colors = new Dictionary<int, Color>();

    public void Start() {
        for (int i=0; i< _refColors.Length; i++) {
            _colors.Add(i, _refColors[i]);
        }
    }

    public Color GetColor(int number) {
        return _colors[number];
    }

}
