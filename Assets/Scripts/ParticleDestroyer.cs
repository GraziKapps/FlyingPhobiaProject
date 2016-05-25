using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {
    //NOT USED.
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnParticleCollision(GameObject other)
    {
        MeshCollider body = other.GetComponent<MeshCollider>();
        if (body)
        {
            Debug.Log("Colisao!");

        }
    }
}
