using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] float xRange = 13f;
    [SerializeField] float yRange = 7f;
    [SerializeField] float zRange = 3f;
    [SerializeField] float boostSpeed = 10f;

    [SerializeField] GameObject[] lasers;

    [SerializeField] float zMove = 3f;
    [SerializeField] float decreaseTime = 0.5f;
    float newZPos = 0f;
    float clampedZPos = 0f;

    float xMove, yMove;
    
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 2.6f;
    [SerializeField] float controlYawFactor = 10f;
    [SerializeField] float controlRollFactor = -35f;





    void Start()
    { 

    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    public void ProcessTranslation()
    {
        xMove = Input.GetAxis("Horizontal");
        float newXPos = transform.localPosition.x + xMove * moveSpeed * Time.deltaTime;
        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);

        yMove = Input.GetAxis("Vertical");
        float newYPos = transform.localPosition.y + yMove * moveSpeed * Time.deltaTime;
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        /* float zMove = Input.GetAxis("Sprint");
        float newZPos = transform.localPosition.z + zMove * boostSpeed * Time.deltaTime;
        float clampedZPos = Mathf.Clamp(newZPos, -zRange, zRange); */

        if (Input.GetKey("left shift"))
        {
            newZPos = transform.localPosition.z + zMove * boostSpeed * Time.deltaTime;
            clampedZPos = Mathf.Clamp(newZPos, -zRange, zRange);
        }
        else
        {
            newZPos = Mathf.Lerp(newZPos, 0f, Time.deltaTime / decreaseTime);
            clampedZPos = Mathf.Clamp(newZPos, -zRange, zRange);
        }

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, clampedZPos);
    }


    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlMove = yMove * controlPitchFactor;
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControlMove = xMove * controlYawFactor;


        float pitch = pitchDueToPosition + pitchDueToControlMove;
        float yaw = yawDueToPosition + yawDueToControlMove;
        float roll = xMove * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);


    }


    void ProcessFiring()
    {
        if (Input.GetButton("Fire1")) {

            SetLasersActive(true);
        } else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }

    }


}
