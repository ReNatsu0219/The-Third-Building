using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [field: SerializeField] private LayerMask groundLayer;
    [field: SerializeField] private float groundCheckRadius;
    [field: SerializeField] private float bottomOffsetY;
    public Rigidbody2D rb;
    public BindableProperty<bool> isGrounded = new BindableProperty<bool>();

    public float OnFallingSpeedY;
    protected virtual void Awake()
    {

    }

    protected virtual void Update()
    {
        Vector2 currentVelocity = rb.velocity;
        //Debug.Log(isGrounded.Value);
        if (currentVelocity.y <= -7.5f)
            rb.velocity = new Vector2(currentVelocity.x, -7.5f);

    }
    protected virtual void FixedUpdate()
    {
        isGrounded.Value = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + bottomOffsetY), groundCheckRadius, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + bottomOffsetY), groundCheckRadius);
    }
}
