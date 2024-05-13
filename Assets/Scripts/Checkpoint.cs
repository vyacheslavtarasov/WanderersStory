using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        _session.SetCurrentCheckpoint(Name);  
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
