using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour
{
    public enum RotationType
    {
        Relative,
        AbsoluteVec3,
        AbsoluteQuat,
        Hybrid
    }

    public enum MoveType
    {
        Walk,
        Fly,
        Rift,
        Path
    }

    private CharacterController _controller;

    [SerializeField]
    private MoveType _moveMode = MoveType.Fly;

    [SerializeField]
    private bool _move = true;
    [SerializeField]
    private bool _rotate = true;

    // Transform da camera para realizar o fly.
    [SerializeField]
    private Transform _cameraTransform;

    // Move
    [System.NonSerialized]
    public float accelerationForward = 0;
    [System.NonSerialized]
    public float accelerationRight = 0;
    [System.NonSerialized]
    public float accelerationUp = 0;

    [SerializeField]
    private float _speed = 6;
    [SerializeField]
    private float _runSpeed = 10;
    [SerializeField]
    private float jumpSpeed = 8.0f;
    [SerializeField]
    private float _angularSpeed = 10.0f;
    [SerializeField]
    private float _gravityForce = 20.0f;

    [SerializeField]
    private bool _strafe = true;

    private Vector3 moveDirection = Vector3.zero;
    private bool _jumping = false;
    private bool _running = false;


    // Rotation
    public Vector3 _rotateAxis = Vector3.zero;

    [System.NonSerialized]
    public RotationType rotationType;

    [System.NonSerialized]
    public Vector3 relativeRotation;

    [System.NonSerialized]
    public Quaternion absoluteRotationQuat;

    [System.NonSerialized]
    public Vector3 absoluteRotationVec3;

    // Ladders
    [SerializeField]
    private Collider _currentLadder = null;
    //private LadderComponent.LadderComponentType _entranceType;

    private bool  _transition = false;

    [SerializeField]
    private float _animationSpeed = 0.6f;

    private Vector3 _initialPosition;
    private Vector3 _finalPosition;

    private Quaternion _initialRotation;
    private Quaternion _finalRotation;

    private float _movePercent = 0.0f;
    private bool _onLadderJump = false;

    private Vector3 _ladderDirection = Vector3.zero;

    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    public float AngularSpeed
    {
        get
        {
            return _angularSpeed;
        }
        set
        {
            _angularSpeed = value;
        }
    }

    public Vector3 MoveDirection
    {
        get
        {
            return moveDirection;
        }
    }

    public string CharacterState
    {
        get
        {
            if( _controller == null )
                return "Flying";

            if( !_controller.isGrounded )
                return "Jumping";

            if( _controller.isGrounded && (moveDirection.x != 0 || moveDirection.z != 0) )
            {
                if( _running )
                    return "Running";
                return "Walking";
            }

            return "Idle";
        }
    }

    // Use this for initialization
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        //if( !_move )
        //    _controller.enabled = false;
    }

    private void Start()
    {
        absoluteRotationVec3 = transform.localRotation.eulerAngles;
        absoluteRotationQuat = transform.localRotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if( _transition )
            Transition();
        else
        {
            if( _move )
                Move();

            if( _rotate )
                Rotate();
        }
    }

    private void Transition()
    {

        if( _movePercent <= 1.0f )
        {
            _movePercent += (Time.deltaTime * _animationSpeed);
            transform.position = Vector3.Lerp( _initialPosition, _finalPosition, _movePercent );
            transform.rotation = Quaternion.Lerp( _initialRotation, _finalRotation, _movePercent );
        }
        else
        {
            _transition = false;
            _movePercent = 0.0f;
        }
    }

    public void MakeTransition( Transform target )
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        _finalPosition = target.position;
        _finalRotation = target.rotation;

        _transition = true;
    }

    public void ResetRotation()
    {
        transform.localRotation = Quaternion.identity;
        absoluteRotationVec3 = transform.localRotation.eulerAngles;
        absoluteRotationQuat = transform.localRotation;
    }

    private void Move()
    {
        if( _moveMode == MoveType.Walk ) //Walk - gravidade e colisão
        {
            if( _currentLadder != null )
            {
                //moveDirection = _currentLadder.transform.up * accelerationForward * _speed;
                moveDirection = _ladderDirection * accelerationForward * ((_running) ? _runSpeed : _speed);

                if( _jumping )
                {
                    _currentLadder = null;
                    _jumping = false;
                    _onLadderJump = true;
                }
            }
            else if( _controller != null && _controller.isGrounded )
            {
                if( _strafe )
                    moveDirection = new Vector3( accelerationRight, 0, accelerationForward );
                else
                    moveDirection = new Vector3( 0, 0, accelerationForward );

                moveDirection = transform.TransformDirection( moveDirection );
                moveDirection *= ((_running) ? _runSpeed : _speed);

                if( _jumping )
                {
                    moveDirection.y = jumpSpeed;
                    _jumping = false;
                }
            }

            if( _currentLadder == null )
                moveDirection.y -= _gravityForce * Time.deltaTime;

            _controller.Move( moveDirection * Time.deltaTime );
        }
        else if( _moveMode == MoveType.Rift )
        {
            if( _controller != null && _controller.isGrounded )
            {
                if( _strafe )
                    moveDirection = new Vector3( accelerationRight, 0, accelerationForward );
                else
                    moveDirection = new Vector3( 0, 0, accelerationForward );

                moveDirection = _cameraTransform.TransformDirection( moveDirection );
                moveDirection *= ((_running) ? _runSpeed : _speed);

                if( _jumping )
                {
                    moveDirection.y = jumpSpeed;
                    _jumping = false;
                }
            }

            if( _currentLadder == null )
                moveDirection.y -= _gravityForce * Time.deltaTime;

            _controller.Move( moveDirection * Time.deltaTime );
        }
        else if( _moveMode == MoveType.Path )
        {
            moveDirection = new Vector3( 0, 0, accelerationForward );

            moveDirection *= ((_running) ? _runSpeed : _speed);
        }
        else //Fly
        {
            moveDirection = new Vector3( accelerationRight, accelerationUp, accelerationForward );
            moveDirection = transform.TransformDirection( moveDirection );

            moveDirection *= ((_running) ? _runSpeed : _speed);

            transform.Translate( moveDirection * Time.deltaTime, _cameraTransform );
        }
    }

    private void Rotate()
    {
        if( _strafe )
        {
            switch( rotationType )
            {
                case RotationType.AbsoluteQuat:
                    transform.localRotation = absoluteRotationQuat;
                    break;

                case RotationType.AbsoluteVec3:
                    transform.localEulerAngles = absoluteRotationVec3;
                    absoluteRotationVec3 = transform.localEulerAngles;
                    break;

                case RotationType.Relative:
                    transform.Rotate( relativeRotation );
                    break;

                case RotationType.Hybrid:
                    transform.localRotation = absoluteRotationQuat;
                    transform.Rotate( relativeRotation );
                    break;

                default:
                    break;
            }
        }
        else
        {
            transform.Rotate( transform.up * accelerationRight * _angularSpeed );
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        //		if( _onLadderJump )
        //		{
        //			_onLadderJump = false;
        //			return;
        //		}
        //		
        //		if( _move )
        //		{
        //			LadderComponent ladderComponent = other.gameObject.GetComponent<LadderComponent>();
        //			if( ladderComponent != null )
        //			{
        //				if( _currentLadder != null )
        //				{
        //					_currentLadder = null;
        //										
        //					_initialPosition = transform.position;
        //					_initialRotation = transform.rotation;
        //					
        //					ladderComponent.GetExitPoint(_controller, out _finalPosition, out _finalRotation);	
        //					
        //					_transition = true;
        //					_movePercent = 0.0f;
        //					
        //				}
        //				else
        //				{
        //					_currentLadder = other;
        //					_ladderDirection = ladderComponent.GetDirection();
        //					
        //					_initialPosition = transform.position;
        //					_initialRotation = transform.rotation;
        //					ladderComponent.GetEnterPoint(_controller, out _finalPosition,out _finalRotation);
        //						
        //					_transition = true;
        //					_movePercent = 0.0f;					
        //				}
        //			}
        //		}
    }

    public void StartJump()
    {
        if( _controller != null && ( _controller.isGrounded || _currentLadder != null ) )
            _jumping = true;
    }

    public void SetRunning( bool state )
    {
        _running = state;
    }

    public bool GetTransitionState()
    {
        return _transition;
    }

    public MoveType MoveMode
    {
        get
        {
            return this._moveMode;
        }
    }

    public void SetMode( MoveType mode )
    {
        _moveMode = mode;

        if( _moveMode == MoveType.Fly )
        {
            // Setar _currentLadder para null ao mudar para o modo fly.
            // Caso contrário, ao mudar para o fly em uma escada e depois voltar para o modo
            // walk, o avatar vai se movimentar apenas na vertical.
            _currentLadder = null;
            _jumping = false;
        }
    }

    public void AllowMove( bool value )
    {
        _move = value;
    }

    public void AllowRotate( bool value )
    {
        _rotate = value;
    }
}
