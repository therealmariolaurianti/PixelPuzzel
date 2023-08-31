using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class MetalBox : MonoBehaviour
    {
        public bool OpenDoor;

        public void OnTriggerExit2D(Collider2D collision)
        {
            var boxCollider = gameObject.GetComponent<BoxCollider2D>();
            boxCollider.isTrigger = false;

            if (OpenDoor)
                CharacterEvents.OpenDoor.Invoke();
        }
    }
}
