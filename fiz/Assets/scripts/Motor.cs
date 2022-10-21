using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _jumpForce;
    [SerializeField] private KeyCode _jumpKeyCode;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private Color _accentActiveColor;
    [SerializeField] private Color _accentDefaltColor;

    private bool isGrounded;
    private Color currGround;
    private void Update()
    {
        if (Input.GetKeyDown(_jumpKeyCode))
            JumpUp();
        if (Input.GetAxis("Horizontal") != 0)
            Move();
    }
    private void JumpUp()
    {
        if (isGrounded)
        {
            _rb.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((_groundLayerMask.value & (1 << col.gameObject.layer)) > 0)
        {
            isGrounded = true;
            SpriteRenderer other = col.gameObject.GetComponent<SpriteRenderer>();
            if (other.color == _accentActiveColor)
                other.color = _accentDefaltColor;
            else other.color = _accentActiveColor;            
        }
    }
    
    
    private void Move()
    {
        _rb.velocity = new Vector2(_speed * Input.GetAxis("Horizontal"), _rb.velocity.y);
    }
}
