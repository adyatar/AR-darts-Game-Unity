using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwflechTest : MonoBehaviour
{
    public GameObject flechprefab;
    public float forceMultiplier = 0.08f;
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
        //initialPosition = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          OnDragStart();     
        }else if(Input.GetMouseButtonUp(0))
        {
            OnDragEnd();
        }
    }

    private void OnDragStart()
    {
        startPosition = Input.mousePosition;
    }
    private void OnDragEnd()
    {
        endPosition = Input.mousePosition;
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
      //  ballCopy.transform.localScale = newScale;
        ballCopy.transform.rotation = Quaternion.Euler(2, 173, -1);
        Rigidbody copyRb = ballCopy.GetComponent<Rigidbody>();


        copyRb.isKinematic = false;

        copyRb.AddForce(throwDirection.normalized * (throwStrength / 2f), ForceMode.Impulse);


        throwflechTest throwBallScript = ballCopy.GetComponent<throwflechTest>();
        if (throwBallScript != null)
        {
            Destroy(throwBallScript);
        }
    }
}
