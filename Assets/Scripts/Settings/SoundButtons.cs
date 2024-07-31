using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtons : MonoBehaviour {
    [SerializeField] GameObject _on;
    [SerializeField] GameObject _off;
    [SerializeField] string _soundSave = "Sound";

    private void Start() {
        int sound = PlayerPrefs.GetInt(_soundSave);
        if (sound == 1) {
            _on.SetActive(false);
            _off.SetActive(true);
        } else {
            _on.SetActive(true);
            _off.SetActive(false);
        }
    }


    public void ChangeButtons() {
        switch (_on.active) {
            case true:
                SoundManager.instance.SetMute();
                _on.SetActive(false);
                _off.SetActive(true);
                break;
            case false:
                SoundManager.instance.SetUnMute();
                _on.SetActive(true);
                _off.SetActive(false);
                break;
        }
    }
}
