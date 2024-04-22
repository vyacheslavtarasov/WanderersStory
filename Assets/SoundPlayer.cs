using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioData[] _sounds;

    public void Play(string clipName)
    {
        foreach (var audio in _sounds)
        {
            if (audio.Name != clipName) continue;

            _source.PlayOneShot(audio.Clip);
        }
    }

    [Serializable]
    public class AudioData
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _clip;

        public string Name => _name;
        public AudioClip Clip => _clip;
    }

}
