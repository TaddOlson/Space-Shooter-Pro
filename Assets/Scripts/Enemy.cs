using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = 1f;

    //Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
   
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        
        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        EnemyFire();
    }

    public void EnemyFire()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] laser = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < laser.Length; i++)
            {
                laser[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            float randomX = Random.Range(-10f, 10f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.tag == "Player")
        {
           
            Player player = other.transform.GetComponent<Player>();
            
            if(player != null)
            {
                player.Damage();
            }

            EnemyDeath();
            
        }

        if (other.tag == "Laser")
        {
            Laser laser = other.transform.GetComponent<Laser>();

            if (laser != null && laser.IsEnemyLaser() == true)
            {
                return;
            }

            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }

            EnemyDeath();         
        }

        if (other.tag == "LunarShot")
        {

            if (_player != null)
            {
                _player.AddScore(20);
            }

            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        _anim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        _audioSource.Play();
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(this.gameObject, 2.8f);
    }
}
