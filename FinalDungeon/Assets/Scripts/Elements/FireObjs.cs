using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireObjs : MonoBehaviour {
	private FirePlayer fireP;
	public 	EnemyClass eClass;
	public GameObject eCaster;

	public List<GameObject> iceList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		fireP = GameObject.FindGameObjectWithTag("Player").GetComponent<FirePlayer> ();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			if (eClass != null) 
			{
				eClass.DamagePlayer();
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
			if (fireP != null && eCaster != null) {
				fireP.enemy = other.gameObject;
				fireP.canDamage = true;
			}
		}
		if (other.gameObject.tag == "Ice") {
			if (fireP != null) {
				if (!iceList.Contains (other.gameObject)) {
					iceList.Add (other.gameObject);
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
			if (fireP != null) {
				fireP.canDamage = false;
				fireP.enemy = null;
			}
		}
		if (other.gameObject.tag == "Ice") {
			if (fireP != null) {
				iceList.Remove(other.gameObject);
			}
		}
	}
}
