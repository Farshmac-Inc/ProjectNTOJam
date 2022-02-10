using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1.0f;
    public float DamageThreshold = 5.0f;
    public float DamageCoef = 100;
    public UnityEngine.UI.Text speedometr;
    public WheelJoint2D MotorWheel;

    private Rigidbody2D body;
    private JointMotor2D motor;
    private Vector2 lastVelosityVector = new Vector2();

    private bool onUnloading = false;
    private Country country;

    public UnityEvent<string> SetSpeedometrValue;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        motor = MotorWheel.motor;
    }

    private void Update()
    {
        lastVelosityVector = body.velocity;
        var a = body.mass += Input.GetAxis("Vertical") / 10;
        body.mass = a < 0.1f ? 0.1f : a > 10 ? 10 : a;
        SetSpeedometrValue?.Invoke(body.velocity.ToString() + " | " + body.mass);
        motor.motorSpeed = Speed / body.mass * Input.GetAxis("Horizontal");
        MotorWheel.motor = motor;
    }


    private void SetCargoMass(float value)
    {
        var a = body.mass - value;
        body.mass = a < 0.5f ? 0.5f : a;
    }
    public void CollisionCar(Collision2D collision)
    {
        if (lastVelosityVector.y < -DamageThreshold)
        {
            SetCargoMass(body.velocity.y * body.velocity.y * body.mass / 2 / DamageCoef);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) => CollisionCar(collision);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        country = collision.GetComponent<Country>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (country == null) country = collision.GetComponent<Country>();
        else
        {
            if (onUnloading = false && Mathf.Abs(body.velocity.x) < 0.1f)
            {
                country.UploadingState(onUnloading, this);
                onUnloading = true;
            }
            if (onUnloading = true && Mathf.Abs(body.velocity.x) >= 0.1f)
            {
                country.UploadingState(onUnloading, this);
                onUnloading = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onUnloading = false;
        country.UploadingState(onUnloading, this);
        country = null;
    }
}
