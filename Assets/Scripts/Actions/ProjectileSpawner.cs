using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class ProjectileSpawner : MonoBehaviour
    {
        public Transform LaunchPoint;
        public GameObject ProjectilePrefab;

        public void FireProjectile()
        {
            Fire();
        }

        private void Fire()
        {
            var projectile = Instantiate(ProjectilePrefab, LaunchPoint.position, ProjectilePrefab.transform.rotation);
            var xDirection = projectile.transform.localScale.x * transform.localScale.x > 0 ? 1 : -1;
            projectile.transform.localScale =
                new Vector3(xDirection, projectile.transform.localScale.y, projectile.transform.localScale.z);
        }
    }
}