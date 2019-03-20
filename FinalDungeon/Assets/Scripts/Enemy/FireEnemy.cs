using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireEnemy : EnemyClass {
	private GameObject fire;
	private GameObject fireObj;
	private Transform spawnPoint;
	private bool fireActivate = false;
	void Start () {
		Damage = 30;
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		elementType = "Fire";
		fire = Resources.Load ("Fire") as GameObject;
		spawnPoint = transform.Find ("SpawnPoint");
		healthbar = transform.Find ("HealthCanvas").Find ("HealthGrey").Find ("HealthRed").GetComponent<Image>();
	}

	// Update is called once per frame
	public override void Update () {
		base.Update ();
		WeaknessCalc ();
		AttackDistance ();
	}

	void AttackDistance(){
		if (distance <= 10) {
			if (!fireActivate && cooledDown) {
				fireObj = Instantiate (fire, spawnPoint.position, transform.rotation) as GameObject;
				fireObj.tag = "Untagged";
				fireObj.GetComponent<FireObjs> ().eClass = GetComponent<EnemyClass>();
				fireObj.transform.parent = spawnPoint.transform;
				fireActivate = true;
				cooledDown = false;
				StartCoroutine ("FireFunction");
			}
		}
	}

	IEnumerator FireFunction(){
		if (fireActivate) 
		{
			yield return new WaitForSeconds (1);
			Destroy (fireObj);
			fireActivate = false;
			StartCoroutine ("Cooldown");
		}
	}
}
