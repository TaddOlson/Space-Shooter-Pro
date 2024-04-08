using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
    {
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _ammoCount = 30;

    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _thruster;
    [SerializeField]
    private GameObject _lunarShotPrefab;


    private bool _hasBeenHit = false;
    private float _damageDelay = 1.0f;
    private float _damageCooldown = 0f;

    private bool _isTripleShotActive = false;
    private bool _isShieldsActive = false;
    private bool _isLunarShotActive = false;
    [SerializeField]
    private int _shieldDurability = 3;
    private SpriteRenderer _shieldColor;

    [SerializeField]
    private GameObject _shieldVisualizer, _speedBoostVisualizer, _thrusterBoostVisualizer;


    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _lunarShotClip;
    private AudioSource _audioSource;

    [SerializeField]
    private float _fuelFillCooldown = 3.0f;
    [SerializeField]
    private float _fuelLevelMax = 60.0f;
    [SerializeField]
    private float _fuelChargeLevel;
    [SerializeField]
    private float _fuelDecrease = 20.0f;
    [SerializeField]
    private float _fuelIncrease = 0.5f;
    private bool _thrusterUsable = true;
    private bool _usingThrusters = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _shieldColor = _shieldVisualizer.GetComponent<SpriteRenderer>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        _rightEngine.gameObject.SetActive(false);
        _leftEngine.gameObject.SetActive(false);
        _thrusterBoostVisualizer.gameObject.SetActive(false);
        _thruster.gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        _hasBeenHit = false;

        if (_damageCooldown > 0)
        {
            _damageCooldown -= Time.deltaTime;
        }

        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            if (_ammoCount == 0)
            {
                return;
            }
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(movement * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5.9f), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

        ThrustersAcceleration();
    }

    public void ThrustersAcceleration()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _thrusterUsable)
        {
            _speed *= _speedMultiplier;
            _thruster.gameObject.SetActive(false);
            _thrusterBoostVisualizer.gameObject.SetActive(true);
            _usingThrusters = true;

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = 5.0f;
            _thrusterBoostVisualizer.gameObject.SetActive(false);
            _thruster.gameObject.SetActive(true);
            _usingThrusters = false;
        }

        if(_usingThrusters)
        {
            ThrustersActive();
        }
        else if (!_usingThrusters)
        {
            StartCoroutine(ThrusterReplenishRoutine());
        }
    }

    public void ThrustersActive()
    {
        if(_thrusterUsable == true)
        {
            _fuelChargeLevel -= Time.deltaTime * _fuelDecrease;
            _uiManager.UpdateFuel(_fuelChargeLevel);
            //_uiManager.UpdateFuelLevel

            if(_fuelLevelMax < 60)
            {
                _uiManager.UpdateFuel(_fuelChargeLevel);
                _usingThrusters = false;
                _thrusterUsable = false;
                _speed = 0;
            }
        }
    }

    IEnumerator ThrusterReplenishRoutine()
    {
        yield return new WaitForSeconds(_fuelFillCooldown);
        while (_fuelChargeLevel <= _fuelLevelMax && !_usingThrusters)
        {
            yield return null;
            _fuelChargeLevel += Time.deltaTime * _fuelIncrease;
            _uiManager.UpdateFuel(_fuelChargeLevel);
        }
        if(_fuelChargeLevel >= _fuelLevelMax)
        {
            _uiManager.UpdateFuel(_fuelChargeLevel);
            _thrusterUsable = true;
        }
    }

    public void FuelChargeLevel()
    {
        _fuelChargeLevel = Mathf.Clamp(_fuelChargeLevel, 13, _fuelLevelMax);

        if (_fuelChargeLevel <= 13.0f)
        {
            _thrusterUsable = false;
        }
        else if (_fuelChargeLevel >= (_fuelLevelMax /2.5f))
        {
            _thrusterUsable = true;
        }
    }

    void FireLaser()
    {
        AmmoCount(-1);
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true )
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            _audioSource.clip = _laserSoundClip;
        }
        else if (_isLunarShotActive == true)
        {
            Instantiate(_lunarShotPrefab, transform.position, Quaternion.identity);
            _audioSource.clip = _lunarShotClip;
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            _audioSource.clip = _laserSoundClip;
        }

        _audioSource.Play();

    }

    public void Damage()
        {
        if (_hasBeenHit == true)
        {
            return;
        }

        if (_damageCooldown > 0)
        {
            return;
        }

        _damageCooldown = _damageDelay;

        _hasBeenHit = true;

        if (_isShieldsActive == true)
        {
            _shieldDurability--;

            switch (_shieldDurability)
            {
                case 3:
                    _shieldColor.color = Color.green;
                    break;
                case 2:
                    _shieldColor.color = Color.blue;
                    break;
                case 1:
                    _shieldColor.color = Color.red;
                    break;
                case 0:
                    _shieldVisualizer.gameObject.SetActive(false);
                    _isShieldsActive = false;
                    break;
            }

            return;
        }

        _lives--;

        if (_lives == 2)
        {
            _rightEngine.gameObject.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.gameObject.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speed *= _speedMultiplier;
        _thruster.gameObject.SetActive(false);
        _speedBoostVisualizer.gameObject.SetActive(true);
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
        _thruster.gameObject.SetActive(true);
        _speedBoostVisualizer.gameObject.SetActive(false);
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
        _shieldColor.color = Color.green;
        _shieldDurability = 3;
    }

    public void LunarShotActive()
    {
        _isLunarShotActive = true;
        StartCoroutine(LunarShotPowerDownRoutine());
    }

    IEnumerator LunarShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isLunarShotActive = false;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void AmmoCount(int lasers)
    {
        _ammoCount += lasers;
        _uiManager.UpdateAmmo(_ammoCount);
    }
    
    public void AmmoCap()
    {
        if (_ammoCount <= 0)
        {
            _ammoCount = 0;
        }
        else if (_ammoCount > 15)
        {
            _ammoCount = 15;
        }

        _uiManager.UpdateAmmo(_ammoCount);
    }

    public void ReloadAmmo()
    {
        _ammoCount = _ammoCount + 15;
        AmmoCap();
        _uiManager.UpdateAmmo(_ammoCount);
    }

    public void HealthGain()
    {
        if (_lives == 3)
        {
            Debug.LogError("Lives full");
            return;
        }

        _lives++;
        _uiManager.UpdateLives(_lives);
    }



}  

    
