using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Unity.VisualScripting.Metadata; //this stuff was making unity throw out errors when building :P, so I commented it out for now - Hannes

namespace Bluescreen.BlorboTheCat
{
    public class WaypointEnemy : MonoBehaviour
    {
        [SerializeField] private float speed = 2;
        public List<Transform> waypoints = new List<Transform>();
        private int target = 0;
        public bool facingRight;

        private void Awake()
        {
            foreach (Transform child in waypoints)
            {
                child.SetParent(null, true);
            }
        }

        void Start()
            {
                // start from first waypoint in list
                //transform.position = waypoints[0].position;
                // nvm

                // sprite flip
                if (waypoints[target].position.x > transform.position.x || waypoints[target].position.y > transform.position.y)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    facingRight = true;
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    facingRight = false;
                }
            }

        void Update()
            {
                // if at a waypoint, change direction to the next one
                if (transform.position == waypoints[target].position)
                {
                    target += 1;
                    // when at last waypoint, go back to first one
                    if (target == waypoints.Count)
                    {
                        target = 0;
                    }
                    // sprite flip
                    if (waypoints[target].position.x > transform.position.x || waypoints[target].position.y > transform.position.y)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        facingRight = true;
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        facingRight = false;
                    }
                } 
                // if not at a waypoint, keep moving towards the next one
                transform.position = Vector2.MoveTowards(transform.position, waypoints[target].position, speed * Time.deltaTime);
            }

        /* old version
        public List<Transform> points;

        public int nextID = 0;

        int idChangeValue = 1;

        public float speed = 2;

        void Update()
        {
            MoveToNextPoint();
        }

        void MoveToNextPoint()
        {
            //Get the next point transform
            Transform goalPoint = points[nextID];
            //Flip the enemy transform to look into the point's direction
            if(goalPoint.transform.position.x > transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
            //Move the enemy towards the goal point
            transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
            //Check the distance between the enemy and the goal point to trigger the next point
            if(Vector2.Distance(transform.position, goalPoint.position)<1f)
            {
                //Check if we are at the end of the line
                if (nextID == points.Count - 1)
                    idChangeValue = -1;
                //Check if we are at the start of the line
                if (nextID == 0)
                    idChangeValue = 1;
                //Apply the change on the nextID
                nextID += idChangeValue;
            }
        }*/
    }
}