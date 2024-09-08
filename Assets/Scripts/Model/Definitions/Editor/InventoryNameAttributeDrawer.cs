using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(InventoryNameAttribute))]
public class InventoryNameAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var defs = DefsFacade.I.Items.ItemsForEditor;
        var names = new List<string>();

        foreach(var itemDef in defs)
        {
            Debug.Log(itemDef.Name);
            names.Add(itemDef.Name);
        }

        var index = names.IndexOf(property.stringValue);
        Debug.Log(property.stringValue);

       index = EditorGUI.Popup(position, property.displayName, index, names.ToArray());
       property.stringValue = names[index];
    }
}
