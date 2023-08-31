using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class DetectionZone : MonoBehaviour
    {
        public List<Collider2D> DetectedColliders = new();

        public UnityEvent<Collider2D> NoCollidersRemain;
        public UnityEvent<Collider2D> OnColliderDetected;

        private static string Projectile => "Projectile";
        private static string Platform => "Platform";
        private static string Trap => "Trap";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsInvalid(collision))
                return;

            DetectedColliders.Add(collision);
            OnColliderDetected.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            DetectedColliders.Remove(collision);
            if (DetectedColliders.Count <= 0)
                NoCollidersRemain?.Invoke(collision);
        }

        private static bool IsInvalid(Collider2D collision)
        {
            return collision.CompareTag(Projectile) || collision.CompareTag(Trap);
        }
    }
}