using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public static ShopManager instance;
    public static Action OnBuy;

    private Color _ballColor;
    private int _balance = 0;

    private string _saveBalance;


    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        _balance = PlayerPrefs.GetInt(_saveBalance, 0);
    }

    public void SetColor(Color color) {
        _ballColor = color;
    }
    
    public Color GetColor() {
        return _ballColor;
    }

    private bool CanBuy(int price) {
        return price < _balance;
    }

    public int GetBalance() {
        return _balance;
    }

    public bool Buy(Color ballColor, int price) {
        if (CanBuy(price)) { 
            SetColor(ballColor);
            _balance -= price;
            OnBuy?.Invoke();

            PlayerPrefs.SetInt(_saveBalance, _balance);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }

}
