using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float mainThrust = 700f;
    float turnSpeed = 200f;
    
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem thrustPS;
    [SerializeField] ParticleSystem rightThrustPS;
    [SerializeField] ParticleSystem leftThrustPS;

    //bool isAlive = true;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else {
            StopThrust();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
           RotateRight();
        }
        else{
            StopRotating();
        }

        
    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if(!audioSource.isPlaying) {
            thrustPS.Play();
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void StopThrust()
    {
        audioSource.Stop();
        thrustPS.Stop();
    }

    /*shut the fuck up
    void ProcessRotation()
    {
        int direction = (Input.GetKey(KeyCode.A)?1:0) - (Input.GetKey(KeyCode.D)?1:0);
        if (!rightThrustPS.isPlaying && direction < 0)
            rightThrustPS.Play();
        else if (!leftThrustPS.isPlaying && direction > 0)
            leftThrustPS.Play();
        else{
            rightThrustPS.Stop();
            leftThrustPS.Stop();
        }

        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * turnSpeed * direction * Time.deltaTime);
        rb.freezeRotation = false;
    }*/

    void RotateLeft()
    {
        ApplyRotation(turnSpeed);
        if (!rightThrustPS.isPlaying)
            rightThrustPS.Play();
    }

    void RotateRight()
    {
        ApplyRotation(-turnSpeed);
        if (!leftThrustPS.isPlaying)
            leftThrustPS.Play();
    }

    void StopRotating()
    {
        rightThrustPS.Stop();
        leftThrustPS.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }
}
