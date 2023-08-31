using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Slime : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, 1);
        }
    }
}