using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TankMovement : MonoBehaviour
{
    [Header("Tank Movement Attributes")]
    [SerializeField]private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float hullTraverseSpeed;
    [SerializeField] private TextMeshProUGUI engineStatus;
    [SerializeField] private Slider engineSlider;

    private Rigidbody2D rb;
    
    //Player's Input//
    private float xAxis;
    private float yAxis;
    //

    private bool isEngineOn = false;
    private TankStatus tankStatus;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tankStatus = GetComponent<TankStatus>();
    }
    void Update()
    {
        HandleInput();
    }
    private void FixedUpdate()
    {
        if (isEngineOn && tankStatus.CanMove()) 
        {
            HullRotation(xAxis);
            HullForwardMovement();
        }
        EngineSettings();
    }
    private void HandleInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
    }
    private void HullRotation(float rotationInput)
    {
        float rotationAmount = -rotationInput * hullTraverseSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount);
    }
    private void HullForwardMovement()
    {
        if (tankStatus.IsLeftTrackHit())
        {
            LeftDamageTrack();
        }
        else if (tankStatus.IsRightTrackHit())
        {
           RightDamageTrack();
        }
        else
        {
           NoDamageTrack();
        }
    }
    private void LeftDamageTrack()
    {
        //right track is only the operational
        if (yAxis == 1)
        {
            //w key is held
            transform.Rotate(Vector3.forward * hullTraverseSpeed * Time.deltaTime);
        }
        else if (yAxis == -1)
        {
            //s key is held
            transform.Rotate(Vector3.back * hullTraverseSpeed * Time.deltaTime);
        }
    }
    private void RightDamageTrack()
    {
        //left track is only the operational
        if (yAxis == 1)
        {
            //w key is held
            transform.Rotate(Vector3.back * hullTraverseSpeed * Time.deltaTime);
        }
        else if (yAxis == -1)
        {
            //s key is held
            transform.Rotate(Vector3.forward * hullTraverseSpeed * Time.deltaTime);
        }
    }
    private void NoDamageTrack()
    {
        //both tracks are operational
        Vector2 moveDirection = transform.up * yAxis;
        moveDirection.Normalize();
        Vector2 accelerationVector = acceleration * Time.deltaTime * moveDirection;
        rb.AddForce(accelerationVector);

        //speed limit//
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        //slowing down due to no input//
        else if (yAxis == 0)
        {
            Vector2 slowDown = acceleration * Time.deltaTime * -rb.velocity.normalized;
            rb.AddForce(slowDown);

            //full stop//
            if (rb.velocity.magnitude < 0.1f)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    private void EngineSettings()
    {
        if(isEngineOn)
        {
            engineStatus.text = "Engine is ON / Hold E to Stop";
            //turning off//
            if(Input.GetKey(KeyCode.E))
            {
                engineSlider.value -= 1;
                if(engineSlider.value == 0)
                {
                    isEngineOn = false;
                }
            }
            else
            {
                engineSlider.value += 1;
            }
        }
        else if(!isEngineOn)
        {
            engineStatus.text = "Engine is OFF / Hold E to Start";
            //turning on//
            if (Input.GetKey(KeyCode.E))
            {
                engineSlider.value += 1;
                if(engineSlider.value == 100)
                {
                    isEngineOn = true;
                }
            }
            else
            {
                engineSlider.value -= 1;
            }
        }
    }
}

