using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    public float maxFireDistance = 200f;
    public static RaycastController instance;
    public Text birdName;
    public Transform gunFlashTarget;
    public float fireRate = 1.6f;
    private bool nextShot = true;
    private string objName = "";
    AudioSource audio;
    public AudioClip[] clips;
    public GameObject newBird;
    public GameObject gunFlash;
    //int layer_mask;
    


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (spawnNewBird());
        audio = GetComponent<AudioSource>();
    }

    //Play an audio clip from the 'clips' array
    public void playSound(int sound)
    {
        audio.clip = clips[sound];
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if(nextShot)
        {
            StartCoroutine (takeShot());
            nextShot = false;
        }
    }

    private IEnumerator takeShot()
    {
        //Call fire sound from the GunController script
        GunController.instance.fireSound();

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        RaycastHit hit;

        GameController.instance.shots--;

        int layer_mask = LayerMask.GetMask("BirdFlight");
        if(Physics.Raycast(ray, out hit, maxFireDistance, layer_mask))
        {
            objName = hit.collider.gameObject.name;

            //debug to make sure raycast is hittting the Bird Collider
            birdName.text = objName;
            Vector3 birdPosition = hit.collider.gameObject.transform.position;

            if(objName == "Bird(Clone)")
            {
                //Load 'Explosion' particle system on location of hit bird
                GameObject Explosion = Instantiate(Resources.Load("Boom", typeof(GameObject))) as GameObject;
                Explosion.transform.position = birdPosition;
                playSound(1);
                Destroy(hit.collider.gameObject);

                //After a new bird spawn it cycles through the game hierarchy to delete clone remains 
                StartCoroutine(spawnNewBird());
                StartCoroutine(clearMemory());

                //Reduces the shots per round, increase scroe, and round score. See GameController.cs
                GameController.instance.shots = 3;
                GameController.instance.playerScore++;
                GameController.instance.roundScore++;
            }

        }

        GameObject gunFlash = Instantiate(Resources.Load("GunSmoke", typeof(GameObject))) as GameObject;

        //Make the smoke a child of 'GunFlash' position
        gunFlash.transform.position = gunFlashTarget.position;

        yield return new WaitForSeconds(fireRate);
        nextShot = true;

        //Clears up memory from any duplicate 'GunSmoke' particle systems
        GameObject[] gunSmokeGroup = GameObject.FindGameObjectsWithTag("GunSmoke");
        foreach(GameObject smoke in gunSmokeGroup)
        {
            Destroy(smoke.gameObject);
        }
    }

    private IEnumerator spawnNewBird()
    {
        yield return new WaitForSeconds(3f);
        //Spawn the new Bird
        GameObject newBird = Instantiate(Resources.Load("Bird", typeof(GameObject))) as GameObject;

        //Make the new Bird a child of Image Target
        newBird.transform.parent = GameObject.Find("ImageTarget").transform;

        //Scale the new Bird
        newBird.transform.localScale = new Vector3(10f,10f,10f);

        //Random Start postiion for the Bird
        Vector3 temp;
        temp.x = Random.Range(-5f,5f);
        temp.y = Random.Range(1f,5f);
        temp.z = Random.Range(-5f,5f);
        newBird.transform.position = new Vector3(temp.x,temp.y,temp.z);
        
    }

    private IEnumerator clearMemory()
    {
        yield return new WaitForSeconds(1f);

        GameObject[] boomGroup = GameObject.FindGameObjectsWithTag("Boom");
        foreach(GameObject boom in boomGroup)
        {
            Destroy(boom.gameObject);
        }
    }
}
