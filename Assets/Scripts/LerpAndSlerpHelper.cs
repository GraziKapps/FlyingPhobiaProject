using UnityEngine;
using System.Collections;
using System;


public class LerpAndSlerpHelper //NOT A MONO BEHAVIOUR, NEEDS TO BE CALLED
{
    
    
    public struct HelperReturn
    {
        public HelperReturn(Vector3 returnPosition, float percentageComplete, Quaternion returnRotation)
        {
            this.returnPosition = returnPosition;
            this.percentageComplete = percentageComplete;
            this.returnRotation = returnRotation;
        }
        public  Vector3 returnPosition { get;  private set; }
        public  float percentageComplete { get; private set; }
        public  Quaternion returnRotation { get; set; }
    }

    /// <summary>
    /// The time taken to move from the start to finish positions
    /// </summary>
    public float timeTakenDuringLerp = 1f;
 
    /// <summary>
    /// How far the object should move when 'space' is pressed
    /// </summary>
    public float distanceToMove = 10;
 
    //Whether we are currently interpolating or not
    private bool _isLerping;
 
    //The start and finish positions for the interpolation
    private Vector3 _startPosition;
    private Vector3 _endPosition;
 
    //The Time.time value when we started the interpolation
    private float _timeStartedLerping;
    private  bool _isSlerping;
    private Vector3 positionNow;
    private Quaternion rotationNow;
    float percentageComplete;
    private  Quaternion quaternionNow;
    private Quaternion _quaternionStart;
    private Quaternion _quaternionEnd;
 
    /// <summary>
    /// Called to begin the linear interpolation
    /// </summary>
    /// 

    public void LeprAndSlerpHelperStarter(bool _isLerping, bool _isSlerping, float distanceToMove, Vector3 _startPosition, Vector3 _endPosition)
    {
        this._isLerping = _isLerping;
        this._isSlerping =_isSlerping;
        _timeStartedLerping = Time.time;
 
        //We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
        //if(_isLerping){
            this._startPosition = _startPosition;
            this._endPosition = _endPosition + Vector3.forward*distanceToMove;
        //}

           

    }
 
    //void Update()
    //{
    //    //When the user hits the spacebar, we start lerping
    //    if(Input.GetKey(KeyCode.Space))
    //    {
    //        StartLerping();
    //    }
    //}
 
    //We do the actual interpolation in FixedUpdate(), since we're dealing with a rigidbody
    public HelperReturn FixedUpdate()
    {
        if(_isLerping)
        {
            //We want percentage = 0.0 when Time.time = _timeStartedLerping
            //and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
            //In other words, we want to know what percentage of "timeTakenDuringLerp" the value
            //"Time.time - _timeStartedLerping" is.
            float timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / timeTakenDuringLerp;
 
            //Perform the actual lerping.  Notice that the first two parameters will always be the same
            //throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
            //to start another lerp)
             positionNow= Vector3.Lerp (_startPosition, _endPosition, percentageComplete);
 
            //When we've completed the lerp, we set _isLerping to false
            if(percentageComplete >= 1.0f)
            {
                _isLerping = false;
            }
            return new HelperReturn(positionNow, percentageComplete,Quaternion.identity);
        }

        if (_isSlerping)
        {
            //We want percentage = 0.0 when Time.time = _timeStartedLerping
            //and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
            //In other words, we want to know what percentage of "timeTakenDuringLerp" the value
            //"Time.time - _timeStartedLerping" is.
            float timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            //Perform the actual lerping.  Notice that the first two parameters will always be the same
            //throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
            //to start another lerp)
            quaternionNow = Quaternion.Slerp(_quaternionStart,_quaternionEnd,percentageComplete);

            //When we've completed the lerp, we set _isLerping to false
            if (percentageComplete >= 1.0f)
            {
                _isLerping = false;
            }
            return new HelperReturn(Vector3.zero, percentageComplete, quaternionNow);
        }
        else return new HelperReturn(Vector3.zero,0,quaternionNow);
        
        
    }
}

