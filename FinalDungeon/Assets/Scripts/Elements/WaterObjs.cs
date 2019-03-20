using UnityEngine;
using System.Collections;

public class WaterObjs : MonoBehaviour {
	private WaterPlayer waterP;
	public 	EnemyClass eClass;
	public GameObject eCaster;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		waterP = GameObject.FindGameObjectWithTag("Player").GetComponent<WaterPlayer> ();
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
			if (waterP != null && eCaster != null) {
				waterP.enemy = other.gameObject;
				waterP.canDamage = true;
			}
		}
		if (other.gameObject.tag == "Water") {
			WaterAsset waterA = other.gameObject.GetComponent<WaterAsset> ();
			if (waterA != null && eCaster != null) {
				if (!waterA.isGrowing) {
					waterA.isGrowing = true;
				}
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
			if (waterP != null) {
				waterP.canDamage = false;
				waterP.enemy = null;
			}
		}
		if (other.gameObject.tag == "Water") {
			WaterAsset waterA = other.gameObject.GetComponent<WaterAsset> ();
			if (waterA != null) {
				if (waterA.isGrowing) {
					waterA.isGrowing = false;
				}
			}
		}
	}
}
