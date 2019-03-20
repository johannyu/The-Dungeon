using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour {
	private float   health          = 100f;  // A variable for the character's health points
    private int     lives           = 3;    // A variable to see how many lives the character has
    private float     energy          = 100f;  // A variable for the character's energy for attacks

	private float 	healthValue;
	private float 	randomValue;
	private float   damage          = 0.0f; // A variable for the character's damage
	public 	float	initDamage		= 0.0f;
    public  float   cooldownRate    = 3.0f; // A variable to see how long skills take to cool down
	public 	float	energyVal		= 0.45f;

    private string  elementType     = null; // A variable to record the element type
	public bool addedToList = false;

	public bool canDamage = false;
	public bool usingEnergy = false;
	public GameObject	enemy;
	public GameObject 	mainPlayer;
    public GameObject firePlayer;
    public GameObject waterPlayer;
    public GameObject airPlayer;
    private Recruiting lvlKey;

    public float Health // Gets and sets the private variable of health
	{
		get
		{ 
			return health;
		}
		set
		{ 
			health = value;
			if (health <= 0.0f && gameObject != mainPlayer) 
			{
				GameObject.Find("PlayerPlace").GetComponent<PlayerSwitch>().ChangeCharacters();
			}
			if (health <= 0.0f && gameObject == mainPlayer) {
				SceneManager.LoadScene (2);
			}
		}
	}
    public float Energy // Gets and sets the private variable of energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
        }
    }

    public float Damage // Gets and sets the private variable of damage
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

    public string ElementType // Gets and sets the private variable of elementType
    {
        get
        {
            return elementType;
        }
        set
        {
            elementType = value;
        }
    }

	public void UseEnergy()
	{
		if (usingEnergy) {
			Energy -= energyVal;
		} 
		if (!usingEnergy)
		{
			if (Energy <= 100) {
				Energy += 0.3f;
			}
		}
		if (Input.GetKeyDown (KeyCode.Q) && GameObject.Find("PlayerPlace").GetComponent<PlayerSwitch>().isMoving == true) {
			usingEnergy = false;
			canDamage = false;
		}
	}

	/// <summary>
	/// Changes the direction in which the player is facing to prep for elemental attack.
	/// </summary>
    public virtual void ElementAttack()
    { 
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButton (0)) 
		{
			if (Physics.Raycast(ray, out hit))
			{
				Quaternion lookPos = Quaternion.LookRotation (hit.point - transform.position);
				lookPos.x = 0;
				lookPos.z = 0;
				transform.rotation = Quaternion.Lerp (transform.rotation, lookPos, 10 * Time.deltaTime);
			}
		}
	}

	public void DamageEffect(){
		if (enemy != null) 
		{
			enemy.GetComponent<EnemyClass> ().Health -= Damage / enemy.GetComponent<EnemyClass> ().Armor;
		}
	}

	public virtual IEnumerator DamageEnemy() {
		while (true) {
			yield return new WaitForSeconds (0.8f);
			if (canDamage) {
				DamageEffect ();
			}
		}
	}

    void OnControllerColliderHit(ControllerColliderHit other) {
        if (other.gameObject.tag == "MedKit")
        {

            Destroy(other.gameObject);
            if (Health <= 100)
            {
                randomValue = Random.Range(5, 25);
                healthValue = Health + randomValue;
                if (healthValue >= 100)
                {
                    Health = 100;
                }
                else {
                    Health += randomValue;
                }
            }
        }
        if (other.gameObject.tag == "Door")
        {
            levelKey lvlKey = GetComponent<levelKey>();
            if (lvlKey.fireUI != null && lvlKey.waterUI != null && lvlKey.airUI != null)
            {
                SceneManager.LoadScene(4);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (gameObject == mainPlayer)
        {
            if (other.gameObject.tag == "FirePickUp")
            {
                waterPlayer.SetActive(false);
                airPlayer.SetActive(false);
                firePlayer.SetActive(true);
                firePlayer.transform.position = transform.position;
                GameObject.Find("PlayerPlace").GetComponent<PlayerSwitch>().ReassignPlayers();
            }
            if (other.gameObject.tag == "WaterPickUp")
            {
                firePlayer.SetActive(false);
                airPlayer.SetActive(false);
                waterPlayer.SetActive(true);
                waterPlayer.transform.position = transform.position;
                GameObject.Find("PlayerPlace").GetComponent<PlayerSwitch>().ReassignPlayers();
            }
            if (other.gameObject.tag == "AirPickUp")
            {
                firePlayer.SetActive(false);
                waterPlayer.SetActive(false);
                airPlayer.SetActive(true);
                airPlayer.transform.position = transform.position;
                GameObject.Find("PlayerPlace").GetComponent<PlayerSwitch>().ReassignPlayers();
            }
        }

	}
}
