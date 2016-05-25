using UnityEngine;
using System.Collections;

public class AddColliders : MonoBehaviour {

    //Add colliders for every set of chairs in the scene
    public GameObject[] seats;
	// Use this for initialization
	void Start () {
        //All seats are tagged as Seat in the scene
        seats = GameObject.FindGameObjectsWithTag("Seat");

        foreach(GameObject s in seats)
        {  
            BoxCollider box;
            //GameObject child = new GameObject();
            //Vector3 boxSize, boxCenter;
            //float x, y, z;
            //bool firstParse = false;
            //child.transform.parent = s.transform;
            
            //:(
            //Designer didn't add a mesh renderer to every seat group so this if is need as a workaround.
            if (s.GetComponent<MeshRenderer>() != null)
            {
                
                box = s.AddComponent<BoxCollider>() as BoxCollider;
                //if(!firstParse)
                //{
                //    boxSize = box.size;
                //    boxCenter = box.center;
                //    firstParse = true;
                //}
            }
            else
            {
                box = s.AddComponent<BoxCollider>() as BoxCollider;

                box.size = new Vector3(1.5f, 0, 1);
                box.center = new Vector3(0,0,0);
              //  box.size = new Vector3(1574.414f, 692.5975f, 1127.09f);
             //   box.center = new Vector3(4.368685f, 4.577638e-05f, -1.08353e-11f);
            }
            box.size = new Vector3(1.5f, 0, 1);
            box.center = new Vector3(0, 0, 0);
          // box.size =  new Vector3 (box.size.x,300,box.size.z);
          // box.center = new Vector3(4.368685f, 200, -1.08353e-11f);
         
        }
	
	}
	
    //Does this even need to be a monobehaviour?
	void Update () {
	
	}
}
