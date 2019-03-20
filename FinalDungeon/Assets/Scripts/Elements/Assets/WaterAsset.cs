using UnityEngine;
using System.Collections;

public class WaterAsset : MonoBehaviour {
	private bool canGrow = true;
	public bool isGrowing = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (canGrow) {
			if (isGrowing) {
				Vector3 yCoor = new Vector3 (transform.localScale.x, transform.localScale.y + 1, transform.localScale.z);
				transform.localScale = Vector3.Lerp (transform.localScale, yCoor, 2 * Time.deltaTime);
			}
			if (Input.GetMouseButtonUp (0) || Input.GetKeyDown(KeyCode.Q) || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().Energy <= 0) {
				isGrowing = false;
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "WaterLimit") {
			canGrow = false;
		}
	}
}
