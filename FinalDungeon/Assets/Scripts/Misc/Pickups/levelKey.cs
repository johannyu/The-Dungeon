using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelKey : MonoBehaviour {
    public GameObject airKey;
    public GameObject waterKey;
    public GameObject fireKey;

    public Image airUI;
    public Image waterUI;
    public Image fireUI;

    private bool hasF = false;
    private bool hasA = false;
    private bool hasW = false;

	// Use this for initialization
	void Start () {
        airKey = GameObject.FindGameObjectWithTag("AirKey");
        waterKey = GameObject.FindGameObjectWithTag("WaterKey");
        fireKey = GameObject.FindGameObjectWithTag("FireKey");

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {


        if (col.gameObject.tag == "AirKey")
        {
            hasA = true;
            Debug.Log("hasA is true");

            if (hasA == true)
            {
                airUI.enabled = true;
            }
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "FireKey")
        {
            hasF = true;
            Debug.Log("hasF is true");

            if (hasF == true)
            {
                fireUI.enabled = true;
            }
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "WaterKey")
        {
            hasW = true;
            Debug.Log("hasW is true");

            if (hasW == true)
            {
                waterUI.enabled = true;
            }
            Destroy(col.gameObject);
        }
        
    }
    

}
