using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private float DamageThreshold = 5.0f;
    [SerializeField] private float DamageCoef = 100;
    [SerializeField] private WheelJoint2D MotorWheel1;
    [SerializeField] private WheelJoint2D MotorWheel2;

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

    [SerializeField] private float timerMinute;
    [SerializeField] private float timerSecond;
    [SerializeField] private Text textTimer;
    
    private void Start()
    {
        if (inputManager == null) inputManager = FindObjectOfType<InputManager>();
        body = GetComponent<Rigidbody2D>();
        motor = MotorWheel1.motor;
        textTimer.text = timerMinute.ToString() + ':' + timerSecond.ToString();
    }

    private void CounterTimer()
    {
        timerSecond -= Time.deltaTime;
        if (timerSecond <= -1)
        {
            timerSecond = 59;
            timerMinute -= 1;
        }
        textTimer.text = timerMinute.ToString() + ':' + Mathf.Round(timerSecond).ToString();
    }

    private void Update()
    {
        lastVelosityVector = body.velocity;
        if (timerMinute <= 0 && timerSecond <= 0)
        {
            Debug.LogError("Pashel NAXYI");
            return;
        }
        CounterTimer();
        
    }
    private void FixedUpdate()
    {
        SetSpeedometrValue?.Invoke($"{ body.velocity} | body.mass");
        motor.motorSpeed = Speed / body.mass * inputManager.inputVector*1000;
        MotorWheel2.motor = MotorWheel1.motor = motor;
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
