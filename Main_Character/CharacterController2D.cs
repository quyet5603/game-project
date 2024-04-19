using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Đảm bảo nếu thêm script này vào bất kỳ object nào thì object đó cần có Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]

public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 2.0f;
    // [SerializeField] float runSpeed = 5f;
    Vector2 motionVector; // Lấy hướng di chuyển
    public Vector2 lastMotionVector; // Lấy hướng di chuyển khi nhả phím
    Animator animator;
    public bool moving;   // Kiểm tra đang di chuyển
    public bool running;

    void Awake()
    {
        speed = 5.0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Move()
    {
        rigidbody2d.velocity = motionVector * speed;
    }

    private void Update()
    {
        // Lấy hướng di chuyển của player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        motionVector = new Vector2(horizontal, vertical);

        // Chuyển đổi trạng thái nhờ truyền giá trị cho 2 tham số trong animator
        // Từ 2 biến dó chọn hướng di chuyển cho character
        // animator.SetFloat("horizontal", horizontal);
        // animator.SetFloat("vertical", vertical);

        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);

        // Nếu đang di chuyển thì liên tục cập nhật hướng di chuyển
        if(moving)
        {
            // Chuyển đổi trạng thái nhờ truyền giá trị cho 2 tham số trong animator
            // Từ 2 biến dó chọn hướng di chuyển cho character
            animator.SetFloat("horizontal", horizontal);
            animator.SetFloat("vertical", vertical);

            // Liên tục cập nhật hướng khi di chuyển để lấy hướng khi dừng
            lastMotionVector = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical",vertical);
        }
        
    }

    void FixedUpdate()
    {
        Move();
    }

    
}
