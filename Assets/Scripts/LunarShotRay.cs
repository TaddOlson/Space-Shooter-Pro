using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarShotRay : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6.0f;

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

}
