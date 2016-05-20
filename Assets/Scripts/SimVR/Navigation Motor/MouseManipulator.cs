//using System;
using UnityEngine;
using System.Collections;


public class MouseManipulator : MonoBehaviour
{
	
	[SerializeField]
	private Motor _motor;
	
	[SerializeField]
	private float _sensibilityX = 15;
	
	[SerializeField]
	private float _sensibilityY = 15;
	
	[SerializeField]
	private Vector3 MovementAxis;
	
//	[SerializeField]
//	private float minimumX = -360F;
//	
//	[SerializeField]
//	private float maximumX = 360F;
	
	[SerializeField]
	private float minimumY = -60F;
	
	[SerializeField]
	private float maximumY = 60F;
	
	[SerializeField]
	private float rotationY = 0F;
	
	[SerializeField]
	private float rotationX = 0F;
	
	public enum MLCE_MouseButton
	{
		Left,
		Right,
		Middle
	}
	
	[SerializeField]
	private MLCE_MouseButton _mouseButton = MouseManipulator.MLCE_MouseButton.Right;
	
	void Start ()
	{
		if( _motor == null )
			this.enabled = false;
		
		_motor.rotationType = Motor.RotationType.AbsoluteVec3;
		
	}
	
	void Update () 
	{
		if( Input.GetMouseButton( (int)_mouseButton ) || _motor.GetTransitionState() )
		{
			_motor._rotateAxis = MovementAxis;
			rotationX = ( transform.localEulerAngles.y + Input.GetAxis( "Mouse X" ) * _sensibilityX ) * _motor._rotateAxis.x;
			
			rotationY += ( Input.GetAxis( "Mouse Y" ) * _sensibilityY ) * _motor._rotateAxis.y;
			rotationY = Mathf.Clamp( rotationY, minimumY, maximumY );
			
			_motor.absoluteRotationVec3 = new Vector3( -rotationY, rotationX, 0 );
			
			//Debug.Log(" Axis = {"+Input.GetAxis("Mouse X")+","+Input.GetAxis("Mouse Y")+"}");
			//Debug.Log(" Rot = {"+rotationX+","+rotationY+"}");
		}
	}
}


