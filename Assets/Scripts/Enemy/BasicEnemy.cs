using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Transform target;
    public Transform BackPoint;
    [SerializeField] Rigidbody rb;
    [SerializeField] Enemy enemy;
    public float speed = 3f;
    public float attack1Range = 1f;
    PlayerProperty playerproperty;
    // Use this for initialization
    void Start()
    {
        rb.GetComponent<Rigidbody>();
      playerproperty = FindObjectOfType<PlayerProperty>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveToPlayer()
    {
        Vector3 direction = target.position - transform.position;
        //rotate to look at player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -180, 0), Space.Self);

        //move towards player
        if (Vector3.Distance(transform.position, target.position) > attack1Range && playerproperty.isDead == false)
        {
            if (enemy.IAttacked == false)
            {
                rb.MovePosition((Vector3)transform.position + (direction * speed * Time.deltaTime));
            }
        }
    }

    public void Rest()
    {
        Vector3 direction = BackPoint.position - transform.position;
        if (Vector3.Distance(transform.position, BackPoint.position) > 3)
        {
            transform.LookAt(BackPoint.position);
            transform.Rotate(new Vector3(0, -180, 0), Space.Self);
            rb.MovePosition((Vector3)transform.position + (direction * speed * Time.deltaTime));
        }
        else
        {
            transform.rotation = BackPoint.rotation;
            //transform.rotation = Quaternion.identity;
        }
    }
}
