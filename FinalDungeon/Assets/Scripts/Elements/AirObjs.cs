using UnityEngine;
using System.Collections;

public class AirObjs : MonoBehaviour {
	private AirPlayer airP;
	public EnemyClass eClass;
	public GameObject eCaster;
	// Use this for initialization
	void Start () {
		airP = GameObject.FindGameObjectWithTag ("Player").GetComponent<AirPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Enemy") {
			if (airP != null && eCaster != null) {
				airP.enemy = other.gameObject;
				airP.DamageEffect ();
				Destroy (gameObject);
			}
		}
		if (other.gameObject.tag == "Player") {
			Destroy (gameObject);
			if (eCaster != null) 
			{
				airP.usingEnergy = false;
			}
			if (eClass != null) 
			{
				eClass.DamagePlayer ();
			}
		}
	}
}
