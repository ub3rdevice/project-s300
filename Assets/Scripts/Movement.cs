using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    [SerializeField] float mainThrustSpeed = 1000f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem LeftThrusterParticles;
    [SerializeField] ParticleSystem RightThrusterParticles;
    
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrustSpeed * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

      void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartLeftRotation();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
        else
        {
            StopRotation();
        }
    }
    void StartLeftRotation()
    {
        ApplyRotation(rotationSpeed);

        if (!RightThrusterParticles.isPlaying)
        {
            RightThrusterParticles.Play();
        }
    }

    void StartRightRotation()
    {
        ApplyRotation(-rotationSpeed);

        if (!LeftThrusterParticles.isPlaying)
        {
            LeftThrusterParticles.Play();
        }
    }

     void StopRotation()
    {
        RightThrusterParticles.Stop();
        LeftThrusterParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {   
        rb.freezeRotation = true; // freezing rotatiomn so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation
    }
}
