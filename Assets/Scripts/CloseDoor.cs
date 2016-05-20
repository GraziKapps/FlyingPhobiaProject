using UnityEngine;
using System.Collections;

//Name says it all: Close the plane door after entering.
public class CloseDoor : MonoBehaviour
{

    // Use this for initialization
    Collider thisCollider;
    MeshRenderer thisMesh;
    public Collider interiorCollider;
    
    void Start()
    {

        thisCollider = transform.GetComponent<Collider>();
        thisMesh = transform.GetComponent<MeshRenderer>();
        //  interiorCollider = transform.GetComponent<Collider>();

    }
    

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Aqui estou!");
    }

    //After passing through the door..
    void OnTriggerExit(Collider other)
    {
        
        StartCoroutine(TimerWait());
        Debug.Log("Passei da porta");

        
        // Destroy everything that leaves the trigger
        //   Destroy(other.gameObject);
    }

    IEnumerator TimerWait() 
    {
        //Wait a little to give the player time to move and not close the door with him inside it. Then, just enable the collider and the renderer.
        Debug.Log("WAITING");
        yield return new WaitForSeconds(2);
        thisCollider.isTrigger = false;
        thisMesh.enabled = true;
        thisMesh.GetComponent<MeshCollider>().enabled = true;
        thisMesh.GetComponent<BoxCollider>().enabled = false;
        foreach(Transform child in transform)
        {
            child.GetComponent<MeshRenderer>().enabled = true;
           //// child.GetComponent<MeshCollider>().enabled = true;
            
        }
//        interiorCollider.enabled = true;
    }
}
