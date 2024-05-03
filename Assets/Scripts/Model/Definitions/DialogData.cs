using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class DialogData
{
    [SerializeField] private string[] _sentences;
    public string[] Sentences => _sentences;
}

