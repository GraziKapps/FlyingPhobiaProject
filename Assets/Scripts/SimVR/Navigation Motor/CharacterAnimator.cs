using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    private Motor _motor;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _animationSpeed = 1;

    private void Update()
    {
        _animator.SetBool("Grounded", _motor.GetComponent<CharacterController>().isGrounded);
        _animator.SetFloat("Speed", _motor.Speed * _motor.accelerationForward * _animationSpeed);
    }
}