using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativePowerup : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    private Player _playerObject;
    private float _speed = 4f;
    private float _maxDist = 10f;
    private float _minDist = 0;
    private AudioClip _clip;

    void Start()
    {
        _playerObject = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        Vector3 lookVector = _playerTransform.transform.position - transform.position;
        lookVector.y = transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        transform.LookAt(_playerTransform);

        if (Vector3.Distance(transform.position, _playerTransform.position) >= _minDist)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
        else if (Vector3.Distance(transform.position, _playerTransform.position) <= _maxDist)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if(player != null)
            {
                player.PlayerSlowdown();
            }
            
            Destroy(this.gameObject);
        }
    }
}
