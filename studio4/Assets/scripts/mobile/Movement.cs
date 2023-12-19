using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check for the touch phase
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                float touchHorizontal = touch.deltaPosition.x / Screen.width;
                horizontal = touchHorizontal * speed;
            }
        }

        Vector3 direction = new Vector3(horizontal, 0, 0);
        rb.velocity = direction * speed;
    }
}
