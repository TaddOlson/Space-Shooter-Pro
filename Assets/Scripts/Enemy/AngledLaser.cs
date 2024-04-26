using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngledLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;
    private int _laserDirection;

    // Update is called once per frame
    void Update()
    {
        if (_laserDirection == 0)
        {
            RightDiagonalLaser();
        }
        else if (_laserDirection == 1)
        {
            LeftDiagonalLaser();
        }
    }

    public void LaserDirection (int direction)
    {
        _laserDirection = direction;
    }

    public void RightDiagonalLaser()
    {
        transform.Translate(new Vector3(-15.0f, -10.0f, 0).normalized * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    public void LeftDiagonalLaser()
    {
        transform.Translate(new Vector3(15.0f, -10.0f, 0).normalized * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    public bool IsEnemyLaser()
    {
        return _isEnemyLaser;
    }

}
