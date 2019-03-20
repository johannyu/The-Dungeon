using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour {
	public Camera camObj;
	public GameObject playerPos;
	public Vector3 playerVector;
	public Image healthbar;
	public Image energybar;
	private PlayerClass playerC;
	// Use this for initialization
	void Start () {
		playerPos = GameObject.Find ("PlayerPlace");
	}
	
	// Update is called once per frame
	void Update () {
		playerC = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerClass>();
		playerVector = new Vector3 (playerPos.transform.position.x, 3, playerPos.transform.position.z);
		if (gameObject.name == "PlayerHealth") {
			transform.position = playerVector;
			healthbar.fillAmount = playerC.Health / 100;
			energybar.fillAmount = playerC.Energy / 100;
			if (playerC.Health / 100 <= 0.3) {
				healthbar.color = Color.yellow;
			} 
			else 
			{
				healthbar.color = Color.green;
			}
		}
		transform.LookAt(transform.position + camObj.transform.rotation * Vector3.forward, camObj.transform.rotation * Vector3.up);
	}
}
