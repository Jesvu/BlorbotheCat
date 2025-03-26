using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class InfoSign : MonoBehaviour
    {
        [SerializeField] private Transform ptf;
        [SerializeField] private float detectDistance = 1.5f;
        [SerializeField] private float popupTime = 1f;
        [SerializeField] private float yOffset = 5f;
        private Vector3 startPos;

        private float popupTimeCounter;
        private float popupTimePercentage;

        [SerializeField] private GameObject infoObject;
        [SerializeField] private CanvasGroup infoObjectGroup;

        void Awake()
        {
            startPos = infoObject.transform.localPosition;
            popupTimeCounter = popupTime;
            ptf = GameObject.Find("Player").GetComponent<Transform>();
        }

        void Update()
        {
            if(ptf != null)
            {
                if (Vector2.Distance(ptf.position, transform.position) <= detectDistance)
                {
                    if (popupTimeCounter > 0f) { popupTimeCounter -= Time.deltaTime; }
                    else { popupTimeCounter = 0f; }
                }
                else if (popupTimeCounter < popupTime) { popupTimeCounter += Time.deltaTime; }
                else { popupTimeCounter = popupTime; }

                popupTimePercentage = popupTimeCounter / popupTime;

                infoObjectGroup.alpha = 1 - popupTimePercentage;

                infoObject.transform.localPosition = Vector3.Lerp(startPos, startPos - new Vector3(0f, -yOffset, 0f), 1 - popupTimePercentage);
            }
            
        }
    }
}
