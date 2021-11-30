using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    float force = 25f;

    Grapple grapple;
    Rigidbody rb;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    public void Initialize(Grapple grapple, Transform shootTransform)
    {
        transform.forward = shootTransform.forward;
        this.grapple = grapple;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = new Vector3[]
        {
            transform.position,
            grapple.transform.position
        };

        lineRenderer.SetPositions(positions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((LayerMask.GetMask("Scenario") & 1 << other.gameObject.layer) > 0)
        {
            rb.useGravity = false;
            rb.isKinematic = true;

            grapple.StartPull();
        }
    }
}
