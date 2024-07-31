using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBallImage : MonoBehaviour
{
    Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        ShopItem.OnChangeColor += UpdateImge;
    }

    private void UpdateImge(Color color) {
        _image.color = color;
    }

    private void OnDestroy() {
        ShopItem.OnChangeColor -= UpdateImge;
    }
}