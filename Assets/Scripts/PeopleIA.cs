using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeopleIA : MonoBehaviour
{
    //public NavMeshAgent nav;
    public int action;
    public float cronometer;
    Animator animator;
    public Quaternion angle;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PeopleBehaviour();
    }

    public void PeopleBehaviour()
    {
        cronometer += 1 * Time.deltaTime;
        if (cronometer >= 3)
        {
            action = Random.Range(0, 2);
            cronometer = 0;
        }
        switch (action)
        {
            case 0:
                animator.SetBool("Walk", false);
                break;
            case 1:
                range = Random.Range(0, 360);
                angle = Quaternion.Euler(0, range, 0);
                action++;
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                animator.SetBool("Walk", true);
                break;
        }
    }
}
