using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    [SerializeField] private bool _grounded = false;
    private float horizontal = 0f;
    private float vertical = 0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            vertical = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (horizontal != 0 || vertical != 0)
        {
            rb.velocity = new Vector2(horizontal * speed * Time.deltaTime,
                rb.velocity.y + vertical * jumpForce * Time.deltaTime);
            vertical = 0f;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enviroment"))
        {
            _grounded = true;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            gameManager.ChangeHp(-1);
        }

        if (other.gameObject.CompareTag("Heal"))
        {
            Destroy(other.gameObject);
            gameManager.ChangeHp(1);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enviroment"))
        {
            _grounded = false;
        }
    }
}