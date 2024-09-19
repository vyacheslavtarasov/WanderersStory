using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum place {left, right, none}

[Serializable]
public class DialogData
{
    [SerializeField] private string[] _sentences;
    [SerializeField] private string _speakerName;
    private place _place;
    private Color _speakerColor;
    private string _speakerSound;
    public string[] Sentences => _sentences;
    public string SpeakerName => _speakerName;

    // public place Place => _place;
    public place Place { get { return _place; } set { _place = value; } }

    // public Color SpeakerColor => _speakerColor;

    public Color SpeakerColor { get { return _speakerColor; } set { _speakerColor = value; } }
    // public string SpeakerSound => _speakerSound;

    public string SpeakerSound { get { return _speakerSound; } set { _speakerSound = value; } }

}

