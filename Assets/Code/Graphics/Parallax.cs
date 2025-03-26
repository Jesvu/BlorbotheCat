using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parallax : MonoBehaviour
{
    private float length, startpos_x, startpos_y;
    [SerializeField]private Camera cam;
    [SerializeField]private float parallaxEffect;
    private float parallaxEffectY;

    [SerializeField] private bool usesMouseInstead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        startpos_y = transform.position.y;
        startpos_x = transform.position.x;
        //length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        parallaxEffectY = parallaxEffect;
        if(!usesMouseInstead)
        {
            float temp = (cam.transform.position.x * (1 - parallaxEffect));
            float dist = (cam.transform.position.x * parallaxEffect);
            float distY = (cam.transform.position.y * parallaxEffectY);
        
            transform.position = new Vector3(startpos_x + dist, startpos_y + distY, transform.position.z);

        }
        else
        {
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            float temp = (mouseWorldPosition.x * (1 - parallaxEffect));
            float dist = (mouseWorldPosition.x * parallaxEffect);
            float distY = (mouseWorldPosition.y * parallaxEffectY);

            transform.position = new Vector3(startpos_x + dist, startpos_y + distY, transform.position.z);
        }
        
    }
}
