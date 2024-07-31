using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    [SerializeField] AudioSource sound;
    [SerializeField] AudioSource music;

    [Header("Sound")]
    [SerializeField] Sound[] _sounds;
    
    [Space]
    [SerializeField] float _soundDelay = 0.5f;

    [Space]
    [SerializeField] string _soundSave = "Sound";
    [SerializeField] string _musicSave = "Music";
    [SerializeField] string _soundPlaySave = "SoundOn";

    static float soundVolume;
    static float musicVolume;

    public static SoundManager instance;

    Dictionary<SoundType, AudioClip> _audios = new Dictionary<SoundType, AudioClip>();
    Dictionary<SoundType, float> _timers = new Dictionary<SoundType, float>();

    private void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetAudios();    
        }
        else {
            if (instance != this) {
                Destroy(this.gameObject);
            }
        }
    }

    private void Start() {
        soundVolume = PlayerPrefs.GetFloat(_soundSave, 1);
        musicVolume = PlayerPrefs.GetFloat(_musicSave, 1);

        sound.volume = soundVolume;
        music.volume = musicVolume;
    }

    public float GetVolume(SliderSettingsType type) {
        switch (type) {
            case SliderSettingsType.Sound:
                return sound.volume;
            case SliderSettingsType.Music:
                return music.volume;
        }

        return 1;
    }

    private void SetAudios() {
        foreach (var sound in _sounds) {
            if (!_audios.ContainsKey(sound._soundType)) {
                _audios.Add(sound._soundType, sound._audioClip);
                _timers.Add(sound._soundType, 0);
            }
        }
    }

    public void ChangeSoundVolume(Slider slider) {
        soundVolume = slider.value;
        sound.volume = soundVolume;

        PlayerPrefs.SetFloat(_soundSave, soundVolume);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(Slider slider) {
        musicVolume = slider.value;
        music.volume = musicVolume;

        PlayerPrefs.SetFloat(_musicSave, musicVolume);
        PlayerPrefs.Save();
    }

    public void SetMute() {
        PlayerPrefs.SetInt(_soundPlaySave, 1);
        PlayerPrefs.Save();
        Mute();
    }

    public void SetUnMute() {
        PlayerPrefs.SetInt(_soundPlaySave, 0);
        PlayerPrefs.Save();
        UnMute();
    }

    public void Mute() {
        sound.volume = 0;
        music.volume = 0;
    }

    public void UnMute() {
        sound.volume = soundVolume;
        music.volume = musicVolume;
    }

    public void PlaySound(SoundType type) {
        if (CanPlay(type)) {
            sound.PlayOneShot(_audios[type]);
        }
    }

    private bool CanPlay(SoundType type) {
        if (_timers.ContainsKey(type) == false) return false;
        
        float lastTimePlayer = _timers[type];

        if (lastTimePlayer + _soundDelay <= Time.time) {
            _timers[type] = Time.time;
            return true;
        }

        return false;
    }

}