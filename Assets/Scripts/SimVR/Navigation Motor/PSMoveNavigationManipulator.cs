using UnityEngine;
using System.Collections;

public class PSMoveNavigationManipulator : MonoBehaviour 
{
	[SerializeField]
	private Motor _motor;
	
	[SerializeField]
	private float _sensibility = 1;
	
    [SerializeField]
    private Vector2 _rotationSensibility;

    [SerializeField]
    private bool _enableJump;
	
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
        if (Input.GetAxis("PS Move Enable Orientation") != 0)
        {
            _motor.relativeRotation = (Vector3.up * Input.GetAxis("Horizontal") * _rotationSensibility.x) + (Vector3.left * Input.GetAxis("Vertical") * _rotationSensibility.y);
            _motor.accelerationForward = 0;
            _motor.accelerationRight = 0;
        }
	 	else
        {
            _motor.accelerationForward = Input.GetAxis("Vertical") * _sensibility;
            _motor.accelerationRight = Input.GetAxis("Horizontal") * _sensibility;

            _motor.relativeRotation = Vector3.zero;
        }
        
		//_motor.accelerationUp = Input.GetAxis( "Vertical Orientation" ) * _sensibility;

		if( Input.GetButton( "Jump" ) && _enableJump )
			_motor.StartJump();
	
		if( Input.GetButtonDown( "Run" ) )
			_motor.SetRunning( true );
		
		if( Input.GetButtonUp( "Run" ) )
			_motor.SetRunning( false );
	}
}