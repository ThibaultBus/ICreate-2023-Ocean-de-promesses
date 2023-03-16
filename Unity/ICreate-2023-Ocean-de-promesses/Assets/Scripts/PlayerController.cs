using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rb;

        [field: HideInInspector] public Vector2 MovementInput { get; set; }

        [field: HideInInspector] public Vector2 LookDirection { get; set; }

        [SerializeField] private float directionOffset = 5f;
    
        // Movement Variables
        [SerializeField, Range(0f, 50f)]
        private float maxSpeed = 10f;
        [SerializeField, Range(0f, 100f)]
        public float maxAcceleration = 30f;
        [SerializeField, Range(0f, 100f)]
        public float maxDeceleration = 40f;
        [SerializeField, Range(0f, 100f)]
        public float turnAcceleration = 50f;
    
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
    
        private float GetAcceleration(float previousVelocity, float desiredVelocity)
        {
            if (previousVelocity * desiredVelocity < 0)
            {
                return turnAcceleration;
            }
            else if (Mathf.Abs(previousVelocity) < Mathf.Abs(desiredVelocity))
            {
                return maxAcceleration;
            }
            else
            {
                return maxDeceleration;
            }
        }
    
        private void FixedUpdate()
        {
            Vector2 desiredVelocity =
                MovementInput * maxSpeed;

            Vector2 maxVelocityChange = new Vector2(
                GetAcceleration(_rb.velocity.x, desiredVelocity.x),
                GetAcceleration(_rb.velocity.y, desiredVelocity.y)
            ) * Time.fixedDeltaTime;

            Vector2 newVelocity = new Vector2(
                Mathf.MoveTowards(_rb.velocity.x, desiredVelocity.x, maxVelocityChange.x),
                Mathf.MoveTowards(_rb.velocity.y, desiredVelocity.y, maxVelocityChange.y)
            );
        
            _rb.velocity = newVelocity;

            LookDirection = MovementInput;

            float angle = Vector2.SignedAngle(Vector2.right, LookDirection) + directionOffset;

            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}