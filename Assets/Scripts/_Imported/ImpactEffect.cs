using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float m_lifeTime = 1.0f;
        [SerializeField] private int m_damage;

        private float fillStep;

        private Vector3 finalSize;

        private float timer;

        private Collider2D firstCollider;
        private Vector2 firstColliderPos;

        private bool byPlayer;
        public bool ByPlayer => byPlayer;

        private void Start()
        {
            GetComponent<CircleCollider2D>().isTrigger = true;

            finalSize = transform.localScale;

            fillStep = finalSize.x / m_lifeTime;

            transform.localScale = Vector3.zero; 
        }

        private void Update()
        {
            timer += Time.deltaTime;

            var newScale = transform.localScale;
            newScale.x += fillStep * Time.deltaTime;
            newScale.y += fillStep * Time.deltaTime;
            newScale.z += fillStep * Time.deltaTime;
            transform.localScale = newScale;

            if (timer >= m_lifeTime)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == firstCollider || (Vector2) collision.transform.position == firstColliderPos) return;

            if (collision.transform.root.TryGetComponent(out Destructible dest))
            {
                dest.ApplyDamage(this, m_damage);

                if (byPlayer)
                {
                    Player.Instance.AddScore(dest.ScoreValue);
                }
            }
        }

        public void GetFirstCollider(Collider2D col, Vector2 pos)
        {
            firstCollider = col;
            firstColliderPos = pos;
        }

        public void SetByPlayer(bool state)
        {
            byPlayer = state;
        }

    }
}
