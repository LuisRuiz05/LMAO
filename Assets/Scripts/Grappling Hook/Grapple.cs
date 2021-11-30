using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    float pullSpeed = 0.5f;
    float stopDistance = 4f;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform shootTransform;

    Hook hook;
    bool isPulling;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isPulling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hook == null && Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            isPulling = false;
            hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
            hook.Initialize(this, shootTransform);
            StartCoroutine(DestroyHookAfterLifetime());
        }
        else if (hook != null && Input.GetMouseButtonDown(1))
        {
            DestroyHook();
        }

        if (!isPulling || hook == null) return;

        if (Vector3.Distance(transform.position, hook.transform.position) <= stopDistance)
        {
            DestroyHook();
        }
        else
        {
            rb.AddForce((hook.transform.position - transform.position).normalized * pullSpeed, ForceMode.VelocityChange);
        }
    }

    public void StartPull()
    {
        isPulling = true;
    }

    public void DestroyHook()
    {
        if (hook == null) return;

        isPulling = false;
        Destroy(hook.gameObject);
        hook = null;
    }

    IEnumerator DestroyHookAfterLifetime()
    {
        yield return new WaitForSeconds(8f);
        DestroyHook();
    }
}
