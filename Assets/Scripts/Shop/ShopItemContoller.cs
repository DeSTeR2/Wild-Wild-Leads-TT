using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemContoller : MonoBehaviour
{
    List<ShopItem> _items = new List<ShopItem>();

    private void Start() {
        int childCount = transform.childCount;
        for (int i=0; i < childCount; i++) {
            _items.Add(transform.GetChild(i).GetComponent<ShopItem>());
        }
        Debug.Log(_items.Count);
        ShopItem.OnSelect += UnSelect;
    }

    private void UnSelect() {
        Debug.Log("select");
        for (int i=0; i < _items.Count; i++) {
            _items[i].UpdateStatus();
        }
    }

    private void OnDestroy() {
        ShopItem.OnSelect -= UnSelect;
    }
}
