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

        float timeSinceLastAttack = 0;
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
                animator.SetTrigger("attack");  //TODO: 
                timeSinceLastAttack = 0;
            }

        }

        // Animation Controller
        void Hit()
        {
            target.TakeDamage(weaponDamage);//TODO:
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.transform.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
            animator.SetTrigger("stopAttack");// My attack stopping code
        }



    }
}