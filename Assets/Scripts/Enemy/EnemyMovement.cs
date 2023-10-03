using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator animator;
    public Transform destination;
    private Vector3 destinationPoint;

    public float moveSpeed = 1f;

    private float decisionDelay = 0.1f;

    public Rigidbody2D rb;

    Vector2 movement;

    Vector3 scale = Vector3.one;

    private void OnEnable()
    {
        StartCoroutine(ChangeSpeed());
    }

    void Update()
    {
        movement = destinationPoint - transform.position;
        if (movement.x != 0)
        {
            scale.x = Mathf.Sign(movement.x);
            transform.localScale = scale;
        }
    }

    private IEnumerator ChangeSpeed()
    {
        while (gameObject.activeSelf)
        {
            decisionDelay = Random.Range(0.1f, 0.35f);
            yield return new WaitForSeconds(decisionDelay);
            destinationPoint = destination.position;
            moveSpeed = Random.Range(5, 10) * 0.1f;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
