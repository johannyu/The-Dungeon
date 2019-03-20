using UnityEngine;
using System.Collections;
/// <summary>
/// Coded by Johann Yu
/// </summary>
public class CameraScript : MonoBehaviour {

	private GameObject player;

	private Vector3 playerCoor = Vector3.zero;

	private float distToPlay = 7.37f;
	private float heightOfCam = 12.48f;
	public GameObject recruitObj;
	private Recruiting recruit;
	void Start () {
		recruit = recruitObj.GetComponent<Recruiting>();
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find("PlayerPlace");
		playerCoor = player.transform.position;		
		playerCoor = new Vector3(playerCoor.x, heightOfCam, playerCoor.z - distToPlay);
		transform.position = Vector3.Lerp (transform.position, playerCoor, 2 * Time.deltaTime);
		if (recruit.pauseTime) // Pauses the game
		{
			Time.timeScale = 0;
		}
		if (!recruit.pauseTime) // Unpauses the game
		{
			Time.timeScale = 1;
		}
	}
}
