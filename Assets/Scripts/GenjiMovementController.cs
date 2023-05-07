using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenjiMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float damping = 5f;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float wallRaycastDistance;
    [SerializeField] private float edgeUpForce;

    /*Climb Time*/
    [SerializeField] private float climbTime;
    [SerializeField] private float climbChargeSpeed;

    private CharacterController characterController;

    private float velocityY;
    private Vector3 currentImpact;
    private float gravity = Physics.gravity.y;

    private int jumpCount = 0;
    private bool isClimbing = false;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {


        if(characterController.isGrounded)
        {
            jumpCount = 0;
            climbTime = 0;
        }

        if(!isClimbing)
        {
            Move();
            Jump();
        }
        
    }

    private void Move()
    {
        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        movementInput = transform.TransformDirection(movementInput);

        if(characterController.isGrounded && velocityY < 0f)
        {
            velocityY = 0f;
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = movementInput * movementSpeed + Vector3.up * velocityY;

        if(currentImpact.magnitude > 0.2f)
        {
            velocity += currentImpact;
        }

        characterController.Move(velocity * Time.deltaTime);

        currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, damping * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, wallRaycastDistance))
            {
                if(hit.collider.GetComponent<Climbable>() != null)
                {
                    StartCoroutine(Climb(hit.collider));
                    return;
                }
            }
            /*if(characterController.isGrounded)
            {
                AddForce(Vector3.up, jumpForce);
            }*/

            if(jumpCount == 0)
            {
                ResetImpactY();
                AddForce(Vector3.up, jumpForce);

                if(characterController.isGrounded)
                {
                    jumpCount = 1;
                }
                else
                {
                    jumpCount = 2;
                }
            }
            else if(jumpCount == 1)
            {
                ResetImpactY();
                AddForce(Vector3.up, jumpForce);
                jumpCount = 2;
            }
            /*if (IsGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }*/
            /*if(jumpCount > 0)
            {
                if(IsGrounded())
                {
                    jumpCount = 0;
                }
            }*/

            /*if (jumpCount == 0)
            {
                Debug.Log("Jumping");
                rb.velocity = Vector3.up * jumpForce;

                if (IsGrounded())
                {
                    jumpCount = 1;
                }
                else
                {
                    jumpCount = 2;
                }
            }
            else if (jumpCount == 1)
            {
                rb.velocity = Vector3.up * jumpForce;
                jumpCount = 2;
            }*/
        }

        
    }

    public void AddForce(Vector3 direction, float magnitude)
    {
        currentImpact += direction.normalized * magnitude / mass;
    }

    public void ResetImpact()
    {
        currentImpact = Vector3.zero;

        velocityY = 0f;
    }

    public void ResetImpactY()
    {
        currentImpact.y = 0f;
        velocityY = 0f;
    }

    private IEnumerator Climb(Collider climbableCollider)
    {
        isClimbing = true;

        while (Input.GetKey(KeyCode.Space) && climbTime < 3)
        {
            climbTime += Time.deltaTime * climbChargeSpeed;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, wallRaycastDistance))
            {
                if(hit.collider == climbableCollider)
                {
                    characterController.Move(new Vector3(0f, climbSpeed * Time.deltaTime, 0f));
                    yield return null;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        ResetImpactY();

        AddForce(Vector3.up, edgeUpForce);

        isClimbing = false;
    }


}
