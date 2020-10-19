using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Salto")]
    [SerializeField] private float fJumpVelocity = 5;
    [SerializeField] float fJumpControl = 0.2f;
    [SerializeField] [Range(0, 1)] float fJumpCapControl = 0.5f;
    [SerializeField] float fGroundedControl = 0.25f;
    [SerializeField] float fGroundRangeX;
    [SerializeField] float fGroundRangeY;
    [SerializeField] float fSuspensionRange;
    [SerializeField] float fSuspensionTime;
    [SerializeField] float fSuspensionGravity;
    [SerializeField] float fNormalGravity;
    [SerializeField] LayerMask layerGround;
    Vector2 v2GroundedPositionControl;
    Vector2 v2GroundedScaleControl;
    bool bGrounded;
    bool bGravitySwap = false;
    float fJumpRefControl = 0;
    float fSuspensionTimeControl = 0;

    [Header("Movimiento")]
    [SerializeField] float fSpeedDrag = 10;
    [SerializeField] [Range(0, 1)] float fMoveControl = 0.5f;
    [SerializeField] [Range(0, 1)] float fStopControl = 0.5f;
    [SerializeField] [Range(0, 1)] float fTurnControl = 0.5f;
    float fHorizontalVelocity;
    float fGroundedRefControl = 0;
    bool facingRight = true; 
   
    [Header("Knockback")]
    [SerializeField] Transform tTwistPoint;
    [SerializeField] float fKnockbackForce;

    [Header("Ball")]
    [SerializeField] Transform tAttackPos;
    [SerializeField] GameObject goBall;
    [SerializeField] float fBallShootForce;
    [SerializeField] float fBallReloadForce;
    [SerializeField] float fReloadJump;
    Rigidbody2D rbBall;
    Vector2 v2BallToPlayer;
    bool bRDYShoot = false;
    //bool bRDYReload = false;
    bool bBallOn;
    bool bReloading = false;


    [Header("General")]
    [SerializeField] Transform tModel;
    Rigidbody2D rbPlayer;


    //0, red / 1, blue / 2, yellow.
    [Header("Powers")]
    [SerializeField] float fReloadJumpPower;
    [SerializeField] float fJumpPowerTimer;
    [SerializeField] float fBallGravityPower;
    float fJumpPowerTimerControl = 0;
    bool bJumpPowerOn = false;
    int currentPower = 0;
    bool bSecondShot = false;


    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        rbBall = goBall.GetComponent<Rigidbody2D>();

        rbPlayer.gravityScale = fNormalGravity;
        CatchBall();

        fSuspensionTimeControl = fSuspensionTime;
        fJumpPowerTimerControl = fJumpPowerTimer;
    }

    void Update()
    {
        if (Input.GetAxis("Hold") == 0)
        {
            Movement();
        }

        Jump();
        Aim();
        Shoot();
        Reload();
    }

    private void FixedUpdate()
    {
        v2GroundedPositionControl = (Vector2)transform.position + new Vector2(0, -0.1f);
        v2GroundedScaleControl = new Vector2(fGroundRangeX, fGroundRangeY);
        bGrounded = Physics2D.OverlapBox(v2GroundedPositionControl, v2GroundedScaleControl, 0, layerGround);
        if(bGrounded)
        {
            bRDYShoot = true;
            bSecondShot = true;
        }
    }

    private void FlipX()
    {
        facingRight = !facingRight;
        Vector3 scaler = tModel.transform.localScale;
        scaler.x *= -1;
        tModel.transform.localScale = scaler;
    }

    void Knockback(Vector3 hitPoint)
    {
        Vector3 v3PlayerPos = transform.position;
        v3PlayerPos.Normalize();
        if (hitPoint.y <= v3PlayerPos.y)
        {
            rbPlayer.velocity = new Vector2(-hitPoint.x * fKnockbackForce, -hitPoint.y * fKnockbackForce);
        }

    }

    void Aim()
    {
        float fHorizontalStick = Input.GetAxis("HorizontalRightStick");
        float fVerticalStick = Input.GetAxis("VerticalRightStick");
        tTwistPoint.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(-fHorizontalStick, -fVerticalStick) * 180 / Mathf.PI);
    }

    public void CatchBall()
    {
        rbBall.velocity = Vector2.zero;
        rbBall.bodyType = RigidbodyType2D.Kinematic;
        goBall.GetComponent<CircleCollider2D>().enabled = false;
        goBall.transform.parent = tAttackPos;
        goBall.transform.position = tAttackPos.position;
        bBallOn = true;

        if(bReloading)
        {
            if(currentPower == 1)
            {
                bJumpPowerOn = true;
                rbPlayer.velocity = v2BallToPlayer * fReloadJumpPower;
            }
            else
            {
                rbPlayer.velocity = Vector2.up * fReloadJump;
            }
            
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Vector3 v3HitDirection = tAttackPos.position - transform.position;
            v3HitDirection.Normalize();

            if (bBallOn && bRDYShoot)
            {
                rbBall.bodyType = RigidbodyType2D.Dynamic;
                goBall.GetComponent<CircleCollider2D>().enabled = true;
                goBall.transform.parent = null;

                if(currentPower == 2)
                {
                    rbBall.gravityScale = fBallGravityPower;
                }
                
                rbBall.velocity = new Vector2(v3HitDirection.x, v3HitDirection.y) * fBallShootForce;
                Knockback(v3HitDirection);
                bBallOn = false;
                bRDYShoot = false;
            }
            else if (bBallOn && bSecondShot && currentPower == 2)
            {
                rbBall.bodyType = RigidbodyType2D.Dynamic;
                goBall.GetComponent<CircleCollider2D>().enabled = true;
                goBall.transform.parent = null;
                rbBall.gravityScale = fBallGravityPower;
                rbBall.velocity = new Vector2(v3HitDirection.x, v3HitDirection.y) * fBallShootForce;
                Knockback(v3HitDirection);
                bBallOn = false;
                bSecondShot = false;
            }
        }
    }

    public void Reload()
    {
        if(!bBallOn)
        {
            if (Input.GetAxis("Dash") != 0 && bSecondShot)
            {
                v2BallToPlayer = transform.position - goBall.transform.position;
                v2BallToPlayer.Normalize();

                rbBall.velocity = v2BallToPlayer * fBallReloadForce;

                bReloading = true;
            }
            else
            {
                bReloading = false;
            }
        }
    }

    public void Powers()
    {

    }

    private void Movement()
    {
        fHorizontalVelocity = rbPlayer.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fStopControl, Time.deltaTime * fSpeedDrag);
        }
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fTurnControl, Time.deltaTime * fSpeedDrag);
        }
        else
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fMoveControl, Time.deltaTime * fSpeedDrag);
        }

        if(bJumpPowerOn)
        {
            fJumpPowerTimerControl -= Time.deltaTime;
            if(fJumpPowerTimerControl <= 0)
            {
                fJumpPowerTimerControl = fJumpPowerTimer;
                bJumpPowerOn = false;
            }
        }
        else
        {
            rbPlayer.velocity = new Vector2(fHorizontalVelocity, rbPlayer.velocity.y);
        }
        

        if (!facingRight && fHorizontalVelocity > 0)
        {
            FlipX();
        }
        else if (facingRight && fHorizontalVelocity < 0)
        {
            FlipX();
        }
    }

    private void Jump()
    {
        fGroundedRefControl -= Time.deltaTime;
        if (bGrounded)
        {
            fGroundedRefControl = fGroundedControl;
        }

        fJumpRefControl -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            fJumpRefControl = fJumpControl;

        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rbPlayer.velocity.y > 0)
            {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * fJumpCapControl);
            }
        }

        if ((fJumpRefControl > 0) && (fGroundedRefControl > 0))
        {
            fJumpRefControl = 0;
            fGroundedRefControl = 0;
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, fJumpVelocity);
        }

        //Suspension
        if (rbPlayer.velocity.y >= -fSuspensionRange && rbPlayer.velocity.y <= fSuspensionRange && !bGrounded)
        {
            bGravitySwap = true;
        }
        if (bGravitySwap)
        {
            rbPlayer.gravityScale = fSuspensionGravity;
            fSuspensionTimeControl -= Time.deltaTime;
            if (fSuspensionTimeControl <= 0)
            {
                rbPlayer.gravityScale = fNormalGravity;
                fSuspensionTimeControl = fSuspensionTime;
                bGravitySwap = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Fountain")
        {
            
            if (Input.GetButtonDown("Ball"))
            {
                currentPower = other.GetComponent<Fountain>().power;
                print(currentPower);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(v2GroundedPositionControl, v2GroundedScaleControl);
    }
}

