using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum place {left, right}

[Serializable]
public class DialogData
{
    [SerializeField] private string[] _sentences;
    [SerializeField] private string _speakerName;
    [SerializeField] private place _place;
    [SerializeField] private Color _speakerColor;
    [SerializeField] private string _speakerSound;
    public string[] Sentences => _sentences;
    public string SpeakerName => _speakerName;

    public place Place => _place;

    public Color SpeakerColor => _speakerColor;
    public string SpeakerSound => _speakerSound;

}

