using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroyed : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionSoundClip;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the Asteroid is NULL.");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _audioSource.Play();
    }
}
