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

            _source.PlayOneShot(audio.Clip, audio.VolumeBoostFactor);
        }
    }

    public void PlayLoop(string clipName)
    {
        foreach (var audio in _sounds)  
        {
            if (audio.Name != clipName) continue;

            _source.clip = audio.Clip;
            _source.loop = true;
            if (!_source.isPlaying)
            {
                _source.Play();
            }
            
        }
    }

    public void Pause()
    {
        _source.Pause();
    }

    [Serializable]
    public class AudioData
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _volumeBoostFactor = 1.0f;

        public string Name => _name;
        public AudioClip Clip => _clip;
        public float VolumeBoostFactor => _volumeBoostFactor;
    }

}
