using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlling is the car hitted this fireball
/// </summary>
public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject Parent;

    [SerializeField]
    float hitSpeed = 10;

    private void Update()
    {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, Vector3.forward * 3, Time.deltaTime * 20);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag.Equals("Car") && collision.gameObject != Parent)// add force to player car
        {
            Rigidbody detected = collision.transform.GetComponent<Rigidbody>();
            detected.AddForce(detected.transform.position + detected.transform.forward * -10 + Vector3.up * hitSpeed);
        }
    }
}
