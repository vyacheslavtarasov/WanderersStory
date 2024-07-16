using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum place {left, right }

[Serializable]
public class DialogData
{
    [SerializeField] private string[] _sentences;
    [SerializeField] private string _speakerName;
    [SerializeField] private place _place;
    public string[] Sentences => _sentences;
    public string SpeakerName => _speakerName;
    public place Place => _place;

}

