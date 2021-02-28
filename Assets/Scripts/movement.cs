using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class movement : MonoBehaviour
{
    [SerializeField] GameObject freeLookCamera;
    CinemachineFreeLook freeLookComponent;

    public Rigidbody rb;
    [Range(0f, 2f)]
    public float acceleration = 1f;
    [Range(0f, 1f)]
    public float horizontalDamping = 0.2f;
    [Range(0f, 1f)]
    public float dampenStop = 0.2f;
    [Range(0f, 1f)]
    public float dampenTurn = 0.2f;
    bool movingS;
    bool movingF;
    private void Start()
    {
        movingS = false;
        movingF = false;
        freeLookComponent = freeLookCamera.GetComponent<CinemachineFreeLook>();
        freeLookComponent.m_XAxis.m_MaxSpeed = 0;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            freeLookComponent.m_XAxis.m_MaxSpeed = 200;
        }
        if (Input.GetMouseButtonUp(1))
        {
            freeLookComponent.m_XAxis.m_MaxSpeed = 0;
        }


    }
    private void FixedUpdate()
    {
        Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
        float fHorizV = locVel.x;
        float fVertV = locVel.z;

        //Forward/Backwards movement
        fVertV += Input.GetAxisRaw("Vertical") * acceleration;
        if (fVertV < 0)
        {
            fVertV /= 1.05f;
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.01f)
        {
            movingF = false;
            fVertV *= Mathf.Pow(1f - dampenStop, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(Input.GetAxisRaw("Vertical")) != Mathf.Sign(fVertV))
        {
            movingF = true;
            fVertV *= Mathf.Pow(1f - dampenTurn, Time.deltaTime * 10f);
        }
        else
        {
            movingF = true;
            fVertV *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 10f);
        }

        //Sideways Movement
        if (Mathf.Abs(fVertV + locVel.z) < .01f)
        {
            fHorizV += Input.GetAxisRaw("Horizontal") * acceleration / 1.3f;
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            {
                movingS = false;
                fHorizV *= Mathf.Pow(1f - dampenStop, Time.deltaTime * 10f);
            }
            else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizV))
            {
                movingS = true;
                fHorizV *= Mathf.Pow(1f - dampenTurn, Time.deltaTime * 10f);
            }
            else
            {
                movingS = true;
                fHorizV *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 10f);
            }
        }
        //Rotating the player
        if (movingS || movingF)
        {
            Vector3 cameraLook = transform.position - freeLookCamera.transform.position;
            cameraLook.y = 0;
            Quaternion rotation = Quaternion.LookRotation(cameraLook, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .1f);
        }
        locVel = new Vector3(fHorizV, locVel.y, fVertV);
        rb.velocity = transform.TransformDirection(locVel);


    }
}
