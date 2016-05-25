using UnityEngine;

public class ResetYBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 currentPosition = animator.transform.position;
        InitialPosition initial = animator.transform.GetComponent<InitialPosition>();

        animator.transform.position = new Vector3(currentPosition.x, initial.Position.y, currentPosition.z);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exit");
    }
}
