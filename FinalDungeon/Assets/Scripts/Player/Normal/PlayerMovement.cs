using UnityEngine;
using System.Collections;
/// <summary>
/// Coded by Johann Yu
/// </summary>
public class PlayerMovement : MonoBehaviour {
	
	private CharacterController player;                     // Variable to store the player's character controller

	private GameObject 			followObj;                  // Variable to store the object to follow
	private GameObject 			playerObj;                  // Variable to store the player object

    private PlayerSwitch 		pSwitch;                    // Variable to reference the player switch script
	private Vector3 			movement	= Vector3.zero; // Variable for the movement of the player
    
    /*
	public 	float 				jumpHeight	= 5.0f;         // Variable for the jump height
    */
	public 	float 				fallSpeed 	= 10.0f;         // Variable for the gravity
	public	float				speed		= 6.0f;
	private bool 				isActive	= false;        // Variable to see if it needs to have the movement function
	public bool					pressing	= false;        // Variable to see if the player is pressing a button

	void Start () {
		player 		= GetComponent<CharacterController>(); // Find the player's character controller

		followObj	= GameObject.Find("FollowingSpawn"); // Find the object to follow
		pSwitch 	= GameObject.Find ("PlayerPlace").GetComponent<PlayerSwitch>(); // Find the PlayerSwitch game object to find the PlayerSwitch.cs script
	}

	// Update is called once per frame
	void Update () {
		playerObj	= GameObject.FindGameObjectWithTag("Player"); // Consistantly find the object that is tagged player
		if (gameObject.tag == "Player") // If the game object is tagged Player
        {
			isActive = true; // Set the isActive boolean to true
		}
		if (gameObject.tag == "Player2") // If the game object is tagged Player2
        {
			isActive = false; // Set the isActive boolean to false
		}
		if (isActive) // If the game objects isActive boolean is true
        {	
			if (pSwitch.isMoving == false) // If the player isn't in the middle of switching characters
			{
				Move (); // Call the move function
				InputCheck (); // Do the input check function
			}
		} 
		else // If the game object's isActive boolean is false
		{
			FollowPlayer (); // Call the follow player function
		}
	}

	/// <summary>
	/// Character Movement.
	/// </summary>
	void Move()
	{
        if (player.isGrounded) // If the player is on the ground
        {
            if (Input.GetKey(KeyCode.W)) // If pressing W
            {
                pressing = true; // The button is pressed
                transform.localEulerAngles = new Vector3(0, 0, 0); // Rotate the player facing forward
                movement = new Vector3(0, 0, 1); // Move forward
                movement *= speed; // Multiply movement position by the speed to make it move at a steady rate
            }
            if (Input.GetKey(KeyCode.A)) // If pressing A
            {
                pressing = true; // The button is pressed
                transform.localEulerAngles = new Vector3(0, -90, 0); // Rotate the player facing left
                movement = new Vector3(-1, 0, 0); // Move left
                movement *= speed; // Multiply movement position by the speed to make it move at a steady rate
            }
            if (Input.GetKey(KeyCode.S)) // If pressing S
            {
                pressing = true; // The button is pressed
                transform.localEulerAngles = new Vector3(0, 180, 0); // Rotate the player facing backward
                movement = new Vector3(0, 0, -1); // Move back
                movement *= speed; // Multiply movement position by the speed to make it move at a steady rate
            }
            if (Input.GetKey(KeyCode.D)) // If pressing D
            {
                pressing = true; // The button is pressed
                transform.localEulerAngles = new Vector3(0, 90, 0); // Rotate the player facing right
                movement = new Vector3(1, 0, 0); // Move right
                movement *= speed; // Multiply movement position by the speed to make it move at a steady rate
            }
            /*if (Input.GetKeyDown (KeyCode.Space)) // If pressing Space
            {
				pressing = true; // The button is pressed
				movement.y = jumpHeight; // Move character's y position to the jump height value
			}
        }
        else // If the character isn't on the ground
		{
			movement = new Vector3 (Input.GetAxis("Horizontal"), movement.y, Input.GetAxis("Vertical")); // Allow it  to move while in mid air
			movement.x *= Speed / 2; // Don't move as fast in the air
			movement.z *= Speed / 2; // Don't move as fast in the air
		}
        */
        }
		movement.y -= fallSpeed; // Apply gravity
        player.Move(movement * Time.deltaTime); // Accesses the character controller to make the player actually move
	}

	/// <summary>
	/// Does something after the player lets go of a button
	/// </summary>
	void InputCheck()
	{
		if (Input.GetKeyUp (KeyCode.W)) // If player lets go of W
		{	
			pressing = false; // The player is no longer pressing anything
			movement = Vector3.zero; // Stop the player from moving
		} 
		else if (Input.GetKeyUp (KeyCode.A)) // If player lets go of W
        {	
			pressing = false; // The player is no longer pressing anything
            movement = Vector3.zero; // Stop the player from moving
        } 
		else if (Input.GetKeyUp (KeyCode.S)) // If player lets go of W
        {	
			pressing = false; // The player is no longer pressing anything
            movement = Vector3.zero; // Stop the player from moving
        } 
		else if (Input.GetKeyUp (KeyCode.D)) // If player lets go of W
        {	
			pressing = false; // The player is no longer pressing anything
            movement = Vector3.zero; // Stop the player from moving
        } 
		/*else if (Input.GetKeyDown (KeyCode.Space)) 
		{
			pressing = false;
		}*/
	}

	/// <summary>
	/// Follows the player.
	/// </summary>
	void FollowPlayer()
	{
		transform.position = Vector3.MoveTowards (transform.position, followObj.transform.position, speed * Time.deltaTime); // Continiously moves towards the empty game object to follow
		transform.rotation = Quaternion.RotateTowards (transform.rotation, playerObj.transform.rotation, 10); // Set the rotation the same as the player's
	}

}
