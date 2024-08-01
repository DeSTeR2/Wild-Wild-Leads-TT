using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType {
    Win,
    CannotMove,
    CoinCollect
}

[Serializable]
public class Sound
{
    public SoundType _soundType;
    public AudioClip _audioClip;
}
