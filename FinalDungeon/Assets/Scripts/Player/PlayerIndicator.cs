using UnityEngine;
using System.Collections;

public class PlayerIndicator : MonoBehaviour {
	private GameObject player;

    private Vector3 playerXZ;

	void Start () {
	
	}
	
	void Update () {
		player = GameObject.Find("PlayerPlace");
		playerXZ = new Vector3(player.transform.position.x, 0.1f, player.transform.position.z);
		transform.position = Vector3.MoveTowards (transform.position, playerXZ, 1);
		transform.rotation = Quaternion.Lerp (transform.rotation, player.transform.rotation, 10 * Time.deltaTime);
	}
}
