using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// thank you Alexander Zotov
namespace Bluescreen.BlorboTheCat
{
    public class BezierFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform[] routes;
        private int routeToGo = 0;
        private float tParam = 0f;
        private Vector2 objectPosition;
        private float speedModifier = 0.15f;
        private bool coroutineAllowed = true;
        public static bool introOngoing = true;
        private bool introDone = false;
        private bool startWaitDone = false;
        private bool endSmoothStarted = false;
        public CinemachineVirtualCamera cm;
        private GameObject playerFollowPoint;

        void Start()
        {
            cm.Follow = gameObject.transform;
            playerFollowPoint = GameObject.Find("CameraFollowPoint");
            cm.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 0;
            cm.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0;
            StartCoroutine(StartWait());
        }

        void Update()
        {
            if (introDone)
            {
                cm.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 1;
                cm.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 1;
                PlayerController.movementFrozen = false;
                PlayerActions.actionsFrozen = false;
            }
            if (!introOngoing)
            {
                if (GameObject.Find("Player") != null)
                {
                    cm.Follow = playerFollowPoint.transform;
                }
                introDone = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                introOngoing = false;
            }
            if (coroutineAllowed && startWaitDone)
            {
                StartCoroutine(GoByTheRoute(routeToGo));
            }
            if (Vector2.Distance(transform.position, playerFollowPoint.transform.position) < 0.75f && !endSmoothStarted)
            {
                StartCoroutine(EndSmooth());
                endSmoothStarted = true;
            }
        }

        private IEnumerator StartWait()
        {
            yield return new WaitForSeconds(2f);
            startWaitDone = true;
            StartCoroutine(StartSmooth());
        }

        private IEnumerator StartSmooth()
        {
            float step = speedModifier/20;
            speedModifier = 0;

            for (int i = 0; i < 20; i++)
            {
                speedModifier += step;
                yield return new WaitForSeconds(0.05f);
            }
        }

        private IEnumerator EndSmooth()
        {
            float step = speedModifier/20;

            for (int i = 0; i < 18; i++)
            {
                speedModifier -= step;
                yield return new WaitForSeconds(0.05f);
            }
        }

        private IEnumerator GoByTheRoute(int routeNum)
        {
            coroutineAllowed = false;

            Vector2 p0 = routes[routeNum].GetChild(0).position;
            Vector2 p1 = routes[routeNum].GetChild(1).position;
            Vector2 p2 = routes[routeNum].GetChild(2).position;
            Vector2 p3 = routes[routeNum].GetChild(3).position;

            while(tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;
                objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
                transform.position = objectPosition;
                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;
            routeToGo += 1;

            if(routeToGo > routes.Length - 1)
            {
                introOngoing = false;
                Destroy(gameObject, 0.1f);
            }
            else
            {
                coroutineAllowed = true;
            }
        }
    }
}
