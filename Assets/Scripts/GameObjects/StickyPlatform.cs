using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class StickyPlatform : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == Data.GameObjectStrings.Player) 
                collision.gameObject.transform.SetParent(transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == Data.GameObjectStrings.Player) 
                collision.gameObject.transform.SetParent(null);
        }
    }
}