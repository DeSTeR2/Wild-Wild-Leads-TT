using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public static ShopManager instance;
    public static Action OnBuy;

    private Color _ballColor;
    private int _balance;

    private string _saveBalance = "BalanceSave";
    private string _saveColor = "ColorSave";


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
        string color = PlayerPrefs.GetString(_saveColor, "RGBA(1.000, 0.000, 0.286, 1.000)");
        
        string[] rgba = color.Substring(5, color.Length - 6).Split(new string[] { ", " }, System.StringSplitOptions.None);
        float [] colors = new float[rgba.Length];

        for (int i = 0; i < rgba.Length; i++) {
            colors[i] = float.Parse(rgba[i], CultureInfo.InvariantCulture.NumberFormat);            
        }

        _ballColor = new Color(colors[0], colors[1], colors[2], colors[3]);
    }

    public void SetColor(Color color) {
        _ballColor = color;
        PlayerPrefs.SetString(_saveColor, color.ToString());
    }
    
    public Color GetColor() {
        return _ballColor;
    }

    private bool CanBuy(int price) {
        return price <= _balance;
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

    public void AddBalance(int toAdd) {
        _balance += toAdd;
        PlayerPrefs.SetInt(_saveBalance, _balance);
        PlayerPrefs.Save();
    }
}
