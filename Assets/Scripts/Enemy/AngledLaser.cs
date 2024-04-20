using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngledLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
}
