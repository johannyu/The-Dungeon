using UnityEngine;
using System.Collections;

public class AirPlayer : Player {

    private GameObject air;
	public GameObject airObj;
    private Transform spawnArea;
	private PlayerSwitch playerSwitch;
	public 	bool cooledDown = true;
	private bool canPress = true;

	// Use this for initialization
	void Start () {
        ElementType = "Air";
        spawnArea = transform.Find("AirSpawn");
        air = Resources.Load("Air") as GameObject;
		playerSwitch = GameObject.Find ("PlayerPlace").GetComponent<PlayerSwitch> ();
        Damage = 80;
		energyVal = 0.4f;
    }
	
	// Update is called once per frame
	void Update () {
		UseEnergy();
		if (gameObject.tag == "Player" && playerSwitch.isMoving == false) 
		{
            ElementAttack();
        }
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			Destroy(airObj);
			StopCoroutine("AirDamage");
		}
    }
    public override void ElementAttack()
    {
        base.ElementAttack();
        if (Input.GetMouseButtonDown(0))
        {
			if (cooledDown == true && Energy >= 0) {
				usingEnergy = true;
				airObj = Instantiate (air, spawnArea.transform.position, spawnArea.transform.rotation) as GameObject;
				airObj.transform.parent = spawnArea.transform;
				airObj.GetComponent<AirObjs> ().eCaster = gameObject;
				canPress = true;
			}
        }
		if (Input.GetMouseButtonUp(0) || Energy <= 0)
        {
			if (airObj != null && canPress) {
				canPress = false;
				usingEnergy = false;
				airObj.transform.parent = null;
				airObj.GetComponent<Rigidbody> ().AddForce (airObj.transform.forward * 800);
				Destroy (airObj, 1);
				StartCoroutine ("Cooldown");
			}
        }
    }

	IEnumerator Cooldown(){
		if (cooledDown) {
			cooledDown = false;
			yield return new WaitForSeconds(1);
			cooledDown = true;
		}
	}
}
