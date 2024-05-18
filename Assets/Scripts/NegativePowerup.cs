using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativePowerup : MonoBehaviour
{
    public Transform player;
    private float _speed = 5f;
    private float _maxDist = 10f;
    private float _minDist = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        if (Vector3.Distance(transform.position, player.position) >= _minDist)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, player.position) <= _maxDist)
        {

        }
        
    }
}
