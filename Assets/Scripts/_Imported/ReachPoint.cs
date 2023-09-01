using UnityEngine;

namespace SpaceShooter
{
    public class ReachPoint : MonoBehaviour
    {
        private bool reachedPointPosition;
        public bool ReachedPointPosition => reachedPointPosition;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent(out SpaceShip ship))
            {
                if (ship == Player.Instance.ActiveShip)
                {
                    reachedPointPosition = true;
                } 
            }
        }
    }
}
