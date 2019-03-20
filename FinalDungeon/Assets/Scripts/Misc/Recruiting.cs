using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Recruiting : MonoBehaviour {
	public GameObject firePlayer;
	public GameObject waterPlayer;
	public GameObject airPlayer;

	public string ePlayer;

	public GameObject popUp;

	private GameObject mainPlayer;
	private PlayerSwitch playerSwitch;

	public bool pauseTime = false;
	public bool frontlines = false;

	public Text deathMsg;
	public string fireMsg;
	public string waterMsg;
	public string airMsg;
	// Use this for initialization
	void Start () {
		mainPlayer = GameObject.Find ("PlayerPlace");
		playerSwitch = mainPlayer.GetComponent<PlayerSwitch> ();

		fireMsg 	= "Fire Wizard: Please don't kill me, let me join you! I can melt down ice blocks that are blocking the way!";
		waterMsg 	= "Water Wizard: Spare me good sir, I wish to join you. If you need to get across a trench, I'll fill it with water.";
		airMsg 		= "Air Wizard: Killing me would be a mistake. I predict you'll need me to push away heavy boulders.";

		gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if (firePlayer.tag == "Player" || waterPlayer.tag == "Player" || airPlayer.tag == "Player") {
			frontlines = true;
		} 
		else 
		{
			frontlines = false;
		}
	}

	public void ButtonYes(){
		if (frontlines) {
			playerSwitch.ChangeCharacters ();
			if (ePlayer == "Fire") {
				waterPlayer.SetActive (false);
				airPlayer.SetActive (false);
				firePlayer.SetActive (true);
				firePlayer.transform.position = mainPlayer.transform.position;
			}
			if (ePlayer == "Water") {
				firePlayer.SetActive (false);
				airPlayer.SetActive (false);
				waterPlayer.SetActive (true);
				waterPlayer.transform.position = mainPlayer.transform.position;
			}
			if (ePlayer == "Air") {
				firePlayer.SetActive (false);
				waterPlayer.SetActive (false);
				airPlayer.SetActive (true);
				airPlayer.transform.position = mainPlayer.transform.position;
			}
			pauseTime = false;
			gameObject.SetActive (false);
		}
		if (!frontlines) {
			if (ePlayer == "Fire") {
				waterPlayer.SetActive (false);
				airPlayer.SetActive (false);
				firePlayer.SetActive (true);
				firePlayer.transform.position = mainPlayer.transform.position;
			}
			if (ePlayer == "Water") {
				firePlayer.SetActive (false);
				airPlayer.SetActive (false);
				waterPlayer.SetActive (true);
				waterPlayer.transform.position = mainPlayer.transform.position;
			}
			if (ePlayer == "Air") {
				firePlayer.SetActive (false);
				waterPlayer.SetActive (false);
				airPlayer.SetActive (true);
				airPlayer.transform.position = mainPlayer.transform.position;
			}
			playerSwitch.ReassignPlayers ();
			pauseTime = false;
			gameObject.SetActive (false);
		}
	}

	public void ButtonNo(){
		pauseTime = false;
		gameObject.SetActive (false);
	}
}
