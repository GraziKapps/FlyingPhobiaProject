using UnityEngine;
using System.Collections;

public class SitDown : MonoBehaviour
{

    // Use this for initialization
    public GameObject oculusController;
    public GameObject oculusController2;
    private CharacterController chController;
    public GameObject plane;
    private MoveForward move;
    private TakeOff takeOff;
    private PlayMovie movie;
    public GameObject engine;
    public GameObject engineFirst;
    public GameObject engineLast;
    
    private AudioSource engineBegin;
    private AudioSource engineEnd;
    public GameObject[] arrowSigns;
    public GameObject moviePlayer;
    Vector3 aux;
    // [System.Serializable]
    // public AudioSource[] engineAudio;
    private AudioSource engineAudio;
    private AudioSource movieAudio;

    // Avatares que o controlador não vê
    private GameObject avatares1;
    private GameObject avatares2;
    private GameObject avatares3;

    //[SerializeField]
   // private GameObject airport;

   // private AudioSource airportChatterAudio;
    void Start()
    {
        chController = oculusController.GetComponent<CharacterController>();
        //plane =
        move = plane.GetComponent<MoveForward>();
        takeOff = plane.GetComponent<TakeOff>();
        engineAudio = engine.GetComponent<AudioSource>();
        engineBegin = engineFirst.GetComponent<AudioSource>();
        engineEnd = engineLast.GetComponent<AudioSource>();

        movie = moviePlayer.GetComponent<PlayMovie>();
        movieAudio = moviePlayer.GetComponent<AudioSource>();
       // airportChatterAudio = airport.GetComponent<AudioSource>();  

        // avatares que o controlador não vê
        avatares1 = GameObject.Find("avataresAviao01");
        avatares2 = GameObject.Find("avataresAviao02");
        avatares3 = GameObject.Find("avataresAviao03");

    }

    // Update is called once per frame
  
    /*
     * TODO:
     * Fix body Z position;
     * Lerp the transition between standing and sitted down.
     * Start of the engine needs to take longer. (not here, but in TakeOff.cs)
     * Sound effect or the turbine must start slow, then go into the loop. Use 2 different audio sources.
     */
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sentou!");

        // Desativa os avatares que o controlador não vê
        avatares1.SetActive(false);
        avatares2.SetActive(false);
        avatares3.SetActive(false);

        foreach (GameObject a in arrowSigns)
            GameObject.Destroy(a);

        oculusController.transform.parent = plane.transform;

        Transform cameraRig = oculusController.transform.GetChild(1);
        aux = new Vector3((float)cameraRig.transform.localPosition.x, -10, (float)cameraRig.transform.localPosition.z);
        oculusController.transform.position = new Vector3(
            oculusController.transform.position.x - 0.2125f, 
            oculusController.transform.position.y - 0.2f,
            oculusController.transform.position.z + 0.25f);
        // cameraRig.localPosition =  Vector3.Lerp(cameraRig.transform.localPosition, aux, Time.deltaTime*2);
        chController.enabled = false;
    
        takeOff.enabled = true;
        //move.enabled = true;
        //movie.enabled = true;
        StartCoroutine(waitSeconds());
        movie.ActuallyPlay(movieAudio);
     //   airportChatterAudio.enabled = false;


        //movieAudio.Play();
        //movieAudio.enabled = true;



        //        Debug.Log("move + " + move.enabled);


    }
    public void reenable()
    {
        oculusController2.transform.position = new Vector3(
             oculusController.transform.position.x + 0.2125f,
             oculusController.transform.position.y + 0.35f,
             oculusController.transform.position.z - 0.3f);

        oculusController2.SetActive(true);
        oculusController.SetActive(false);
        //chController.enabled = true;
    }

    IEnumerator waitSeconds()
    {
        Debug.Log("Waiting... 4 seconds");
        yield return new WaitForSeconds(5);
        movie.ActuallyPlay(movieAudio);
        
        yield return new WaitForSeconds(22);
        Debug.Log("Waiting... 30 seconds");
        engineBegin.enabled = true;
        yield return new WaitForSeconds(10);
       // foreach (AudioSource audio in engineAudio)
       // {
            engineAudio.enabled = true;
       // }
        Debug.Log("Acabouuu!!! É tetraaaaa!");
    }
}
