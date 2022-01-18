using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Vector2 sensitivity;
    [SerializeField] private Vector2 acceleration;
    [SerializeField] private float inputLagPeriod;
    [SerializeField] private float maxVerticalAngleFromHorizon;


    private Vector2 velocity;
    private Vector2 rotation;
    private Vector2 lastInputEvent;
    private float inputLagTimer;


    private float ClampVerticalAngle(float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon); 
    }

    private Vector2 GetInput()
    {
        inputLagTimer += Time.deltaTime;
        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        if((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer >= inputLagPeriod)
        {
            lastInputEvent = input;
            inputLagTimer = 0;
        }
        return lastInputEvent;
    }

    private void Update()
    {
        Vector2 wantedVelocity = GetInput() * sensitivity; 
        velocity = new Vector2(Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.deltaTime), Mathf.MoveTowards(velocity.y, wantedVelocity.y, acceleration.y * Time.deltaTime));
        rotation += velocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);
        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0); 
    }


    
}
