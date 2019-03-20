using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossEnemy : EnemyClass
{
    private GameObject fire;
    private Renderer colorS;
    private Transform spawnPoint;
    private bool eActivate = false;
    private GameObject air;
    private GameObject elementObj;

    private bool airActivate = false;
    private GameObject water;
    private string elementStr;
    private int numbers;

    private bool fires = true;
    private bool waters = false;
    private bool airs = false;

    public Material fireMat;
    public Material waterMat;
    public Material airMat;

    // Use this for initialization
    void Start()
    {
        air = Resources.Load("Air") as GameObject;
        fire = Resources.Load("Fire") as GameObject;
        water = Resources.Load("Water") as GameObject;
        Damage = 95;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        elementType = "Fire";
        StartCoroutine("ElementSwitch");
        colorS = GetComponent<Renderer>();
        spawnPoint = transform.Find("SpawnPoint");
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        numbers = Random.Range(1, 4);
        WeaknessCalc();
        distance = Vector3.Distance(transform.position, playerObj.transform.position);
        AttackDistance();

        if (Health <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    public IEnumerator ElementSwitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            airActivate = false;
            Destroy(elementObj);
            cooledDown = true;
            if (numbers == 1 && fires != true)
            {
                elementType = "Fire";
                colorS.material = fireMat;
                airs = false;
                waters = false;
                fires = true;

            }
            if (numbers == 2 && waters != true)
            {
                elementType = "Water";
                colorS.material = waterMat;
                airs = false;
                fires = false;
                waters = true;
            }
            if (numbers == 3 && airs != true)
            {
                elementType = "Air";
                colorS.material = airMat;
                waters = false;
                fires = false;
                airs = true;
            }
            //StartCoroutine("ElementSwitch");
        }
    }

    void AttackDistance()
    {
        if (distance <= 10)
        {
            if (!eActivate && cooledDown && elementType == "Fire")
            {
                Debug.Log("thisworking");
                elementObj = Instantiate(fire, spawnPoint.position, transform.rotation) as GameObject;
                elementObj.tag = "Untagged";
                elementObj.GetComponent<FireObjs>().eClass = GetComponent<EnemyClass>();
                elementObj.transform.parent = spawnPoint.transform;
                eActivate = true;
                cooledDown = false;
                StartCoroutine("FireFunction");
            }
            if (!airActivate && cooledDown && elementType == "Air")
            {
                elementObj = Instantiate(air, spawnPoint.position, transform.rotation) as GameObject;
                elementObj.GetComponent<AirObjs>().eClass = GetComponent<EnemyClass>();
                elementObj.tag = "Untagged";
                elementObj.GetComponent<Rigidbody>().AddForce(elementObj.transform.forward * 800);
                airActivate = true;
                cooledDown = false;
                StartCoroutine("FireFunction");
            }
            if (!eActivate && cooledDown && elementType == "Water")
            {
                elementObj = Instantiate(water, spawnPoint.position, transform.rotation) as GameObject;
                elementObj.GetComponent<WaterObjs>().eClass = GetComponent<EnemyClass>();
                elementObj.transform.parent = spawnPoint.transform;
                eActivate = true;
                cooledDown = false;
                StartCoroutine("FireFunction");
            }
        }
    }

    IEnumerator FireFunction()
    {
        if (eActivate)
        {
            yield return new WaitForSeconds(1);
            Destroy(elementObj);
            eActivate = false;
            StartCoroutine("Cooldown");
        }




    }
}
