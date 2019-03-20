using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaterEnemy : EnemyClass {
	private GameObject water;
	private GameObject waterObj;
	private Transform spawnPoint;
	private bool fireActivate = false;
	void Start () {
		Damage = 30;
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		elementType = "Water";
		water = Resources.Load ("Water") as GameObject;
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
				waterObj = Instantiate (water, spawnPoint.position, transform.rotation) as GameObject;
				waterObj.GetComponent<WaterObjs>().eClass = GetComponent<EnemyClass>();
				waterObj.transform.parent = spawnPoint.transform;
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
			Destroy (waterObj);
			fireActivate = false;
			StartCoroutine ("Cooldown");
		}
	}
}
