using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class ProjectileComponent : MonoBehaviour
    {
        private readonly double _timeTillKill = 1.4f;
        private Rigidbody2D _rigidBody;
        private float _timeAlive;

        public Vector2 MoveSpeed = new(2f, 0);

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rigidBody.velocity = new Vector2(MoveSpeed.x * transform.localScale.x * -1, MoveSpeed.y);
        }

        private void Update()
        {
            _timeAlive += Time.deltaTime;
            if (_timeAlive > _timeTillKill)
                Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == Data.GameObjectStrings.Player)
            {
                var damage = collision.gameObject.GetComponent<PlayerDamage>();
                damage.Kill();
            }
            else if(collision.tag == Data.GameObjectStrings.Ground)
            {
                Destroy(gameObject);
            }
        }
    }
}