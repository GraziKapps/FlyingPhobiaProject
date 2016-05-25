using UnityEngine;
using System.Collections;

public class LadyWalk : MonoBehaviour {

    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();

        if (animator)
        {
            Vector3 newPosition = transform.position;
            transform.position += transform.forward * animator.GetFloat("movement") * Time.deltaTime;
           }
    }
}
