using UnityEngine;
using System.Collections;
using System;


//Used to control plane movement. Name is a little off since the script keeps the plane cruising and it's not used only for making it take off.
public class TakeOff : MonoBehaviour
{

    private int speed;
    private CharacterController chController;
    [SerializeField]
    private GameObject oculusController;
    bool hasNormalized = false;
    int i = 0;
    
    public Vector3 getPosition()
    {
        return transform.position;
    }

    //enum for plane current states
    enum PlaneStates { pullback, taxiout, climb, cruise, fall,stop };
    PlaneStates currentState;
    private Vector3 airportPosition;
    
    [SerializeField]
    private GameObject airport;
    private Transform parentposition;
    [SerializeField]
    private GameObject corridor;
    [SerializeField]
    private GameObject waitRoom;
    private Transform parentpositionRoom;
    [SerializeField]
    private GameObject Seat;
    [SerializeField]
    private GameObject walkingLady;
    [SerializeField]
    private GameObject walkingLadyBack;
    private SitDown sitDownScript;
    // Use this for initialization
    //Starts at pullback
    void Start()
    {
        currentState = PlaneStates.pullback;
        // currentState = PlaneStates.waitingVideo;
        chController = oculusController.GetComponent<CharacterController>();
        sitDownScript = Seat.GetComponent<SitDown>();
    }

    // Update is called once per frame
    void Update()
    {
        //Wait for awhile to see video playing a litte before starting
        StartCoroutine(waitSeconds());
        // SamsungTV.

        // StartCoroutine(TakingOffProcess());


    }

    IEnumerator waitSeconds()
    {
        Debug.Log("Waiting Video at TakeOff..");
        yield return new WaitForSeconds(50);
        StartFlying();
        //  Debug.Log("Acabouuu!!! É tetraaaaa!");
    }

    //void waitingVideo()
    //{
    //    StartCoroutine(waitSeconds());
    //    currentState = PlaneStates.pullback;
    //    Debug.Log("Terminei");

    //}

    void PullBack()
    {
        if (speed < 100)
        {
            // Debug.Log("lol");
            transform.position += transform.forward * Time.deltaTime * 20f;
            speed++;
        }
        else
        {
            currentState = PlaneStates.taxiout;
            speed = 0;
        }

    }

    void TaxiOut()
    {
        /*
         * Rotate around y axis.
         */


        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 150, 0), Time.deltaTime);
        // Debug.Log("uououou " + transform.rotation.eulerAngles.y);
        if (transform.rotation.eulerAngles.y <= 190)
        {
            // Debug.Log("Cabou essa putaria");
            currentState = PlaneStates.climb;

        }

    }

    void Climb()
    {
        /*
         * Actual take-off
         */
        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y, 30), 2 * Time.deltaTime);
        Debug.Log("to no climb");

        if (speed < 700)
        {
            transform.position -= transform.forward * Time.deltaTime * 20f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 45, 0), Time.deltaTime);
           
            speed++;
        }

        else if (speed < 900)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(45,45, 0), Time.deltaTime);
            //Quaternion newPiece = transform.rotation * Quaternion.Euler(0, 5, 0);
            transform.position -= transform.forward * Time.deltaTime * 20f;
            transform.position += new Vector3(0, 1, 0);
            speed++;


        }

        else
        {
            speed = 0;
            currentState = PlaneStates.cruise;
        }
    }

    private void Fall()
    {
        

        //funciona mais ou menos... agora ver como sair disso pra outra etapa
      if(i < 300)
        {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-45, -150, 0), Time.deltaTime);
        //Quaternion newPiece = transform.rotation * Quaternion.Euler(0, 5, 0);
        transform.position -= transform.forward * Time.deltaTime * 20f;
        transform.position -= new Vector3(0, 1, 0);
        Debug.Log("???");
          i++;
        }
      else if (i < 600)
      {
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 150, 0), Time.deltaTime);
          Debug.Log("WUWIWUIW");

          
          //   throw new Exception();
          //-565.5881 y final position
          //create new airport at this position.
          //For now, just some sort of floor will do
          i++;

      }
      else
      {
          airportPosition = new Vector3(transform.position.x, transform.position.y -7, transform.position.z);

          parentposition = GameObject.FindGameObjectWithTag("Spawn").transform;
          Instantiate(airport, airportPosition, Quaternion.identity);
          currentState = PlaneStates.stop;

          
          // get sphere position. // instantiate from child does not make sense.
         
          Vector3 parentpositionVector = new Vector3(parentposition.position.x, parentposition.position.y, parentposition.position.z);
          GameObject newcorridor =(GameObject)Instantiate(corridor, parentpositionVector, Quaternion.identity);

          newcorridor.transform.rotation = Quaternion.Euler(0, 141.6595f, 0);
          
          //tá pegando do antigo... tenho que pegar o novo... como?



          parentpositionRoom = newcorridor.transform.FindChild("Corredor de Embarque").FindChild("RoomIn").transform ;          
          Debug.Log(parentpositionRoom.position.x);

          // get sphere position. // instantiate from child does not make sense.
         
          Vector3 parentpositionRoomVector = new Vector3(parentpositionRoom.position.x, parentpositionRoom.position.y, parentpositionRoom.position.z);
          GameObject newRoom = (GameObject)Instantiate(waitRoom, parentpositionRoomVector, Quaternion.identity);
          newRoom.transform.rotation = Quaternion.Euler(0, 102.6279f, 0);

          //newcorridor.transform.rotation = Quaternion.Euler(0, 107.1242f, 0);
          //reset oculus position
         // int l=0;
          Debug.Log("BU" + oculusController.transform.parent);
         // while(l < 300){
          transform.DetachChildren();
          sitDownScript.reenable();
          
        //  l++;
         // }
      }
        

    }

    private void Cruise()
    {
        //  if (transform.rotation.eulerAngles.y <= 261)
        // {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 150, 0), Time.deltaTime);
        // }
        transform.position -= transform.forward * Time.deltaTime * 20f;

        Debug.Log("crusando");
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("pertei");
            currentState = PlaneStates.fall;
        }
        if (walkingLady != null)
        {
            StartCoroutine(waitSecondsWalk());

        }
        else
        {
            if(walkingLadyBack!=null)
                StartCoroutine(waitSecondsWalkBack());
        }
        
    }


    IEnumerator waitSecondsWalkBack()
    {
        yield return new WaitForSeconds(20);
        if (walkingLadyBack != null)
        if (walkingLadyBack.active == false)
        {
            walkingLadyBack.SetActive(true);
        }
    }

    IEnumerator  waitSecondsWalk()
    {
        yield return new WaitForSeconds(20);
        if (walkingLady != null)
        if (walkingLady.active == false)
        {
            walkingLady.SetActive(true);
        }
    }

    void StartFlying()
    {

        switch (currentState)
        {


            case PlaneStates.pullback:
                PullBack();
                break;

            case PlaneStates.taxiout:
                TaxiOut();
                break;

            case PlaneStates.climb:
                Climb();
                break;

            case PlaneStates.fall:
                Fall();
                break;

            case PlaneStates.cruise:

                Cruise();
                break;



        }
    }





    //not used anymore. Kept as reference of ideas from before.
    IEnumerator TakingOffProcess()
    {
        yield return new WaitForSeconds(10);
        // obviously,needs much more work than this.
        /*
         * TODO:
         * Actual goes only from 0 to a maximum rotation, then stops
         * Maybe a better idea would be just rotating a little and keeping it that way until resetting.
         * Rotation needs to be smooth.
         * 
         */

        /*
         * TODO: Divide this method into 3 different ones:
         * Pull back: Plane pulls back, meaning, it goes into reverse for a while.
         * Taxing-out: Plane rotates while moving to get ready for a take off position
         * Climb: Planes starts to go up
         * Cruise: Plane just goes straight until receives a call to go down. This is the MoveForward class right now.
         * This process needs to be entirely sequential, 3 options for that:
         *  1: Multiples waitForSeconds
         *  2: Checking with multiple bools
         *  3: Checking with an enumerator that represents each situation, in a case statement.
        */
        // newRot = Quaternion.Euler(cameraAngles); // get the equivalent quaternion
        // transform.rotation = Quaternion.Slerp(transform.rotation, newRot, speed * Time.deltaTime);

        // MethodTest();

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 30), 2 * Time.deltaTime);
        if (speed < 300)
        {

            Quaternion newPiece = transform.rotation * Quaternion.Euler(0, 5, 0);
            transform.position += new Vector3(0, 1, 0);
            speed++;


        }
        else //if (hasNormalized==false)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, -30), 2 * Time.deltaTime);
            hasNormalized = true;
        }
        yield return new WaitForSeconds(10);
    }


}