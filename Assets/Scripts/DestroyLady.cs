using UnityEngine;
using System.Collections;

public class DestroyLady : MonoBehaviour {

	// Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ha");
        if (other.tag == "Lady")
        {
            Destroy(other.gameObject);
            Debug.Log("Destroyed");
        }
    }
}
