using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotating the fireball
/// </summary>
public class RotateAround : MonoBehaviour
{
    [SerializeField]
    [Range(0,10000)]
    float RotateSpeed = 10; //Rotating Speed of center points fire ball

    public bool isAI = false;
    public bool isRotatingAI = false;
    

    void Start()
    {
        RotateSpeed = Random.Range(200, 400); //sets the rotateSpeed randomly
    }
    float currentSpeed = 0; //Current Rotate speed

    void FixedUpdate()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded)
            return;
        if (!isAI)
        {
            if (Input.GetMouseButton(0))
            {
                this.transform.Rotate(Vector3.up, 1 * currentSpeed * Time.deltaTime);

                currentSpeed = Mathf.Lerp(currentSpeed, RotateSpeed, Time.deltaTime);//Set current speed to rotate speed
            }

            if (Input.GetMouseButtonUp(0))
            {
                currentSpeed = 0;

            }
        }
        else
        {

            if (isRotatingAI)
            {
                this.transform.Rotate(Vector3.up, 1 * currentSpeed * Time.deltaTime);

                currentSpeed = Mathf.Lerp(currentSpeed, RotateSpeed, Time.deltaTime);//Set current speed to rotate speed
            }
            else
            {
                currentSpeed = 0;
            }
        }
    }
}
