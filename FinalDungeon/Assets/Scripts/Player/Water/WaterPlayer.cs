using UnityEngine;
using System.Collections;

public class WaterPlayer : Player
{

    private GameObject water;
    private GameObject waterObj;
    private Transform  spawnArea;
	private PlayerSwitch playerSwitch;
    private bool firstHit = false;

	// Use this for initialization
	void Start ()
    {
        ElementType = "Water";
        spawnArea = transform.Find("WaterSpawn");
        water = Resources.Load("Water") as GameObject;
		playerSwitch = GameObject.Find ("PlayerPlace").GetComponent<PlayerSwitch> ();
        Damage = 70;
    }
	
	// Update is called once per frame
	void Update ()
    {
		UseEnergy ();
		if (gameObject.tag == "Player" && playerSwitch.isMoving == false) 
		{
			ElementAttack ();
		}
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			Destroy(waterObj);
			StopCoroutine("WaterDamage");
		}
    }
    public override void ElementAttack()
    {
        base.ElementAttack();
        if (Input.GetMouseButtonDown(0))
        {
			if (Energy >= 0) {
				usingEnergy = true;
				waterObj = Instantiate (water, spawnArea.transform.position, spawnArea.transform.rotation) as GameObject;
				waterObj.transform.parent = spawnArea.transform;
				waterObj.GetComponent<WaterObjs> ().eCaster = gameObject;
				StartCoroutine ("DamageEnemy");
			}
        }
		if (Input.GetMouseButtonUp(0) || Energy <= 0)
        {
			usingEnergy = false;
			Destroy(waterObj);
			StopCoroutine("DamageEnemy");
        }
    }
}
