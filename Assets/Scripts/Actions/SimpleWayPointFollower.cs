using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class SimpleWayPointFollower : MonoBehaviour
    {
        private int _currentWayPointIndex;

        public GameObject[] WayPoints;
        public float Speed = 2f;

        void FixedUpdate()
        {
            if (Vector2.Distance(WayPoints[_currentWayPointIndex].transform.position, transform.position) < .1f)
            {
                _currentWayPointIndex++;
                if (_currentWayPointIndex >= WayPoints.Length)
                {

                    _currentWayPointIndex = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position,
                WayPoints[_currentWayPointIndex].transform.position, Time.deltaTime * Speed);
        }
    }
}