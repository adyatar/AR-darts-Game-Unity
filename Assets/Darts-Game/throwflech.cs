using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwflech : MonoBehaviour
{
    public GameObject flechprefab;
    private float forceMultiplier = 0.08f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public AudioSource arrow;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private Vector3 newScale = new Vector3(0.7f, 0.7f, 0.7f);
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        initialPosition = transform.position;
    }
    private void Update()
    {
        if(collision2.trh==0)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // Get the first touch

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnDragStart(touch.position);
                        break;

                    case TouchPhase.Ended:
                        OnDragEnd(touch.position);
                        break;
                }



                ///////////// pour le flesh suivre le curseur/////////
                // Create a ray from the camera through the touch point
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                // Create a plane. This plane is aligned with the XY axis and is at a distance of 'transform.position.z' from the camera
                Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, transform.position.z));

                float entry;

                // Check if the ray intersects with the plane
                if (plane.Raycast(ray, out entry))
                {
                    Vector3 intersection = ray.GetPoint(entry);

                    // Move the object to the intersection point but maintain the original Z position
                    transform.position = new Vector3(intersection.x, intersection.y, transform.position.z);
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    // Reset the object's position to its initial position
                    Renderer rend = GetComponent<Renderer>();

                    rend.enabled = false;
                    transform.position = initialPosition;
                    Invoke("showFlesh", 0.5f);

                }

            }
        }
       
        
    }
    private void showFlesh()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
    private void OnDragStart(Vector3 touchPosition)
    {
        startPosition = touchPosition;
    }
    private void OnDragEnd(Vector3 touchPosition)
    {
        endPosition = touchPosition;
        float dragDistance = (endPosition - startPosition).magnitude;
        float dynamicThrowForce = dragDistance * forceMultiplier;
        Throw(dynamicThrowForce);
    }
    private void Throw(float throwStrength)
    {
        arrow.Play();
        //When a user touches the screen of a mobile device, that touch has a 2D screen position (x, y in pixels).
       //  The ScreenPointToRay function is used to create a 3D ray that starts from the camera and goes through that 2D point
        Ray ray = Camera.main.ScreenPointToRay(endPosition);
        //extracting the direction (3d)
        Vector3 throwDirection = ray.direction;
        

        throwDirection.y += 1f;
    



        GameObject ballCopy = Instantiate(flechprefab, transform.position, Quaternion.identity);
        //ballCopy.transform.localScale = newScale;
        ballCopy.transform.rotation = Quaternion.Euler(2, 173, -1);
        Rigidbody copyRb = ballCopy.GetComponent<Rigidbody>();


        copyRb.isKinematic = false;

        copyRb.AddForce(throwDirection.normalized * (throwStrength / 2f), ForceMode.Impulse);


        throwflech throwBallScript = ballCopy.GetComponent<throwflech>();
        if (throwBallScript != null)
        {
            Destroy(throwBallScript);
        }
    }
    
}


