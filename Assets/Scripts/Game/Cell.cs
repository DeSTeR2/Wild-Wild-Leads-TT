using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private bool _canStep;
    private Image _image;

    private Color _defColor;

    private void Start() {
        _image = GetComponent<Image>();
    }

    public void Init(Color color, bool canStep = true) {
        if (_image == null) {
            _image = GetComponent<Image>();
        }
        _defColor = color;

        _image.color = color;
        _canStep = canStep;
    }

    public void ReColor(Color color) {
        _image.color = color;
        _canStep = false;
    }

    public void Reset() {
        _image.color = _defColor;
        _canStep = true;
    }

    public bool Paited() {
        return _canStep;
    }

    public bool CanStep() {
        return _canStep;
    }
}
