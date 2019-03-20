using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreens : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame() {
		SceneManager.LoadScene(1);
	}

	public void BackToTitle(){
		SceneManager.LoadScene(0);
	}
}
