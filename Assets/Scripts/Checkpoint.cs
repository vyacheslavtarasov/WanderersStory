using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.InputSystem;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private SpawnPrefab _heroSpawner;
    [SerializeField] public UnityEvent Checked;
    [SerializeField] public UnityEvent UnChecked;

    public string Name => _name;
    private GameSession _session;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _animator = GetComponent<Animator>();
        if (_session.IsChecked(_name))
        {
            Checked?.Invoke();
        }

        _session.OnChanged += OnCurrentCheckpointChange;

        /*var devices = InputSystem.devices;

        // Log the names of the devices
        foreach (var device in devices)
        {
            Debug.Log("Device: " + device.name + " (" + device.deviceId + ")");
        }*/



    }

    private void OnCurrentCheckpointChange(string newCheckpointName)
    {
        if(_session.IsChecked(_name))
        {
            Checked?.Invoke();
            _animator.SetBool("act", true);
        }
        else
        {
            UnChecked?.Invoke();
            _animator.SetBool("act", false);
        }
    }

    public void SetCurrentCheckpoint()
    {
        StatefulObject[] objList = FindObjectsOfType<StatefulObject>(true);

        List<ObjectState> levelObjectsStates = new List<ObjectState>();

        foreach(StatefulObject obj in objList)
        {
            levelObjectsStates.Add(new ObjectState(obj.gameObject.name, obj.DefaultState, obj.GetCurrentState()));
        }

        List<ObjectState> deadObjects = _session.Data.LevelObjectsState.Where(p => !levelObjectsStates.Any(p2 => p2.ObjectName == p.ObjectName)).ToList();

        List<ObjectState> addition = new List<ObjectState>();

        // Debug.Log(JsonConvert.SerializeObject(levelObjectsStates));

        foreach (ObjectState state in deadObjects)
        {
            addition.Add(new ObjectState(state.ObjectName, state.DefaultState, "dead"));
        }

        _session.Data.LevelObjectsState = new List<ObjectState>(levelObjectsStates);
        _session.Data.LevelObjectsState.AddRange(addition);

        _session = FindObjectOfType<GameSession>();
        _session.SetCurrentCheckpoint(Name);
        // Debug.Log(_session.Data);
        _session.Data.LevelName = SceneManager.GetActiveScene().name;
        GameSettings.I.Session.Value = _session.Data.ShallowCopy();
        // Debug.Log($"Session: {PlayerPrefs.GetString("session")}");

       

    }

    public void SpawnHero()
    {
        _heroSpawner.Spawn();
    }

    private void OnDestroy()
    {
        _session.OnChanged -= OnCurrentCheckpointChange;
    }

    
}
