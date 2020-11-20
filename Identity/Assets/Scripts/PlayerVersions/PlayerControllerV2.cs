using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    [Header("Jump")]
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

    [Header("WallJump")]
    [SerializeField] float fWallJumpforce;
    [SerializeField] float fWallRangeX;
    [SerializeField] float fWallRangeY;
    [SerializeField] float fWallJumpTime;
    [SerializeField] Vector2 v2WallJumpdir;
    [SerializeField] LayerMask layerWall;
    Vector2 v2WallDetectScale;
    float fWallJumpTimeControl = 0;
    bool bWallJump = false;

    [Header("Movement")]
    [SerializeField] float fSpeedDrag = 10;
    [SerializeField] [Range(0, 1)] float fMoveControl = 0.5f;
    [SerializeField] [Range(0, 1)] float fStopControl = 0.5f;
    [SerializeField] [Range(0, 1)] float fTurnControl = 0.5f;
    float fHorizontalVelocity;
    float fGroundedRefControl = 0;
    bool facingRight = true;

    [Header("Shoot&Reload")]
    [SerializeField] float fBallShootForce;
    [SerializeField] float fPlayerShootForce;
    [SerializeField] float fBallReloadForce;
    [SerializeField] float fPlayerReloadForce;
    [SerializeField] float fShootDoneTime;
    bool bBallOn = false;
    bool bShootDone = false;
    bool bShootRDY = false;
    bool bReloading = false;
    float fShootDoneTimeControl = 0;


    [Header("Slingshot")]
    [SerializeField] float fSlingForce;
    [SerializeField] float fSlingDistance;
    bool bSlingOn = false;
    bool bSlingDone = false;
    bool bSlingRDY = true;
    bool bSlingReloading = false;
    Vector2 v2PlayerToBall;
    Vector2 v2BallToPlayer;
    float fPlayerBallDistance;



    [Header("Ball")]
    [SerializeField] Transform tAttackPos;
    [SerializeField] Transform tTwistPoint;
    [SerializeField] GameObject goBall;
    [SerializeField] GameObject goLimit;
    [SerializeField] LayerMask layerBall;
    Rigidbody2D rbBall;
    bool bBallDetecion = false;


    [Header("General")]
    [SerializeField] Transform tModel;
    Rigidbody2D rbPlayer;

    [Header("Particles")]
    [SerializeField] ParticleSystem PsystLanded;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        rbBall = goBall.GetComponent<Rigidbody2D>();
        goLimit.SetActive(false);

        rbPlayer.gravityScale = fNormalGravity;
        CatchBall();
        fSuspensionTimeControl = fSuspensionTime;
        fWallJumpTimeControl = fWallJumpTime;
        fShootDoneTimeControl = fShootDoneTime;
        v2WallJumpdir.Normalize();
    }

    void Update()
    {
        if (Input.GetAxis("Hold") == 0)
        {
            
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            //print(fPlayerBallDistance);
        }

        
        Jump();
        WallJump();
        Aim();
        Slingshot();
        BallDetection();
        Shoot();
        Reload();
    }

    private void FixedUpdate()
    {

        if(!bSlingDone && !bWallJump && !bShootDone || bSlingReloading && !bShootDone)
        {
            Movement();
        }

        v2GroundedPositionControl = (Vector2)transform.position + new Vector2(0, -0.1f);
        v2GroundedScaleControl = new Vector2(fGroundRangeX, fGroundRangeY);
        bGrounded = Physics2D.OverlapBox(v2GroundedPositionControl, v2GroundedScaleControl, 0, layerGround);
        v2WallDetectScale = new Vector2(fWallRangeX, fWallRangeY);

        if(bGrounded)
        {
            bSlingRDY = true;
            bShootRDY = true;
            
        }
    }

    private void FlipX()
    {
        facingRight = !facingRight;
        Vector3 scaler = tModel.transform.localScale;
        scaler.x *= -1;
        tModel.transform.localScale = scaler;
    }

    public void CatchBall()
    {
        if(!bSlingOn)
        {
            bBallOn = true;
            bSlingReloading = false;
            bSlingOn = false;
            bSlingDone = false;
            bBallDetecion = false;
            rbBall.velocity = Vector2.zero;
            rbBall.bodyType = RigidbodyType2D.Kinematic;
            goBall.GetComponent<CircleCollider2D>().enabled = false;
            goBall.transform.parent = transform;
            goBall.transform.position = transform.position;
        }

        if (bReloading)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
            rbPlayer.velocity = Vector2.up * fPlayerReloadForce;
            bReloading = false;
        }
    }

    void Aim()
    {
        float fHorizontalStick = Input.GetAxis("HorizontalRightStick");
        float fVerticalStick = Input.GetAxis("VerticalRightStick");
        tTwistPoint.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(-fHorizontalStick, -fVerticalStick) * 180 / Mathf.PI);
    }

    void BallDetection()
    {
        if(bBallDetecion)
        {
            Collider2D[] colBallCatch = Physics2D.OverlapCircleAll(transform.position, 1.2f, layerBall);

            for (int i = 0; i < colBallCatch.Length; i++)
            {
                if (colBallCatch[i].gameObject == goBall)
                {
                    CatchBall();
                }
            }
        }
    }

    void Slingshot()
    {
        v2PlayerToBall = goBall.transform.position - transform.position;
        v2PlayerToBall.Normalize();
        v2BallToPlayer = transform.position - goBall.transform.position;
        v2BallToPlayer.Normalize();
        fPlayerBallDistance = Vector2.Distance(goBall.transform.position, transform.position);

        if (bSlingReloading)
        {
            rbBall.velocity = v2BallToPlayer * 40;
            bBallDetecion = true;
        }
        if(bSlingDone)
        {
            if(rbPlayer.velocity.x >= -0.5f && rbPlayer.velocity.x <= 0.5f  || rbPlayer.velocity.y >= -0.5f && rbPlayer.velocity.y <= 0.5f)
            {
                bSlingDone = false;
                bSlingReloading = true;
            }


            bSlingRDY = false;

            bBallDetecion = true;
        } 

        if (fPlayerBallDistance >= fSlingDistance || Input.GetAxis("Dash") == 0)
        {
            if(bSlingOn)
            {
                goLimit.SetActive(false);
                bSlingReloading = true;
                bSlingOn = false;
            }
        }
        else if (Input.GetAxis("Dash") != 0 && !bSlingReloading && bSlingRDY && !bReloading)
        {
            goLimit.SetActive(true);
            bSlingOn = true;
            goBall.GetComponent<CircleCollider2D>().enabled = true;
            goBall.transform.parent = null;
            rbBall.velocity = Vector2.zero;
            

            if (Input.GetButtonDown("Jump"))
            {
                goLimit.SetActive(false);
                float forceToApply = fPlayerBallDistance * fSlingForce;
                rbPlayer.velocity = v2PlayerToBall * forceToApply;
                bSlingOn = false;
                bSlingDone = true;
            }
        }
    }

    void Shoot()
    {
        if(bShootDone)
        {
            fShootDoneTimeControl -= Time.deltaTime;

            if(fShootDoneTimeControl <= 0)
            {
                fShootDoneTimeControl = fShootDoneTime;
                bShootDone = false;
            }
        }

        if (Input.GetButtonDown("Shoot"))
        {
            Vector3 v3HitDirection = tAttackPos.position - transform.position;
            v3HitDirection.Normalize();

            if (bBallOn && bShootRDY)
            {
                bShootDone = true;
                rbBall.bodyType = RigidbodyType2D.Dynamic;
                goBall.GetComponent<CircleCollider2D>().enabled = true;
                goBall.transform.parent = null;
                rbBall.velocity = new Vector2(v3HitDirection.x, v3HitDirection.y) * fBallShootForce;

                Vector3 v3PlayerPos = transform.position;
                v3PlayerPos.Normalize();
                if (v3HitDirection.y <= v3PlayerPos.y)
                {
                    rbPlayer.velocity = new Vector2(-v3HitDirection.x * fPlayerShootForce, -v3HitDirection.y * fPlayerShootForce);
                }

                bBallOn = false;
                bShootRDY = false;
            }
        }
    }

    public void Reload()
    {
        if (!bBallOn)
        {
            if (Input.GetAxis("Reload") != 0)
            {
                v2BallToPlayer = transform.position - goBall.transform.position;
                v2BallToPlayer.Normalize();

                rbBall.velocity = v2BallToPlayer * fBallReloadForce;
                bBallDetecion = true;
                bReloading = true;
            }
            else
            {
                bReloading = false;
            }
        }
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

        rbPlayer.velocity = new Vector2(fHorizontalVelocity, rbPlayer.velocity.y);
        
        

        if (!facingRight && fHorizontalVelocity > 0)
        {
            FlipX();
        }
        else if (facingRight && fHorizontalVelocity < 0)
        {
            FlipX();
        }
    }

    private void WallJump()
    {
        if(!bGrounded && !bSlingDone && Input.GetButtonDown("Jump"))
        {
            Collider2D[] wallDetect = Physics2D.OverlapBoxAll(transform.position, v2WallDetectScale, 0, layerWall);

            for (int i = 0; i < wallDetect.Length; i++)
            {
                if (wallDetect[i].gameObject.GetComponent<WallJump>().colRight)
                {
                    bWallJump = true;
                    rbPlayer.velocity = new Vector2(v2WallJumpdir.x, v2WallJumpdir.y) * fWallJumpforce;
                }
                else
                {
                    bWallJump = true;
                    rbPlayer.velocity = new Vector2(-v2WallJumpdir.x, v2WallJumpdir.y) * fWallJumpforce;
                }
            }
        }

        if(bWallJump)
        {
            fWallJumpTimeControl -= Time.deltaTime;

            if (fWallJumpTimeControl <= 0)
            {
                fWallJumpTimeControl = fWallJumpTime;
                bWallJump = false;
            }
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
        if (!bSlingDone && Input.GetButtonDown("Jump"))
        {
            fJumpRefControl = fJumpControl;

        }

        if (!bSlingDone && Input.GetButtonUp("Jump"))
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

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(v2GroundedPositionControl, v2GroundedScaleControl);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, v2WallDetectScale);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.2f);
    }
}

