using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    Vector3 scale = Vector3.one;

    private void Start()
    {
        animator.SetBool("walk", false);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetBool("walk", movement.x != 0 || movement.y != 0);
        if (movement.x != 0)
        {
            scale.x = Mathf.Sign(movement.x);
            transform.localScale = scale;
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (!animator.GetBool("throw"))
            {
                StartCoroutine(AnimationComplete());
                animator.SetBool("throw", true);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    IEnumerator AnimationComplete()
    {
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("throw", false);
    }
}
