using UnityEngine;
using System.Collections;

public class KeyboardManipulator : MonoBehaviour 
{
	[SerializeField]
	private Motor _motor;
	
	[SerializeField]
	private float _sensibility = 1;
	
	// Use this for initialization
	void Start ()
	{
		if( _motor == null )
			this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	 	_motor.accelerationForward = Input.GetAxis( "Vertical" ) * _sensibility;
	 	_motor.accelerationRight = Input.GetAxis( "Horizontal" ) * _sensibility;
	 	_motor.accelerationUp = Input.GetAxis( "Up" ) * _sensibility;
		
		if( Input.GetKey( KeyCode.Space ) )
			_motor.StartJump();
	
		if( Input.GetKeyDown( KeyCode.LeftShift ) )
			_motor.SetRunning( true );
		
		if( Input.GetKeyUp( KeyCode.LeftShift ) )
			_motor.SetRunning( false );
	
	}
}
