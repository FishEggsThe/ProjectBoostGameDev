using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    Vector3 movementVector = new Vector3(0, 4, 0);
    float movementFactor;
    float period = 2f; // Don't set this to zero dumbass

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        if(period == 0) { return;}
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        
        movementFactor = (rawSinWave + 1f)/2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
