using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    public float m_Speed;

    public float timer = 60;
    private float startTime;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        startTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        WalkForward();
    }

    void WalkForward()
    {
       // Vector3 direction = m_Rigidbody.rotation * transform.forward;

        if (timer < startTime - 1.5f)
        {
            animator.SetBool("walk", true);
            m_Rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * m_Speed);
        }


    }


}
