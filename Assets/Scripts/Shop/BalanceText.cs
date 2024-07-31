using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalanceText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _balance;

    private void Start() {
        ShopManager.OnBuy += UpdateBalance;
    }

    private void OnEnable() {
        UpdateBalance();
    }

    public void UpdateBalance() {
        _balance.text = ShopManager.instance.GetBalance().ToString();
    }

    private void OnDisable() {
        ShopManager.OnBuy -= UpdateBalance;
    }
}
