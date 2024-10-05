using System;
using UnityEngine;

namespace Script
{
    public class BossStateMachine : MonoBehaviour
    {
        private enum State
        {
            Idle,
            Move,
            Attack
        }

        [SerializeField] private State currentState;
        [SerializeField] private BossEnemy boss;
        [SerializeField] private float attackRate;
        public float timer;
        private bool attacked;

        // Dictionary to handle state actions and transitions
        private Action currentStateAction;

        private void Start()
        {
            timer = attackRate;
            SetState(State.Move); // Set initial state
        }

        private void Update()
        {
            currentStateAction?.Invoke(); // Execute current state action
        }

        private void SetState(State newState)
        {
            currentState = newState;
            switch (currentState)
            {
                case State.Idle:
                    boss.move = false;
                    currentStateAction = Idle;
                    break;
                case State.Move:
                    boss.move = true;
                    currentStateAction = Move;
                    break;
                case State.Attack:
                    currentStateAction = Attack;
                    break;
            }
        }

        private void Idle()
        {
            // Debug.Log("do idle");

            // Transition logic
            SetState(State.Attack);
        }

        private void Move()
        {
            // Debug.Log("do move");

            // Transition logic
            if (!boss.move)
            {
                SetState(State.Idle);
            }
        }

        private void Attack()
        {
            // Debug.Log("do attack");

            timer -= Time.deltaTime;
            if (!attacked &&timer < 1)
            {
                boss.Attack();

                attacked = true;
            }

            

            // Transition logic
            if (attacked)
            {
                if (timer < -3)
                {
                    SetState(State.Move);
                    attacked = false;
                    timer = attackRate;
                }
            }
            

        }
    }
}