using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] Sprite _coin;
    [SerializeField] Image _coinImage;

    private bool _canStep;
    private Image _image;

    private Color _defColor;
    private bool _hasCoin = false;


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

        if (_hasCoin) {
            _coinImage.gameObject.SetActive(true);
            LevelManager.instance.UndoCollectCoin();
        }
    }

    public bool Paited() {
        return _canStep;
    }

    public bool CanStep() {
        return _canStep;
    }

    public void SetCoin() {
        _coinImage.gameObject.SetActive(true);
        _coinImage.sprite = _coin;
        _hasCoin = true;
    }

    public void CollectCoin() {
        if (_coinImage.gameObject.active == true) {
            _coinImage.gameObject.SetActive(false);
            LevelManager.instance.CollectCoin();
        }
    }
}
