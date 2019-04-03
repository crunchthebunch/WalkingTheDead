using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(walkSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0.0f, walkSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
    }
}
