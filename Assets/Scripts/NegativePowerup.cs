using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativePowerup : MonoBehaviour
{
    public Transform player;
    private float _speed = 4f;
    private float _maxDist = 10f;
    private float _minDist = 0;
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.LookAt(player);

        if (Vector3.Distance(transform.position, player.position) >= _minDist)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
        else if (Vector3.Distance(transform.position, player.position) <= _maxDist)
        {
            
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
