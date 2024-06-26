﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiagonal : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    private float _fireRate = 2.0f;
    private float _canFire = 1.0f;

    private int _enemyDirection;

    // Start is called before the first frame update
    void Start()
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
            Debug.LogError("Audio Source is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyDirection == 0)
        {
            EnemyDiagonalMovementRight();
        }
        else if (_enemyDirection == 1)
        {
            EnemyDiagonalMovementLeft();
        }

        EnemyFire();
    }

    public void EnemyDirection(int direction)
    {
        _enemyDirection = direction;
    }

    public void EnemyDiagonalMovementRight()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 60);

        if (transform.position.y < -6.0f && transform.position.x > 8.0f)
        {
            transform.position = new Vector3(-11.0f, 7.7f, 0);
        }
    }

    public void EnemyDiagonalMovementLeft()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, -60);

        if (transform.position.y < -6.0f && transform.position.x < -8.0f)
        {
            transform.position = new Vector3(11.0f, 7.7f, 0);
        }
    }

    public void EnemyFire()
    {
        
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 5f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, new Vector3(.7f, -1f, 0) + transform.position, Quaternion.identity);
            AngledLaser[] laser = enemyLaser.GetComponentsInChildren<AngledLaser>();

            for (int i = 0; i < laser.Length; i++)
            {
                laser[i].AssignEnemyLaser();
                laser[i].LaserDirection(_enemyDirection);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            EnemyDeath();

        }

        if (other.tag == "Laser")
        {
            Laser laser = other.transform.GetComponent<Laser>();
            AngledLaser angledLaser = other.transform.GetComponent<AngledLaser>();

            if (laser != null && laser.IsEnemyLaser() == true)
            {
                return;
            }

            if (angledLaser != null && angledLaser.IsEnemyLaser() == true)
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
        Destroy(this.gameObject, 1.0f);
    }
}
