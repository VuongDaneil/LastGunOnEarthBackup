using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    float horizontalMovement;
    float verticalMovement;

    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float runSpeed = 20f;
    [SerializeField] float dashSpeed = 150f;
    [SerializeField] float MaxStamina = 600;
    float stamina;
    float rbDragValue = 6f;
    float movementMultiplier = 10f;
    Vector3 moveDirection;


    [Header("Jump")]
    float distToGround;
    bool isGrounded;
    [SerializeField] float jumpForce = 40f;
    float air_rbDragValue = 2f;
    [SerializeField] float airMultiplier = 0.3f;

    [Header("HUD")]
    GameObject Status_display;

    Rigidbody rb;
    RigidbodyConstraints originalConstraints;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        originalConstraints = rb.constraints;
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        stamina = MaxStamina * 0.8f;

        //HUD
        Status_display = GameObject.FindWithTag("Status_UI");
        PlayerStatus player_info = Status_display.GetComponent<PlayerStatus>();
        player_info.PlayerInfo = this;
    }


    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround - 1f);

        //Sprint & jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) { Jump(); }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && isGrounded && stamina > 3) { Sprint(); }

        if (Input.GetKeyDown(KeyCode.X) && isGrounded) { Dash(); }

        //manage stamina
        //Nếu stamina nhỏ 0 -> 0
        //Nếu stamina vượt qua 500 -> 500
        if (isWalking()) stamina += 0.5f;
        if (isResting()) stamina += 1f;
        if (stamina < 0) stamina = 0;
        if (stamina >= MaxStamina) stamina = MaxStamina; //max stamina

        MyInput();
        controlDrag();

        //print(rb.velocity.magnitude);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }
    bool isResting()
    {
        if (rb.velocity.magnitude > 2) { return false; }
        return true;
    }
    bool isWalking()
    {
        if (rb.velocity.magnitude > 18) { return false; }
        return true;
    }
    void controlDrag()
    {
        if (isGrounded)
        {
            rb.drag = rbDragValue;
        }
        else
        {
            rb.drag = air_rbDragValue;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
    void Sprint()
    {
        if (isGrounded && stamina > 0)
        {
            stamina-=2;
            rb.AddForce(moveDirection.normalized * runSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded && stamina > 0)
        {
            stamina -= 2;
            rb.AddForce(moveDirection.normalized * runSpeed * movementMultiplier * airMultiplier, ForceMode.VelocityChange);
        }
    }
    void Dash()
    {
        if(stamina > 100)
        {
            stamina -= 100;
            rb.AddForce(moveDirection.normalized * dashSpeed * movementMultiplier, ForceMode.Acceleration);
        }
    }
    void Jump()
    {
        if (stamina >= 100)
        {
            stamina -= 100;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }


    public float getMaxStamina()
    {
        return MaxStamina;
    }
    public float getStamina()
    {
        return stamina;
    }
}
