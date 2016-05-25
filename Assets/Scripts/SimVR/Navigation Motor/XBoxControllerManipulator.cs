using UnityEngine;
using System.Collections;

public class XBoxControllerManipulator : MonoBehaviour 
{
	[SerializeField]
	private Motor _motor;
	
	[SerializeField]
	private float _sensibility = 1;
	
	[SerializeField]
	private float _rotationSensibility = 1;
	
	// Use this for initialization
	void Start ()
	{
		if( _motor == null )
		{
			this.enabled = false;
			return;
		}

		_motor.rotationType = Motor.RotationType.Relative;
	}
	
	// Update is called once per frame
	void Update () 
	{
	 	_motor.accelerationForward = Input.GetAxis( "Vertical" ) * _sensibility;
	 	_motor.accelerationRight = Input.GetAxis( "Horizontal" ) * _sensibility;
		_motor.accelerationUp = Input.GetAxis( "Vertical Orientation" ) * _sensibility;
		
		if( Input.GetButton( "Jump" ) )
			_motor.StartJump();
	
		if( Input.GetButtonDown( "Run" ) )
			_motor.SetRunning( true );
		
		if( Input.GetButtonUp( "Run" ) )
			_motor.SetRunning( false );
	
		_motor.relativeRotation = Vector3.up * Input.GetAxis( "Horizontal Orientation" ) * _rotationSensibility;
	}
}