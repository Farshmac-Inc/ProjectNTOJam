using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
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

    [SerializeField] private Sprite carLow;
    [SerializeField] private Sprite carMiddle;
    [SerializeField] private Sprite carHard;
    
    private void Start()
    {
        if (inputManager == null) inputManager = FindObjectOfType<InputManager>();
        body = GetComponent<Rigidbody2D>();
        motor = MotorWheel.motor;
    }
    private void Update()
    {
        lastVelosityVector = body.velocity;
    }
    private void FixedUpdate()
    {
        SetSpeedometrValue?.Invoke($"{ body.velocity} | body.mass");
        motor.motorSpeed = Speed / body.mass * inputManager.inputVector;
        MotorWheel.motor = motor;
    }

    private void SetCargoMass(float value)
    {
        var a = body.mass - value;
        body.mass = a <= MinMass ? MinMass : a;
        if (body.mass <= 2.5 && body.mass > 1)
            GetComponent<SpriteRenderer>().sprite = carMiddle;
        else if (body.mass <= 1)
            GetComponent<SpriteRenderer>().sprite = carLow;
        else if (body.mass >2.5)
            GetComponent<SpriteRenderer>().sprite = carHard;
    }
    private void SetCargoMass(float value, Sprite carSprite)
    {
        var a = body.mass - value;
        body.mass = a <= MinMass ? MinMass : a;
        GetComponent<SpriteRenderer>().sprite = carSprite;
    }
    public void CollisionCar(Collision2D collision)
    {
        if (lastVelosityVector.y < -DamageThreshold)
        {
            SetCargoMass(body.velocity.y * body.velocity.y * body.mass / 2 / DamageCoef);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionCar(collision);
        if (collision.gameObject.layer == 11)
        {
            var isObstacle = collision.gameObject.TryGetComponent<ObstacleDamage>(out var obstacleCurrent);
            float damage = isObstacle ? obstacleCurrent.GetDamage() : 0;
            SetCargoMass(damage);
        }   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out NeedStopObject needStopObject)) return;
        if (collision.TryGetComponent(out Country country)) country.UnloadingFinish.AddListener(SetCargoMass);
        SetUploadingState.AddListener(needStopObject.UnloadingState);
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

    public void Death()
    {
        Debug.Log("Opps Mazafaka");
    }
}
