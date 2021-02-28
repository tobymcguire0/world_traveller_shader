using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public OculusSphere sphere;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("World"))
        {
            sphere.addMaterial(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("World"))
        {
            sphere.removeMaterial(other.gameObject);
        }
    }
}
