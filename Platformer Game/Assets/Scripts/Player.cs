using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f; //플레이어 이동 속도
    public float jumpPower = 5.0f; //플레이어 점프 힘
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask gourndLayer;
    private Animator anim;
    private Rigidbody2D rigidbody;
    bool isGround = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, gourndLayer);
        if (isGround)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }
        Move();
        Jump();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.W) && isGround)
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGround = false;
        }
    }

    void Move()
    {
        float posX = Input.GetAxis("Horizontal");
        if(posX != 0)
        {
            if(posX >= 0)
            {
                transform.eulerAngles= new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        transform.Translate(Mathf.Abs(posX) * Vector3.right * moveSpeed * Time.deltaTime);
    }
}
