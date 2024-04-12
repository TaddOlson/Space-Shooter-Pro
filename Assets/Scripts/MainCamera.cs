using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Animator _anim;
    private bool _isCameraShakeActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraShakeAnimation()
    {
        if (_isCameraShakeActive == false)
        {
            _anim.SetTrigger("OnPlayerDamage");
        }
        else if (_isCameraShakeActive == true)
        {
            return;
        }
        
    }
}
