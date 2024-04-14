using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class InteractionEvent : UnityEvent<GameObject>
{

}


public class Sensor : MonoBehaviour
{

    [SerializeField] private List<GameObject> _gameObjectsList;
    private bool _isTouching;

    [Tooltip("Collider with Is Trigger flag set.")]
    private Collider2D _collider;

    [Tooltip("React only on these Layers.")]
    [SerializeField] private LayerMask _layers = ~0;

    [Tooltip("React only on this tag. No means all.")]
    [SerializeField] private string[] _tags;


    public InteractionEvent CollisionEnterEvent;
    public InteractionEvent CollisionExitEvent;
    public InteractionEvent CollisionExitAllEvent;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if (_collider == null)
        {
            Debug.LogError("Assign the gameobject Collider in the inspector before resuming.");
            // UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public bool GetCollisionStatus()
    {
        return _isTouching;
    }

    public List<GameObject> GetIntersectedObjects() {
        return _gameObjectsList;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((_layers & (1 << collider.gameObject.layer)) == 0)
        {
            return;
        }
        if (_tags.Length == 0 || _tags.Contains(collider.gameObject.tag))
        {

            _isTouching = true;
            CollisionEnterEvent?.Invoke(collider.gameObject);
            _gameObjectsList.Add(collider.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if ((_layers & (1 << collider.gameObject.layer)) == 0)
        {
            return;
        }
        if (_tags.Length == 0 || _tags.Contains(collider.gameObject.tag))
        {
            _gameObjectsList.Remove(collider.gameObject);

            CollisionExitEvent?.Invoke(collider.gameObject);
            if (_gameObjectsList.Count > 0)
            {
                
            }
            else
            {
                CollisionExitAllEvent?.Invoke(collider.gameObject);
                _isTouching = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if ((_layers & (1 << collider.gameObject.layer)) == 0)
        {
            return;
        }
        if (_tags.Length == 0 || _tags.Contains(collider.gameObject.tag))
        {

            _isTouching = true;
            CollisionEnterEvent?.Invoke(collider.gameObject);
            _gameObjectsList.Add(collider.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if ((_layers & (1 << collider.gameObject.layer)) == 0)
        {
            return;
        }
        if (_tags.Length == 0 || _tags.Contains(collider.gameObject.tag))
        {
            _gameObjectsList.Remove(collider.gameObject);

            CollisionExitEvent?.Invoke(collider.gameObject);
            if (_gameObjectsList.Count > 0)
            {
                
            }
            else
            {
                CollisionExitAllEvent?.Invoke(collider.gameObject);
                _isTouching = false;
            }
        }
    }

}
