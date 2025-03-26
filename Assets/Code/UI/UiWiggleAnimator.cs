using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class UiWiggleAnimator : MonoBehaviour
    {
        [SerializeField] private float scaleModifier = 1f;
        [SerializeField] private float scaleSpeed;
        [SerializeField] private float minSize = 1f;
        [SerializeField] private float rotationAmount = 1f;
        [SerializeField] private float rotationSpeed;
        private float timer;

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            float x = Mathf.Abs(scaleModifier * Mathf.Cos(scaleSpeed * timer));
            float y = rotationAmount * Mathf.Sin(rotationSpeed * timer);

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, y));
            transform.localScale = new Vector3(x+minSize, x+minSize, x+minSize);
        }
    }
}
