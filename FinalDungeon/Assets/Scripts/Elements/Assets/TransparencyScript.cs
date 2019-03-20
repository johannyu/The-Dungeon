using UnityEngine;
using System.Collections;

public class TransparencyScript : MonoBehaviour {
	private float TransparencyNumber = 1f;
	// Use this for initialization
	void Start () {
		if (tag == "Ice") {
			TransparencyNumber = 0.8f;
		}
		if (tag == "Water") {
			TransparencyNumber = 0.5f;
		}
		Color colorObj = GetComponent<Renderer>().material.color;
		gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
		colorObj.a = TransparencyNumber;
		gameObject.GetComponent<Renderer>().material.color = colorObj;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
