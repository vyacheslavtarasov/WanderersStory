using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Hero : Creature
{
    private PlayerPerkController _playerPerkController;
    [SerializeField] private Sensor _interactSensor;

    [Space]
    [Header("Particles")]
    [SerializeField] private SpawnPrefab RunDustParticleSpawner;
    [SerializeField] private SpawnPrefab JumpDustParticleSpawner;
    [SerializeField] private SpawnPrefab SlapTheGroundParticleSpawner;
    [SerializeField] private ParticleSystem _particleSystem;

   

    

    private GameSession _session;

    public string WallSide = "";

    /*[ContextMenu("serialize")]
    public void Serialize()
    {
        Debug.Log(_session.Data);
        GameSettings.I.Session.Value = _session.Data;
        Debug.Log($"Session: {PlayerPrefs.GetString("session", "default")}");
        Debug.Log(GameSettings.I.Session.Value);
    }*/
    

    [SerializeField] protected Sensor _stickyWallCheckerR;
    [SerializeField] public CircleCollider2D wallClingCollider;

    CinemachineCameraShaker shaker;

    public void OnPerksUpdate(List<PlayerPerk> perks)
    {
        _playerPerkController = FindObjectOfType<PlayerPerkController>();
        var doubleJump = _playerPerkController.GetItem("doubleJump");
        if (!doubleJump.IsVoid && doubleJump.Active)
        {
            _jumpsAmount = 2;
        }
        else
        {
            _jumpsAmount = 1;
        }

        var wallCling = _playerPerkController.GetItem("wallCling");
        if (!wallCling.IsVoid && wallCling.Active)
        {
            wallClingCollider.enabled = true;
        }
        else
        {
            wallClingCollider.enabled = false;
        }

    }

    
    
    private void Start()
    {
        shaker = FindObjectOfType<CinemachineCameraShaker>();

        GetComponent<Health>().OnDamage.AddListener(shaker.StartShake);

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        _session = FindObjectOfType<GameSession>();
        _healthComponent.overallHealth = _session.Data.Health;
        _inventory.SetInventory(_session.Data.Inventory);
        _jumpsAmount = _session.Data.JumpsAmount;
        OnPerksUpdate(null);

        _inventory.OnChanged += OnChangeInventory;


        _healthComponent.currentHealth = _session.Data.Health;
        _healthComponent.ChangeHealth(0.0f);

        AssignHeroToFollowCamera[] cameras = FindObjectsOfType<AssignHeroToFollowCamera>();
        foreach (AssignHeroToFollowCamera cam in cameras)
        {
            cam.AssignHero();
        }


        InputActionAsset InputActionAsset = Resources.Load<InputActionAsset>("HeroInputActions");

        // Debug.Log("start of the hero with ");
        // Debug.Log(_session.LoadLevelWithOpening);

        /*sif (_session.LoadLevelWithOpening)
        {

        }
        else
        {
            foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
            {
                if (localActionMap.name == "ArcadeLevelDefault")
                {
                    Debug.Log("enabling arcade hero controller3");
                    localActionMap.Enable();
                }
                else
                {
                    localActionMap.Disable();
                }
            }
        }*/
        

    }

    public void JumpFromShelf()
    {

        foreach(GameObject obj in GroundChecker.GetIntersectedObjects().ToArray())
        {
            if (obj.CompareTag("Shelf"))
            {
                
                obj.GetComponent<EdgeCollider2D>().enabled = false;
                StartCoroutine(SwitcColliderhBack(obj, 1.4f));
            }
        }
    }

    private IEnumerator SwitcColliderhBack(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.GetComponent<EdgeCollider2D>().enabled = true;
    }

    public void Interact()
    {
        if (_interactSensor != null)
        {
            // Debug.Log(_interactSensor.GetIntersectedObjects());
            foreach (GameObject obj in _interactSensor.GetIntersectedObjects())
            {
                Interactive interactiveComponent = obj.GetComponent<Interactive>();
                    interactiveComponent.Interact(this.gameObject);
                
            }
        }
    }

    public void ChangeQuickMenuFocus(bool direction)
    {
        if (direction)
        {
            _session.Data.QuickInventoryIndex = Mathf.Clamp(_session.Data.QuickInventoryIndex += 1, 0, 2);
        }
        else
        {
            _session.Data.QuickInventoryIndex = Mathf.Clamp(_session.Data.QuickInventoryIndex -= 1, 0, 2);
        }



        _inventory.SetDirty();
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
        GetComponent<Health>().OnDamage.RemoveListener(shaker.StartShake);

    }

    public void OnChangeInventory(List<InventoryItemData> _inventory)
    {
        // _session.Data.Inventory = new List<InventoryItemData>(_inventory);
        _session.Data.UpdateInventory(_inventory);
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

        if (_wallStick)
        {
            
            _rigidbody.AddForce(new Vector2(_direction.x, 0) * 480.0f, ForceMode2D.Force);
            _currentJumpsCount = _jumpsAmount - 1;
            _jumpAvailable = false;
            _wallStick = false;
            _animator.SetBool("WallHang", false);

        }
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


    public override void OnGroundTouch(GameObject myGameObject)
    {
        if (_fallTime > 0.4f && _slapAvailable)
        {
            SlapTheGroundParticleSpawner.Spawn();
            _soundPlayer4OneShots.Play("Landing");
        }
        _slapAvailable = false;
        base.OnGroundTouch(myGameObject);

    }

    private void MoveWithAPlatform(Vector2 delta)
    {

        _rigidbody.position += delta;
        // _rigidbody.MovePosition(_rigidbody.position + delta);

    }


    

    protected override void FixedUpdate()
    {

        /*if (platformController)
        {

            MoveWithAPlatform(platformController.Delta);

        }*/



        base.FixedUpdate();

        

        LaunchParticles();


        if (_stickyWallCheckerR.GetCollisionStatus() && _direction.x == 1.0f && !_isGrounded && !_wallStick && (WallSide == "Right" || WallSide == ""))
        {
            // Debug.Log("stick left");
            _wallStick = true;
            WallSide = "Right";
            _animator.SetBool("WallHang", true);
            _currentJumpsCount = _jumpsAmount;
            _soundPlayer4OneShots.Play("Cling");
        }

        if (_stickyWallCheckerR.GetCollisionStatus() && _direction.x == -1.0f && !_isGrounded && !_wallStick && (WallSide == "Left" || WallSide == ""))
        {
            // Debug.Log("stick right");
            _wallStick = true;
            WallSide = "Left";
            _animator.SetBool("WallHang", true);
            _currentJumpsCount = _jumpsAmount;
            _soundPlayer4OneShots.Play("Cling");
        }

        if (!_stickyWallCheckerR.GetCollisionStatus())
        {
            WallSide = "";
        }

        if (_wallStick)
        {
            _rigidbody.gravityScale = 0.0f;
            _rigidbody.velocity = Vector2.zero;
        }

    }
}
