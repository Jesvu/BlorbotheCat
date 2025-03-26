using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Tilemaps;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class PlayerController : MonoBehaviour
    {
        private float moveSpeed = 5f;
        public Transform movePoint;
        public LayerMask wallLayers;
        public Animator animator;
        private Vector2 movement;
        private Vector2 movingDir;
        private bool wasMovingVertical;

        public static bool movementFrozen = false;

        [SerializeField] private AudioSource walkAudioSource;
        [SerializeField] private ParticleSystem walkParticles;
        private bool particlesStopped;


        void Start()
        {
            // separate the move point from parent player character
            movePoint.parent = null;
            // assign layers that prevent movement
            wallLayers = LayerMask.GetMask("Walls") | LayerMask.GetMask("Breakable") | LayerMask.GetMask("Water");

            walkAudioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            
            if (!movementFrozen)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                bool inputHorizontal = Mathf.Abs(movement.x) == 1;
                bool inputVertical = Mathf.Abs(movement.y) == 1;

                animator.SetFloat("Horizontal", movingDir.x);
                animator.SetFloat("Vertical", movingDir.y);
                animator.SetFloat("Speed", movingDir.sqrMagnitude);

                

                if (!LevelLoader.firstActionDone && movingDir != Vector2.zero) {LevelLoader.firstActionDone = true;}

                if (movingDir.x != 0)
                {
                    animator.SetFloat("lastMoveX", movingDir.x);
                    animator.SetFloat("lastMoveY", 0);
                }
                if (movingDir.y != 0)
                {
                    animator.SetFloat("lastMoveY", movingDir.y);
                    animator.SetFloat("lastMoveX", 0);
                }

                // move the move point when the character is near enough the last position
                if (Vector3.Distance(transform.position, movePoint.position) <= .02f)
                {
                    // if moving in both directions, prioritize latest input (if no walls in the way)
                    if (inputHorizontal && inputVertical)
                    {
                        if (wasMovingVertical)
                        {
                            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f, 0f), .2f, wallLayers))
                            {
                                movePoint.position += new Vector3(movement.x, 0f, 0f);
                            }
                            else
                            {
                                animator.SetFloat("lastMoveX", movement.x);
                                animator.SetFloat("lastMoveY", 0);
                            }
                        }
                        else
                        {
                            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), .2f, wallLayers))
                            {      
                                movePoint.position += new Vector3(0f, movement.y, 0f);
                            }
                            else
                            {
                                animator.SetFloat("lastMoveY", movement.y);
                                animator.SetFloat("lastMoveX", 0);
                            }
                        }
                    }
                    
                    // horizontal movement
                    else if (inputHorizontal)
                    {
                        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f, 0f), .2f, wallLayers))
                        {
                            movePoint.position += new Vector3(movement.x, 0f, 0f);
                            wasMovingVertical = false;
                        }
                        else
                        {
                            animator.SetFloat("lastMoveX", movement.x);
                            animator.SetFloat("lastMoveY", 0);
                        }
                    }

                    // vertical movement
                    else if (inputVertical)
                    {
                        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), .2f, wallLayers)) {
                            movePoint.position += new Vector3(0f, movement.y, 0f);
                            wasMovingVertical = true;
                        }
                        else
                        {
                            animator.SetFloat("lastMoveY", movement.y);
                            animator.SetFloat("lastMoveX", 0);
                        }
                    }

                    if (Vector3.Distance(transform.position, movePoint.transform.position) == 0) {movingDir = Vector2.zero; }
                }

                // if moving, set the moving direction
                else
                {
                    
                    if (Mathf.Abs(transform.position.x - movePoint.transform.position.x) > Mathf.Abs(transform.position.y - movePoint.transform.position.y))
                    {
                        if (transform.position.x - movePoint.transform.position.x < 0) {movingDir = Vector2.right;}
                        else if (transform.position.x - movePoint.transform.position.x > 0) {movingDir = Vector2.left;}
                    }
                    else if (Mathf.Abs(transform.position.x - movePoint.transform.position.x) < Mathf.Abs(transform.position.y - movePoint.transform.position.y))
                    {
                        if (transform.position.y - movePoint.transform.position.y > 0) {movingDir = Vector2.down;}
                        else if (transform.position.y - movePoint.transform.position.y < 0) {movingDir = Vector2.up;}
                    }            
                }

                if (movingDir != Vector2.zero && walkParticles.isStopped) { walkParticles.Play(); }
                else if (walkParticles.isPlaying && movingDir == Vector2.zero) { walkParticles.Stop(); }
            }
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        }

        public void PlayWalkSfx()
        {
            walkAudioSource.Play();
        }
    }
}
