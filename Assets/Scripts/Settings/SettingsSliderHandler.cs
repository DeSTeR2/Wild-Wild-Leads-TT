using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public enum SliderSettingsType {
    Sound, 
    Music
}

public class SettingsSliderHandler : MonoBehaviour
{
    [SerializeField] SliderSettingsType _type;

    Slider _slider;

    private void Awake() {
        _slider = GetComponent<Slider>();
    }
    private void OnEnable() {
        if (SoundManager.instance != null) _slider.value = SoundManager.instance.GetVolume(_type);
    }

    public void Operation() {
        switch (_type) {
            case SliderSettingsType.Sound:
                SoundManager.instance.ChangeSoundVolume(_slider);
                break;
            case SliderSettingsType.Music:
                SoundManager.instance.ChangeMusicVolume(_slider);
                break;
        }
    }
}
