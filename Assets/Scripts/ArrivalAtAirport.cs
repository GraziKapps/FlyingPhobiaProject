using UnityEngine;
using System.Collections;
using System;

public class ArrivalAtAirport : MonoBehaviour {

	// Use this for initialization

    /*The idea behind this script: Create a way to make the plane actually finish its course, for that we need to:
     * 1: Make the plane go down (duh)
     * 2: Create a new airport?? Since flight times can vary, the arrival airport can't be exactly on the same place OR only the clouds in the sky would move, the plane being kept 
     * on the same place. Open to new ideas.
     * 3: By following the create path, I have save the airport construction as a prefab.
     * 4: This script will activate its actual behavior by a timer or by user input.
     */
    [SerializeField]
    private GameObject airport;
    [SerializeField]
    private float airportHeight;

    private Vector3 airportPosition;
	void Start () {
	    
	}
    //252.0382- x
    //-198.0233
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.C))
        {
            airportPosition = new Vector3(transform.position.x, airportHeight, transform.position.z);
            Instantiate(airport, airportPosition, Quaternion.identity);
            //start process.
            //create a new aiport...?
            Debug.Log("???");
            if(transform.position.y<=-196)
            {
                Debug.Log("WUWIWUIW");
                throw new Exception();

            }
        }
	
	}
}
