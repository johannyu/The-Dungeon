using UnityEngine;
using System.Collections;

public class KinematicScript : MonoBehaviour {
	private Rigidbody rigid;
	private bool clicking = false;
	private GameObject gameObj;
	[SerializeField]
	private bool canUseGravity = true;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			clicking = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			clicking = false;
		}
		if (gameObj == null && !canUseGravity) {
			rigid.isKinematic = true;
		}
	}

	void OnTriggerStay (Collider other) {
		if (gameObject.tag == "Boulder") {
			canUseGravity = false;
			if(other.gameObject.tag == "Air" && !clicking){
				rigid.isKinematic = false;
				gameObj = other.gameObject;
			}
		}
		if (gameObject.tag == "Ice") {
			canUseGravity = false;
			if (other.gameObject.tag == "Fire") {
				rigid.isKinematic = false;
				gameObj = other.gameObject;
			}
		}
	}
}
