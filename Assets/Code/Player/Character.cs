using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

namespace Bluescreen.BlorboTheCat
{

    public class Character : MonoBehaviour
    {

        //this stuff was making unity throw out errors when building :P, so I commented it out for now - Hannes
        /*
        public const string WalkingParam = "Walking";

        public float moveSpeed = 4f;
        
        private Vector2 input;

        public Transform movePoint;

        private new Rigidbody2D rigidbody;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            movePoint.parent = null;
        }

        private void Update()
        {
           
            if (Vector2.Distance(transform.position, movePoint.position) <= .02f)
            {
                movePoint.Translate(input * new Vector2(1f, 1f));
            }
            transform.position = Vector2.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            UpdateAnimator();
            
        }

        private void UpdateAnimator()
        {
            // Run
            if (Mathf.Abs(rigidbody.position.x) > 0)
            {
                animator.SetInteger(WalkingParam, 1);
            }
            // Idle
            else
            {
                animator.SetInteger(WalkingParam, 0);
            }
        }

        public void Move(InputAction.CallbackContext context)
        {
            input = context.ReadValue<Vector2>();
        }*/
    }
}
