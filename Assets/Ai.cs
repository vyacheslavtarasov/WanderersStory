using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    private Coroutine _currentCoroutine;

    public Sensor visionAreaComponent;
    public Sensor attackAreaComponent;

    private GameObject _target;
    public float AttackCooldown = 1.0f;

    public Transform[] waypoints;
    public int _targetWaypointIndex;

    private Creature _creature;

    public string state;


    private IEnumerator Patrol()
    {
        while (true)
        {
            state = "patrol";
            var direction = waypoints[_targetWaypointIndex].position - transform.position;
            if (direction.magnitude < 0.5f)
            {
                _targetWaypointIndex += 1;
                _targetWaypointIndex %= waypoints.Length;
                StartState(Stand());
            }
            else
            {
                direction.y = 0;
                direction.Normalize();
                _creature.SetDirection(direction);
            }
            yield return null;
        }
    }

    private IEnumerator Stand()
    {
        state = "stand";
        _creature.SetDirection(Vector3.zero);
        float timeToStand = Random.Range(0.0f, 3.0f);
        yield return new WaitForSeconds(timeToStand);
        StartState(Patrol());
    }

    private void StartState(IEnumerator newState)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(newState);
        }
        else
        {
            _currentCoroutine = StartCoroutine(newState);
        }
    }

    private IEnumerator Attack()
    {
        _creature.SetDirection(Vector3.zero);
        while (attackAreaComponent.GetCollisionStatus())
        {
            state = "attack";
            _creature.Attack();
            yield return new WaitForSeconds(AttackCooldown);
        }

        if (visionAreaComponent.GetCollisionStatus())
        {
            StartState(Pursuit());
        }
        else
        {
            StartState(Patrol());
        }
    }

    private IEnumerator Pursuit()
    {
        
        while (visionAreaComponent.GetCollisionStatus())
        {
            state = "pursuit";
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            _creature.SetDirection(direction);
            yield return null;
        }

        StartState(Patrol());

    }

    public void OnCharacterVisible(GameObject character)
    {
        _target = character;
        StartState(Pursuit());
    }

    public void OnCharacterVisibilityLost(GameObject character)
    {
        _target = null;
        _creature.SetDirection(Vector3.zero);
        StartState(Patrol());
    }

    public void OnAttackPossible(GameObject character)
    {
        _target = character;
        StartState(Attack());
    }

    private void Awake()
    {
        _creature = GetComponent<Creature>();
        _targetWaypointIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartState(Stand());
    }

}
