using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Using for car movement AI or Player
/// </summary>
///
[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{
    /// <summary>
    /// Player type is AI or User
    /// </summary>
    public enum PlayerType
    {
        AI,
        User
    }
    public PlayerType playerType = PlayerType.AI;

    [SerializeField] GameObject TargetPlayer;// Target player for AI

    Rigidbody rb;
    [SerializeField]
    [Range(0,10)]
    float MovementSpeed = 10;

    [SerializeField] Joystick joystick;//Joystick to rotation user 

    RotateAround rotateAround;//rotate to fireball
    public bool AmIDead = false;//Am i dead if i am dead not to move
    
    void Start()
    {
        GameManager.instance.AddToPlayerToPlayers(this.gameObject);//Add me to player list
        rb = this.GetComponent<Rigidbody>();

        rotateAround = this.GetComponentInChildren<RotateAround>();
        if (playerType == PlayerType.AI)
            rotateAround.isAI = true;
    }


    void Update()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded || AmIDead)
            return;

        if(playerType == PlayerType.AI)
        {
            if(TargetPlayer == null && GameManager.instance.PlayersCount() > 1)//If not have a target player and player list count bigger than 1 
            {
                TargetPlayer = GameManager.instance.ReturnRandomPlayer(this.gameObject);//Get Random player
                rotateAround.isRotatingAI = false;
            }

            else if(TargetPlayer != null && !TargetPlayer.GetComponent<CarMovement>().AmIDead)//If the targer player is not null and the target player is not dead go target player
            {
                this.transform.LookAt(TargetPlayer.transform.position);
                if (Vector3.Distance(TargetPlayer.transform.position, this.transform.position) < 4)//If the distance is lower than 4 rotate the fire ball
                {
                    rotateAround.isRotatingAI = true;
                }
                else
                {
                    rotateAround.isRotatingAI = false;
                }
            }
            else
            {
                rotateAround.isRotatingAI = false;
                TargetPlayer = GameManager.instance.ReturnRandomPlayer(this.gameObject);
            }
        }

        else if(playerType == PlayerType.User)
        {
            //Debug.Log(new Vector3(0, (180.0f / Mathf.PI) * Mathf.Atan(joystick.Vertical / joystick.Horizontal), 0));
           this.transform.localRotation = Quaternion.Euler(new Vector3(0, (180.0f / Mathf.PI) * Mathf.Atan2(joystick.Horizontal,  joystick.Vertical), 0));//Rotate the player car using joystick
        }
    }

    private void FixedUpdate()
    {

        if (!GameManager.isGameStarted || GameManager.isGameEnded || AmIDead)
            return;
        rb.MovePosition(this.transform.position + this.transform.forward * MovementSpeed * Time.fixedDeltaTime);//Move car forward
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Car") && collision.gameObject != this.gameObject)//If hit the other car add force car from it backwards
        {
            Rigidbody detected = collision.transform.GetComponent<Rigidbody>();
            detected.AddForce(detected.transform.position + detected.transform.forward * -1  * 100);
        }

        if (collision.transform.tag.Equals("Sea"))//If i triggered to the sea i am dead player
        {
            GameManager.instance.RemovePlayerFromList(this.gameObject);
            AmIDead = true;
            this.gameObject.SetActive(false);

            if(playerType == PlayerType.User)
            {
                GameManager.instance.OnGameEnded();
            }
        }
    }
}
