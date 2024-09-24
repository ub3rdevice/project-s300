using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    float movementFactor; //Range toggles slider instead of textbox
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPos = transform.position;
        Debug.Log(startingPos);
    }

   
    void Update()
    {
        OscillatorMover();
    }

    void OscillatorMover()
    {
        if (period <= Mathf.Epsilon) { return; } //protection from NaN for period value, Mathf.Epsilon is the smallest float number unity can handle, do not compare floats (obvs)

        float cycles = Time.time / period; // this value grows over time

        const float tau = Mathf.PI * 2; // constant value of 6.283 (Pi * 2)
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculation for going from 0 to 1, but in a fancy & cleaner way

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }

}
