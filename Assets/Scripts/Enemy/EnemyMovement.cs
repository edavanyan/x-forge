using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator animator;
    public Transform destination;

    public float moveSpeed = 1f;

    public Rigidbody2D rb;

    Vector2 movement;

    Vector3 scale = Vector3.one;

    private void Start()
    {
        StartCoroutine(ChangeSpeed());
    }

    void Update()
    {
        movement = destination.position - transform.position;
        if (movement.x != 0)
        {
            scale.x = Mathf.Sign(movement.x);
            transform.localScale = scale;
        }
    }

    private IEnumerator ChangeSpeed()
    {
        yield return new WaitForSeconds(0.25f);
        moveSpeed = Random.Range(5, 10) * 0.1f;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
