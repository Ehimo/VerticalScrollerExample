using UnityEngine;

namespace Interactables
{
    public abstract class InteractableWhoCollideWithBullet : Interactable
    {
        protected abstract void InteractWithBullet();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                OnCollideWithPlayer();
            }
            else if (col.gameObject.tag.Equals("Bullet"))
            {
                OnCollideWithBullet();
                col.gameObject.SetActive(false);
            }
        }

        protected void OnCollideWithBullet()
        {
            InteractWithBullet();
            Dispose();
        }
    }
}