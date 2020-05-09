using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float smoothing;

    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    // Fixed update is when the physics systems ticks
    // Late update always comes last
    void FixedUpdate()
    {
        Transform target = player.transform;
        // Vector3 velocity = player.GetComponent<Rigidbody2D>().velocity;
        Vector3 velocity = Vector3.zero;

        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            //linear interpolation

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
        }
    }
}
