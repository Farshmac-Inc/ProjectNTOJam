using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private float DamageThreshold = 5.0f;
    [SerializeField] private float DamageCoef = 100;
    [SerializeField] private WheelJoint2D MotorWheel;

    [SerializeField] private UnityEvent<string> SetSpeedometrValue;
    [SerializeField] private UnityEvent<bool> SetUploadingState;

    private bool onUnloading = false;
    private Rigidbody2D body;
    private JointMotor2D motor;
    private Vector2 lastVelosityVector = new Vector2();

    private float MinMass = 0.5f;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        motor = MotorWheel.motor;
    }
    private void Update()
    {
        lastVelosityVector = body.velocity;
        SetSpeedometrValue?.Invoke($"{ body.velocity} | body.mass");
        motor.motorSpeed = Speed / body.mass * Input.GetAxis("Horizontal");
        MotorWheel.motor = motor;
    }

    private void SetCargoMass(float value)
    {
        var a = body.mass - value;
        body.mass = a < MinMass ? MinMass : a;
    }
    private void SetCargoMass(float value, Sprite carSprite)
    {
        var a = body.mass - value;
        body.mass = a < MinMass ? MinMass : a;
        GetComponent<SpriteRenderer>().sprite = carSprite;
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
        if (!collision.TryGetComponent(out Country _country)) return;
        _country.UnloadingFinish.AddListener(SetCargoMass);
        SetUploadingState.AddListener(_country.UnloadingState);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (onUnloading == false && Mathf.Abs(body.velocity.x) < 0.1f)
        {
            onUnloading = true;
            SetUploadingState?.Invoke(onUnloading);
        }
        if (onUnloading == true && Mathf.Abs(body.velocity.x) >= 0.1f)
        {
            onUnloading = false;
            SetUploadingState?.Invoke(onUnloading);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onUnloading = false;
        SetUploadingState?.Invoke(onUnloading);
    }
}
