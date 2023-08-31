using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Rotate : MonoBehaviour
    {
        public float Speed = 2f;

        private void Update()
        {
            transform.Rotate(0, 360 * Speed * Time.deltaTime, 0);
        }
    }
}