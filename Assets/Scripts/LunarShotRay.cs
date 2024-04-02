using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarShotRay : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6.0f;
    private CapsuleCollider2D _collider;
    private float m_ScaleX, m_ScaleY, m_ScaleZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.transform.GetComponent<Enemy>();

            Destroy(other.gameObject);
        }
    }
}
