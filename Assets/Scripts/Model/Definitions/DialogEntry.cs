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
    public DialogData Data => _data;
}

