using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarShotRay : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6.0f;
    private Animator _anim;

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
            Destroy(other.gameObject);
        }
    }

    //Prevent the animation from looping when fired.
    private void StopAnim()
    {

    }

    //Transition the animations from size to size at a certain distance.
    //on y axis 1.5 between each shot size
    private void CrossFadeInFixedTime(float fixedTransitionDuration = 4.0f)
    {

    }
}
