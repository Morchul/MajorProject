using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    [SerializeField]
    private CharacterBaseAI ai;

    [SerializeField]
    private int sensorType;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sensor Trigger Enter");
        ai.Sensor.Trigger(sensorType);
    }

    private void OnTriggerExit(Collider other)
    {
        ai.Sensor.Trigger(sensorType);
    }
}
