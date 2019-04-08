///<summary>
///Atchyutha Choday
///April 4,2019
///</summary>
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

//Created a Namespace called "SurvivalShooter" 
namespace SurvivalShooter
{ 

/// <summary>
/// This method shows about the player movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    //Controls the speed of the player
    public float speed = 6f;

    //Store the movement of the player
    Vector3 movement;
    //Reference to the animator component
    Animator anim;
    //Reference to rigidbody component
    Rigidbody playerRigidbody;
    //Layer mask to tell ray cast that we only want to hit the floor created
    int floorMask;
    //length of the ray cast from camera
    float camRayLength = 100f;

    //Setting up references which is similar to start function
    void Awake()
    {
        //Create a layer mask to get mask from floor
        floorMask = LayerMask.GetMask("Floor");
        //Get reference to animator
        anim = GetComponent<Animator>();
        //Get reference to rigidbody
        playerRigidbody = GetComponent<Rigidbody>(); 
    }
    
    //Function to Move physics characters
    void FixedUpdate()
    {
        //Get input from horizontal axis
        float h = Input.GetAxisRaw("Horizontal");
        //Get input from vertical axis
        float v = Input.GetAxisRaw("Vertical"); 

        Move(h, v); //Move our player
        Turning(); //Direction of our player
        Animating(h, v); //Animation of our player
    }
    
    //Function to move our player
    void Move(float h, float v)
    {
        //Set value of movement vector
        movement.Set(h, 0f, v);
        //normalizing the movement vector and set relative speed
        movement = movement.normalized * speed * Time.deltaTime;
        //moves rigidbody
        playerRigidbody.MovePosition(transform.position + movement); 
    }

    //Function to specify direction of player
    void Turning() 
    {
        //Create ray and cast it from mouse position
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Get information back from ray cast
        RaycastHit floorHit;

        //perform casting the ray action and if it hits, 
        //perform the action in curly braces
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) 
        
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            //Making sure vector is along the floor plane
            playerToMouse.y = 0f;

            //Creating a rotation that is in forward axis
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            //Player rotation is to new rotation not transformed
            playerRigidbody.MoveRotation(newRotation); 
        }
    }

    //Function to specify animation of player
    void Animating(float h, float v)
        {
            //Only true if there is any input from axis
            bool walking = h!=0f || v!= 0f;
            //Signal to animator that player is walking or not
            anim.SetBool("IsWalking", walking); 
        }

    }
  }
