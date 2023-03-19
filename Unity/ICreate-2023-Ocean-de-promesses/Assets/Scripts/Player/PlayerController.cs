using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rb;

        [field: HideInInspector] public Vector3 MovementInput { get; set; }

        [field: HideInInspector] public Vector3 LookDirection { get; set; }

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
            Vector3 desiredVelocity =
                MovementInput * maxSpeed;

            Vector3 maxVelocityChange = new Vector3(
                GetAcceleration(_rb.velocity.x, desiredVelocity.x),
                0f,
                GetAcceleration(_rb.velocity.z, desiredVelocity.z)
            ) * Time.fixedDeltaTime;

            Vector3 newVelocity = new Vector3(
                Mathf.MoveTowards(_rb.velocity.x, desiredVelocity.x, maxVelocityChange.x),
                0f,
                Mathf.MoveTowards(_rb.velocity.z, desiredVelocity.z, maxVelocityChange.z)
            );
        
            _rb.velocity = newVelocity;

            LookDirection = MovementInput;
            
            if (LookDirection != Vector3.zero)
                transform.LookAt(transform.position + LookDirection);
        }
    }
}