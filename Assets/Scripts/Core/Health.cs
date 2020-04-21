using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100.0f;
        bool isDead = false;
        public bool IsDead => isDead;
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if (healthPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead)
                return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();// sets StartAction(null)
        }
    }
}