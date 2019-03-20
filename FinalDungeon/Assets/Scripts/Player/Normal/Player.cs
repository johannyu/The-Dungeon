using UnityEngine;
using System.Collections;

public class Player : PlayerClass {

	private GameObject 	meleeAttack;
	private Transform psuedoPlayer;
	// Use this for initialization
	void Start () {
		mainPlayer = gameObject;
		ElementType = "Normal";
		Damage = 50;
		meleeAttack = GameObject.Find("Melee");
		psuedoPlayer = transform.Find ("Psuedo");
	}
	
	// Update is called once per frame
	void Update () {
		InputCheck();
		UseEnergy();
	}

	void InputCheck ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && cooldownRate >= 3.0f && tag == "Player" && Energy >= 0) 
		{
			cooldownRate = 0;
			Energy -= 25f;
			StartCoroutine("AttackAnimation");
			if (canDamage && enemy != null) 
			{
				DamageEffect();
			}
		}
	}

	IEnumerator AttackAnimation() 
	{
		int 	moveNum 	= 0;
		for (int i = 0; i <= 2; i++) 
		{
			psuedoPlayer.position = Vector3.MoveTowards(psuedoPlayer.position, meleeAttack.transform.position, i);
			yield return new WaitForSeconds(Time.deltaTime);
			moveNum = i;
		}
		if (moveNum >= 2) 
		{
			for (int i = 0; i <= 2; i++) 
			{
				psuedoPlayer.position = Vector3.MoveTowards (psuedoPlayer.position, transform.position, i);
				yield return new WaitForSeconds(Time.deltaTime);
			}
		}
		for (float f = 0.0f; f <= 3.0f; f += 0.5f)
		{
			cooldownRate = f;
			yield return new WaitForSeconds(0.1f);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
			canDamage = true;
			enemy = other.gameObject;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
			canDamage = false;
			enemy = null;
		}
	}

}
