using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour {

	private float 			health 	= 100;
	private float 			armor  	= 1.0f;
	private float 			damage 	= 0.0f;
	public 	float 			playerArmor = 0.0f;
	public 	float 			distance	= 0f;
	public 	float 			coolVal	= 2f;
	public 	string 			elementType;
	public 	GameObject 		playerObj;
	public 	PlayerClass 	playerClass;

	public 	UnityEngine.AI.NavMeshAgent 	nav;
	public 	bool 			cooledDown = true;
	public  bool			canDamage = true;

	public 	Image			healthbar;

	public 	GameObject 		recruit;
	private Recruiting 		reScript;

	public 	float Health
	{
		// Getter and a setter
		get
		{
			return health;
		}
		set
		{
			health = value;
			if(health <= 0)
			{
				reScript = recruit.GetComponent<Recruiting> ();
				if (elementType == "Fire" && reScript.ePlayer != "Fire") {
					recruit.SetActive(true);
					reScript.deathMsg.text = reScript.fireMsg;
					reScript.pauseTime = true;
					reScript.ePlayer = elementType;
					Destroy (gameObject, 0.1f);
				}
				if (elementType == "Water" && reScript.ePlayer != "Water") {
					recruit.SetActive(true);
					reScript.deathMsg.text = reScript.waterMsg;
					reScript.pauseTime = true;
					reScript.ePlayer = elementType;
					Destroy (gameObject, 0.1f);
				}
                if (elementType == "Air" && reScript.ePlayer != "Air")
                {
                    recruit.SetActive(true);
                    reScript.deathMsg.text = reScript.airMsg;
                    reScript.pauseTime = true;
                    reScript.ePlayer = elementType;
                    Destroy(gameObject, 0.1f);
                }
                else
                {
                    Destroy(gameObject);
                }
				reScript.popUp.SetActive(true);
			}
		}
	}
	public float Armor
	{
		get
		{
			return armor;
		}
		set
		{
			armor = value;
		}
	}

	public float Damage
	{
		get
		{
			return damage;
		}
		set
		{
			damage = value;
		}
	}

	public virtual void Update () {
		healthbar.fillAmount = Health / 100;
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		playerClass = playerObj.GetComponent<PlayerClass>();
		distance    = Vector3.Distance(transform.position, playerObj.transform.position);
		FollowPlayer ();
	}	

	public void FollowPlayer()
	{
		if (distance >= 7) {
			nav.destination = playerObj.transform.position;
			LookFunction ();
		} 
		if(distance < 7)
		{
			LookFunction ();
			nav.destination = transform.position;
		}
		if (distance >= 10) {
			nav.destination = transform.position;
		}
	}

	void LookFunction(){
		Quaternion lookPos = Quaternion.LookRotation (playerObj.transform.position - transform.position);
		lookPos.x = 0;
		lookPos.z = 0;
		transform.rotation = Quaternion.Lerp (transform.rotation, lookPos, 30 * Time.deltaTime);
	}

	public virtual void WeaknessCalc(){
		if (playerClass.ElementType == "Normal") {
			if (elementType == "Fire") 
			{
				Armor = 3.5f;
				playerArmor = 25f;
			}
			if (elementType == "Water") 
			{
				Armor = 3.5f;
				playerArmor = 25f;
			}
			if (elementType == "Air") 
			{
				Armor = 3.5f;
				playerArmor = 25f;
			}
		}
		if (playerClass.ElementType == "Fire") {
			if (elementType == "Fire") {
				Armor = 5f;
				playerArmor = 6f;
			}
			if (elementType == "Water") {
				Armor = 8f;
				playerArmor = 3f;
			}
			if (elementType == "Air") 
			{
				Armor = 3f;
				playerArmor = 12f;
			}
		}
		if (playerClass.ElementType == "Water") {
			if (elementType == "Fire") {
				Armor = 3f;
				playerArmor = 12f;
			}
			if (elementType == "Water") {
				Armor = 5f;
				playerArmor = 6f;
			}
			if (elementType == "Air") 
			{
				Armor = 8f;
				playerArmor = 3f;
			}
		}
		if (playerClass.ElementType == "Air") {
			if (elementType == "Fire") {
				Armor = 8f;
				playerArmor = 3f;
			}
			if (elementType == "Water") {
				Armor = 3f;
				playerArmor = 12f;
			}
			if (elementType == "Air") 
			{
				Armor = 5f;
				playerArmor = 6f;
			}
		}
	}
	public void DamagePlayer(){
		playerClass.Health -= Damage / playerArmor;
	}


	public IEnumerator Cooldown(){
		if (!cooledDown) {
			yield return new WaitForSeconds(coolVal);
			cooledDown = true;
		}
	}

}
