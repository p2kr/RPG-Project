using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent agent;
        Animator anim;
        Health health;
        [SerializeField] float maxSpeed = 6f;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            // GetComponent<Fighter>().Cancel();
            MoveTo(destination, speedFraction);
        }
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            agent.SetDestination(destination);
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            agent.isStopped = false;
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }

        void UpdateAnimator()
        {
            Vector3 velocity = (agent.velocity);
            Vector3 localVelocity = transform.InverseTransformVector(velocity);
            anim.SetFloat("forwardSpeed", localVelocity.z);
        }

        // Update is called once per frame
        void Update()
        {
            agent.enabled = !health.IsDead;
            UpdateAnimator();
        }
    }
}