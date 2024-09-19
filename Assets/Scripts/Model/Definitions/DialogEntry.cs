using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/Dialog", fileName = "Dialog")]
public class DialogEntry : ScriptableObject
{
    [SerializeField] private DialogData _data;
    [SerializeField] private DialogData _engData;
    [SerializeField] private DialogData _chiData;

    public DialogData GetLocalizedData(string language)
    {
        Debug.Log("get localized data called");
        _data.Place = Place;
        _engData.Place = Place;
        _chiData.Place = Place;



        if (_speakerColor != Color.clear)
        {
            Debug.Log("changing color");
            Debug.Log(SpeakerColor);
            _data.SpeakerColor = SpeakerColor;
            _engData.SpeakerColor = SpeakerColor;
            _chiData.SpeakerColor = SpeakerColor;
        }

        if (_speakerSound != null && _speakerSound != "")
        {
            _data.SpeakerSound = SpeakerSound;
            _engData.SpeakerSound = SpeakerSound;
            _chiData.SpeakerSound = SpeakerSound;
        }

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

    public void PrintMessage()
    {
        if (_data.SpeakerName == "Герой")
        {
            _place = place.left;

            string hexColor = "#CCE4FF"; // Hex color string (RRGGBB format)
            Color newColor;

            if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
            {
                Debug.Log("Parsed Color: " + newColor);
            }
            else
            {
                Debug.LogError("Failed to parse color string");
            }
            _speakerColor = newColor;

            _speakerSound = "Hero";

        }

        if (_data.SpeakerName == "Торговец")
        {
            _place = place.right;

            string hexColor = "#FFD8D5"; // Hex color string (RRGGBB format)
            Color newColor;

            if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
            {
                Debug.Log("Parsed Color: " + newColor);
            }
            else
            {
                Debug.LogError("Failed to parse color string");
            }
            _speakerColor = newColor;

            _speakerSound = "Trader";

        }

        // To ensure the data persists when exiting play mode in the Editor
        // UnityEditor.EditorUtility.SetDirty(this);
        // UnityEditor.AssetDatabase.SaveAssets();
    }

}

/*[CustomEditor(typeof(DialogEntry))]
public class MyScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector (for displaying normal fields)
        DrawDefaultInspector();

        // Reference to the target ScriptableObject
        DialogEntry myScriptableObject = (DialogEntry)target;

        // Create a button in the Inspector
        if (GUILayout.Button("Try Get Defaults"))
        {
            // Call the function in the ScriptableObject when the button is clicked
            myScriptableObject.PrintMessage();


            
        }
    }
}*/

