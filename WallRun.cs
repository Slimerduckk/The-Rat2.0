using UnityEngine;

public class WallRun : MonoBehaviour {

public float moveSpeed = 20f;           
public float jumpForce = 20f;          
public float wallSlideSpeed = 20f;       
public float wallJumpForce = 20f;       
public float wallJumpTime = 0.5f;       
public LayerMask wallLayerMask;         

private Rigidbody2D rb;                 
private CircleCollider2D circleCollider;
private bool isGrounded;                
private bool isWallRunning;             
private bool isWallSliding;            
private bool isWallSticking;            
private bool hasJumpedOffWall;          
private float lastWallJumpTime;         
private Vector2 wallJumpDirection;      

void Start() {
    rb = GetComponent<Rigidbody2D>();
    circleCollider = GetComponent<CircleCollider2D>();
}
void Update() {
    float moveX = Input.GetAxis("Horizontal");
    rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

    RaycastHit2D wallHit = Physics2D.CircleCast(circleCollider.bounds.center, circleCollider.radius, transform.right, circleCollider.radius, wallLayerMask);
    if (wallHit) {
        isWallRunning = true;
        isGrounded = false;
    } else {
        isWallRunning = false;
    }

    if (isWallRunning && rb.linearVelocity.y < 0f) {
        isWallSliding = true;

        // Limiting the player's speed while wall sliding
        if (rb.linearVelocity.y < -wallSlideSpeed) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
    } else {
        isWallSliding = false;
    }

    if (Input.GetKeyDown(KeyCode.Space) && ((isGrounded && !isWallSliding) || (isWallSliding && !hasJumpedOffWall))) {
        if (isWallSliding) {
            // Determining the direction of the player's wall jump based on the input
            if (Input.GetKey(KeyCode.A)) {
                rb.AddForce(new Vector2(wallJumpDirection.x * wallJumpForce, wallJumpDirection.y * wallJumpForce), ForceMode2D.Impulse);
                rb.linearVelocity = new Vector2(-moveSpeed, jumpForce);
            } else if (Input.GetKey(KeyCode.D)) {
                rb.AddForce(new Vector2(wallJumpDirection.x * wallJumpForce, wallJumpDirection.y * wallJumpForce), ForceMode2D.Impulse);
                rb.linearVelocity = new Vector2(moveSpeed, jumpForce);
            }
            hasJumpedOffWall = true;
        } else {

            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

   

        // Check if grounded
        RaycastHit2D groundHit = Physics2D.CircleCast(circleCollider.bounds.center, circleCollider.radius, Vector2.down, circleCollider.radius, wallLayerMask);
        isGrounded = groundHit.collider != null;

        // Wall jump timer
        if (isWallSliding) {
            wallJumpDirection = wallHit.normal;

            if (Time.time >= lastWallJumpTime + wallJumpTime) {
                isWallSticking = true;
            }
        } else {
            isWallSticking = false;
            lastWallJumpTime = Time.time;
        }

        if (isGrounded || (isWallSliding && !hasJumpedOffWall)) {
            hasJumpedOffWall = false;
        }
    }
}
