using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D),typeof(Rigidbody2D))]
    public class AutoAimProjectile : Projectile
    {
        [SerializeField] private float m_radius;
        [SerializeField] private float m_angularVelocity = 200f;

        private Destructible target;
        public Destructible Target => target;

        private Rigidbody2D rb;

        private bool noTargets = true;
        public bool NoTargets => noTargets;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        protected override void Update()
        {
            if (noTargets)
            {
                base.Update();
            }
            else
            {
                timer += Time.deltaTime;

                if (timer > m_lifeTime) Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (target != null) return;

            if (collision.transform.root.TryGetComponent(out Destructible dest))
            {
                if (dest != m_parent)
                {
                    target = dest;
                    noTargets = false;
                }
            }
            else
            {
                noTargets = true;
            }
        }

        private void FixedUpdate()
        {
            if (target == null) return;

            Vector2 dir = (Vector2) target.transform.position - rb.position;

            dir.Normalize();

            float rotateAmount = Vector3.Cross(dir, transform.up).z;

            rb.angularVelocity = - rotateAmount * m_angularVelocity;

            rb.velocity = transform.up * m_velocity;

            float stepLength = Time.deltaTime * m_velocity;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_parent)
                {
                    dest.ApplyDamage(this, m_damage);

                    if (ByPlayer)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);
                    }
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_radius;
        }

        #endif

    }
}
