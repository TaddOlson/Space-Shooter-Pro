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
    
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();


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
        EnemyDiagonalMovement();

        EnemyFire();
    }

    public void EnemyDiagonalMovement()
    {
        transform.Translate(new Vector3(5.0f, -3.0f, 0).normalized * _speed * Time.deltaTime);

        if (transform.position.y < -6.0f && transform.position.x > 8.0f)
        {
            transform.position = new Vector3(-11.0f, 7.7f, 0);
        }
        else if (transform.position.y < -6.0f && transform.position.x < -8.0f)
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
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            AngledLaser[] laser = enemyLaser.GetComponentsInChildren<AngledLaser>();

            for(int i = 0; i < laser.Length; i++)
            {
                laser[i].AssignEnemyLaser();
            }
        }
    }

    public void EnemyDeath()
    {

    }
}