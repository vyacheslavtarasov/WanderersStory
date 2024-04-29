using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{

    [SerializeField] private Sensor _interactSensor;

    [Space][Header("Particles")]
    [SerializeField] private SpawnPrefab RunDustParticleSpawner;
    [SerializeField] private SpawnPrefab JumpDustParticleSpawner;
    [SerializeField] private SpawnPrefab SlapTheGroundParticleSpawner;
    [SerializeField] private ParticleSystem _particleSystem;

    private GameSession _session;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        _session = FindObjectOfType<GameSession>();
        _healthComponent.overallHealth = _session.Data.Health;
        _inventory.SetInventory(_session.Data.Inventory);
        _jumpsAmount = _session.Data.JumpsAmount;

        _inventory.OnChanged += OnChangeInventory;


        _healthComponent.currentHealth = _session.Data.Health;
        _healthComponent.ChangeHealth(0.0f);
    }

    public void Interact()
    {
        if (_interactSensor != null)
        {
            foreach(GameObject obj in _interactSensor.GetIntersectedObjects())
            {
                Interactive interactiveComponent = obj.GetComponent<Interactive>();
                interactiveComponent.Interact(this.gameObject);
            }
        }
    }

    public void CallPauseMenu()
    {
        var window = Resources.Load<GameObject>("PauseMenuWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(window, canvas.transform);
    }

    private void OnDestroy()
    {
        _inventory.OnChanged -= OnChangeInventory;
    }

    public void OnChangeInventory(List<InventoryItemData> _inventory)
    {
        _session.Data.Inventory = new List<InventoryItemData>(_inventory);
    }


    public override void OnChangeHealth(float wasHealth, float currentHealth, float overallHealth) 
    {
        base.OnChangeHealth(wasHealth, currentHealth, overallHealth);
        if (wasHealth > currentHealth)
        {
            _particleSystem.Play();
        }

        _session.Data.Health = currentHealth;
    }

    protected override void OnJump()
    {
        base.OnJump();
        JumpDustParticleSpawner.Spawn();
    }

    private void LaunchParticles()
    {
        if (_runParticleAvailable && Mathf.Abs(_rigidbody.velocity.x) > 0.8f && Mathf.Abs(_rigidbody.velocity.y) < 0.01f && _direction.x != 0 && _isGrounded)
        {
            RunDustParticleSpawner.Spawn();
            _runParticleAvailable = false;
        }

        if (Mathf.Abs(_rigidbody.velocity.x) < 0.5f && _isGrounded)
        {
            _runParticleAvailable = true;
        }
    }

 
    public void OnGroundTouch(GameObject myGameObject) {
        if (_fallTime > 0.4f)
        {
            SlapTheGroundParticleSpawner.Spawn();
        }
    }

    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        LaunchParticles();
       
    }
}
