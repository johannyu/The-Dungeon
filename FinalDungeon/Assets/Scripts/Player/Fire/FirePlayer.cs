using UnityEngine;
using System.Collections;

public class FirePlayer : PlayerClass {

	private GameObject	fire;
	private GameObject	fireObj;
	private Transform	spawnPoint;

	private PlayerSwitch playerSwitch;
	private FireObjs fireScript;

	// Use this for initialization
	void Start () {
		ElementType = "Fire";
		spawnPoint = transform.Find("FireSpawn");
		fire = Resources.Load("Fire") as GameObject;
		Damage = 70;
		playerSwitch = GameObject.Find ("PlayerPlace").GetComponent<PlayerSwitch> ();
	}
	
	// Update is called once per frame
	void Update () {
		UseEnergy ();
		if (gameObject.tag == "Player" && playerSwitch.isMoving == false) 
		{
			ElementAttack ();
		}
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			Destroy(fireObj);
			StopCoroutine("FireDamage");
			StopCoroutine ("IceReduce");
		}
	}

	public override void ElementAttack ()
	{
		base.ElementAttack();
		if (Input.GetMouseButtonDown(0)) 
		{
			if (Energy >= 0) {
				usingEnergy = true;
				fireObj = Instantiate (fire, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
				fireObj.transform.parent = spawnPoint.transform;
				fireScript = fireObj.GetComponent<FireObjs> ();
				fireObj.GetComponent<FireObjs> ().eCaster = gameObject;
				StartCoroutine ("DamageEnemy");
				StartCoroutine ("IceReduce");
			}
		}
		if (Input.GetMouseButtonUp(0) || Energy <= 0) 
		{
			usingEnergy = false;
			fireScript = null;
			Destroy(fireObj);
			StopCoroutine("DamageEnemy");
			StopCoroutine ("IceReduce");
		}
	}

	IEnumerator IceReduce(){
		while (true) 
		{
			yield return new WaitForSeconds (1f);
			foreach (GameObject fireP in fireScript.iceList) 
			{
				if (fireP != null) 
				{
					fireP.transform.localScale = Vector3.MoveTowards (fireP.transform.localScale, Vector3.zero, 40 * Time.deltaTime);
					if (Vector3.Distance (fireP.transform.localScale, Vector3.zero) <= 0.7f) 
					{
						Destroy (fireP);
					}
				}
			}
		}
	}
}
