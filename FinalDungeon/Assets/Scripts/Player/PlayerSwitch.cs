using UnityEngine;
using System.Collections;
/// <summary>
/// Coded by Johann Yu
/// </summary>
public class PlayerSwitch : MonoBehaviour {
	public 	GameObject 			player;
	public 	GameObject 			player2;
	private GameObject 			playPlace;
	private GameObject 			followSpawn;

	private CharacterController controller;
	private CharacterController	controller2;

	public 	bool				isMoving = false;

	private PlayerMovement 		playerMov;
	// Use this for initialization
	void Start () {
		playPlace 	= GameObject.Find("PlayerPlace");
		followSpawn = GameObject.Find("FollowingSpawn");

		player 		= GameObject.FindGameObjectWithTag("Player");
		player2 	= GameObject.FindGameObjectWithTag("Player2");

		controller 	= player.GetComponent<CharacterController>();
		//controller2 = player2.GetComponent<CharacterController>();
		playerMov 	= player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q) && player2 != null && player.transform.rotation == player2.transform.rotation && playerMov.pressing == false) {
			ChangeCharacters ();
		}
		if (isMoving) {
			player2.transform.position = Vector3.MoveTowards (player2.transform.position, playPlace.transform.position, 6 * Time.deltaTime);
			player.transform.position = Vector3.MoveTowards (player.transform.position, followSpawn.transform.position, 6 * Time.deltaTime);
			if (player2.transform.position == playPlace.transform.position) {	
				player.tag = "Player2";
				followSpawn.transform.parent = player2.transform;
				playPlace.transform.parent = player2.transform;
				playPlace.transform.rotation = player2.transform.rotation;
				ReassignPlayers ();
				controller.enabled = true;
				controller2.enabled = false;
				if (player2.GetComponent<PlayerClass> ().Health <= 0 && player2.gameObject != player2.GetComponent<PlayerClass> ().mainPlayer) {
					PlayerClass pClass = player2.GetComponent<PlayerClass> ();
					pClass.Health = 100;
					pClass.Energy = 100;
					player2.SetActive (false);
					player2 = null;
				}
				isMoving = false;
			}
		}
	}
	public void ChangeCharacters ()
	{
		player2.tag = "Player";
		isMoving = true;
		followSpawn.transform.parent = null;
		playPlace.transform.parent = null;
		controller.enabled = false;
		controller2.enabled = false;
	}

	public void ReassignPlayers() {
		player = GameObject.FindGameObjectWithTag ("Player");
		player2 = GameObject.FindGameObjectWithTag ("Player2");
		controller = player.GetComponent<CharacterController> ();
		controller2 = player2.GetComponent<CharacterController> ();
		playerMov = player.GetComponent<PlayerMovement> ();
	}
}
