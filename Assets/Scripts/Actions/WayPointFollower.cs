using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class WayPointFollower : MonoBehaviour
    {
        private Transform _nextWayPoint;
        private Rigidbody2D _rigidBody;
        private int _wayPointNumber;

        public float Speed;

        public float WayPointReachedDistance;
        public List<Transform> WayPoints;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Flight();
        }

        private void Start()
        {
            _nextWayPoint = WayPoints[_wayPointNumber];
        }

        private void Flight()
        {
            var directionToWayPoint = (_nextWayPoint.position - transform.position).normalized;
            var distance = Vector2.Distance(_nextWayPoint.position, transform.position);

            _rigidBody.velocity = directionToWayPoint * Speed;

            if (distance <= WayPointReachedDistance)
            {
                UpdateDirection();
                _wayPointNumber++;
                if (_wayPointNumber >= WayPoints.Count)
                    _wayPointNumber = 0;

                _nextWayPoint = WayPoints[_wayPointNumber];
            }
        }

        private void UpdateDirection()
        {
            if (transform.localScale.x > 0)
            {
                //face right
                if (_rigidBody.velocity.x <= 0)
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                        transform.localScale.z);
            }
            else
            {
                //face left
                if (_rigidBody.velocity.x >= 0)
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                        transform.localScale.z);
            }
        }
    }
}