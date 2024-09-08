using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/Dialog", fileName = "Dialog")]
public class DialogEntry : ScriptableObject
{
    [SerializeField] private DialogData _data;
    [SerializeField] private DialogData _engData;
    [SerializeField] private DialogData _chiData;

    public DialogData GetLocalizedData(string language)
    {
        if (language == "English" && _engData != null && _engData.Sentences != null && _engData.Sentences.Length > 0)
        {
            return EngData;
        }
        if (language == "Chinese" && _chiData != null && _chiData.Sentences != null && _chiData.Sentences.Length > 0)
        {
            return ChiData;
        }



        return Data;
    }

    [SerializeField] private place _place;
    [SerializeField] private Color _speakerColor;
    [SerializeField] private string _speakerSound;

    public place Place => _place;
    public Color SpeakerColor => _speakerColor;
    public string SpeakerSound => _speakerSound;


    public DialogData Data => _data;
    public DialogData EngData => _engData;
    public DialogData ChiData => _chiData;

}

