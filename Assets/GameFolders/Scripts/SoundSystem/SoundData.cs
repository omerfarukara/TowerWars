using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Data/Sound Data")]
public class SoundData : ScriptableObject
{
    [SerializeField] SoundClip[] soundClips;

    public SoundClip[] SoundClips => soundClips;
}
