using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    [Header("Content")]
    [SerializeField] Color _color;
    [SerializeField] int _price;

    [Header("Items")]
    [SerializeField] Button _buyBtn;
    [SerializeField] Button _selectBtn;
    [SerializeField] TextMeshProUGUI _priceText;
    [SerializeField] Image _statusImage;
    [SerializeField] Image _ballImage;

    [Header("Status")]
    [SerializeField] Color _owned;
    [SerializeField] Color _unOwned;
    [SerializeField] Color _selected;

    public static Action OnSelect;
    public static Action<Color> OnChangeColor;

    private int _ownedStatus;
    private string _selectedStatusSave;

    private void Start() {
        UpdateStatus();
        _ballImage.color = _color;

        _priceText.text = _price.ToString();

        _buyBtn.onClick.AddListener(delegate { Buy(); });
        _selectBtn.onClick.AddListener(delegate { Select(); });

        string selected = PlayerPrefs.GetString(_selectedStatusSave, "RGBA(1.000, 0.000, 0.286, 1.000)");
        if (selected == _color.ToString()) {
            Select();
        }
    }

    public void UpdateStatus() {
        if (_price == 0) {
            PlayerPrefs.SetInt(_color.ToString(), 1);
            PlayerPrefs.Save();
        }

        _ownedStatus = PlayerPrefs.GetInt(_color.ToString(), 0);

        if (_ownedStatus == 0) {
            _statusImage.color = _unOwned;
        } else {
            _statusImage.color = _owned;
        }
    }

    private void Select() {
        if (_ownedStatus == 0) return;
        OnSelect?.Invoke();
        OnChangeColor?.Invoke(_color);

        PlayerPrefs.SetString(_selectedStatusSave, _color.ToString());
        PlayerPrefs.Save();

        _statusImage.color = _selected;
        ShopManager.instance.SetColor(_color);
    }

    private void Buy() {
        if (_ownedStatus == 1) return;

        if (ShopManager.instance.Buy(_color, _price)) {
            PlayerPrefs.SetInt(_color.ToString(), 1);
            PlayerPrefs.Save();

            _ownedStatus = 1;

            Select();
        }
    }

}
