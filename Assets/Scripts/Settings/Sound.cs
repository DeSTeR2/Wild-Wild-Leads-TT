using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType {
    PopEffect,
    PopApear,
    OpenCharacter,
    Watering,
    AppleEat,
    PanelMove,
    Error,
    AddScore,
    EggOpen
}

[Serializable]
public class Sound
{
    public SoundType _soundType;
    public AudioClip _audioClip;
}
