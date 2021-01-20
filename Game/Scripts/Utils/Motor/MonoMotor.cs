using UnityEngine;

namespace OregoFramework.Util
{
    public sealed class MonoMotor : MonoBehaviour
    {
        [Header("Speed")]
        [SerializeField]
        public float maxSpeed;

        [SerializeField]
        public float currentSpeed;
        
        [SerializeField]
        public float minSpeed;

        [Header("Acceleration")]
        [SerializeField]
        public float maxAcceleration;
        
        [SerializeField]
        public float currentAcceleration;

        [Header("Drag")]
        [SerializeField]
        public float linearDrag;

        public void AddForce(float coefficient)
        {
            this.currentAcceleration += this.maxAcceleration * coefficient;
            if (this.currentAcceleration.Abs() >= this.maxAcceleration)
            {
                var accelerationSign = this.currentAcceleration.Sign();
                this.currentAcceleration = accelerationSign * this.maxAcceleration;
            }
        }

        public float NextSpeed()
        {
            this.currentSpeed += this.currentAcceleration;
            if (this.currentSpeed.Abs() >= this.maxSpeed)
            {
                this.currentSpeed = this.currentSpeed.Sign() * this.maxSpeed;
            }

            if (this.currentAcceleration.Abs() <= Float.MIN_TOLERANCE)
            {
                var speedSign = this.currentSpeed.Sign();
                if (this.currentSpeed.Abs() - this.linearDrag > this.minSpeed)
                {
                    this.currentSpeed -= speedSign * this.linearDrag;
                }
                else
                {
                    this.currentSpeed = speedSign * this.linearDrag;
                }
            }

            this.currentAcceleration = Float.ZERO;
            return this.currentSpeed;
        }

        public void Reset()
        {
            this.currentAcceleration = Float.ZERO;
            this.currentSpeed = Float.ZERO;
        }
    }
}