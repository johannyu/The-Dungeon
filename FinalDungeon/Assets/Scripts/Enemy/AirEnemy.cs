using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AirEnemy : EnemyClass {

	private GameObject air;
	private GameObject airObj;
	private Transform spawnPoint;
	private bool airActivate = false;
	void Start () {
		Damage = 30;
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		elementType = "Air";
		air = Resources.Load ("Air") as GameObject;
		spawnPoint = transform.Find ("SpawnPoint");
		coolVal = 1f;
		healthbar = transform.Find ("HealthCanvas").Find ("HealthGrey").Find ("HealthRed").GetComponent<Image>();
	}

	// Update is called once per frame
	public override void Update () {
		base.Update ();
		WeaknessCalc ();
		AttackDistance ();
		if (Health <= 0 && airObj != null) {
			Destroy (airObj);
		}
	}

	void AttackDistance(){
		if (distance <= 10) {
			if (!airActivate && cooledDown) {
				airObj = Instantiate (air, spawnPoint.position, transform.rotation) as GameObject;
				airObj.GetComponent<AirObjs> ().eClass = GetComponent<EnemyClass>();
				airObj.tag = "Untagged";
				airObj.GetComponent<Rigidbody> ().AddForce (airObj.transform.forward * 800);
				airActivate = true;
				cooledDown = false;
				StartCoroutine ("FireFunction");
			}
		}
	}

	IEnumerator FireFunction(){
		if (airActivate) 
		{
			yield return new WaitForSeconds (1);
			Destroy (airObj);
			airActivate = false;
			StartCoroutine ("Cooldown");
		}
	}
}
