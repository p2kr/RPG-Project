using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float timeBetweenAttacks = 1.0f;
        [SerializeField] float weaponDamage = 5.0f;

        float timeSinceLastAttack = Mathf.Infinity;
        Health target;

        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null)
                return;
            if (target.IsDead)
                return;
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();

            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);//TODO:
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");  //TODO: 
        }

        // Animation Controller
        void Hit()
        {
            if (target == null)
                return;
            target.TakeDamage(weaponDamage);//TODO:
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.transform.position, transform.position) < weaponRange;
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead; // TODO:
        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
            StopAttack();
        }

        private void StopAttack()
        {
            animator.SetTrigger("stopAttack");// My attack stopping code
            animator.ResetTrigger("attack");// My attack stopping code
        }


    }
}