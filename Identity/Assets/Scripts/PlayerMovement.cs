using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    [Header("Ataque")]
    [SerializeField] float fAttackTime;
    [SerializeField] float fAttackRangeX;
    //[SerializeField] float fAttackRangeY;
    [SerializeField] int iAttackDamage;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Transform tAttackPos;
    [SerializeField] LayerMask layerPlayerAttack;
    float fAttackControl = 0;

    [Header("Knockback")]
    [SerializeField] Transform tTwistPoint;
    [SerializeField] float fKnockbackForce;

    [Header("Ball")]
    [SerializeField] GameObject goBall;
    [SerializeField] float fBallShootForce;
    [SerializeField] float fBallHitForce;
    [SerializeField] float fBallCd;    
    bool bBallOn;
    Rigidbody2D rbBall;
    float fBallCdControl = 0;

    [Header("Dash")]
    [SerializeField] float fDashTime;
    [SerializeField] float fDashForce;
    [SerializeField] float fDashEnd;
    [SerializeField] float fDashCd;
    float fDashTimeControl = 0;
    float fDashCdControl = 0;
    bool bDashing = false;
    Vector3 v3DashDirection;


    [Header("General")]
    [SerializeField] Transform tModel;
    Rigidbody2D rbPlayer;
    
    
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        rbBall = goBall.GetComponent<Rigidbody2D>();

        rbPlayer.gravityScale = fNormalGravity;
        hitEffect.SetActive(false);
        CatchBall();

        fDashTimeControl = fDashTime;
        fSuspensionTimeControl = fSuspensionTime;
    }

    void Update()
    {
        if(Input.GetAxis("Hold") == 0)
        {
            Movement();
        }

        Jump();
        Aim();
        Attack();
        LaunchBall();
        Dash();
    }

    private void FixedUpdate()
    {
        v2GroundedPositionControl = (Vector2)transform.position + new Vector2(0, -0.1f);
        v2GroundedScaleControl = new Vector2(fGroundRangeX, fGroundRangeY);
        bGrounded = Physics2D.OverlapBox(v2GroundedPositionControl, v2GroundedScaleControl, 0, layerGround);
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
    }

    public void LaunchBall()
    {
        if(bBallOn)
        {
            /*
            if(Input.GetButtonDown("Ball"))
            {
                Vector3 v3HitDirection = tAttackPos.position - transform.position;
                v3HitDirection.Normalize();

                rbBall.bodyType = RigidbodyType2D.Dynamic;
                goBall.GetComponent<CircleCollider2D>().enabled = true;
                goBall.transform.parent = null;
                rbBall.velocity = new Vector2(v3HitDirection.x, v3HitDirection.y) * fBallHitForce;
                Knockback(v3HitDirection);
                bBallOn = false;
            }
            */
        }
        else
        {
            fBallCdControl -= Time.deltaTime;

            if (fBallCdControl <= 0)
            {
                if (Input.GetButtonDown("Ball"))
                {
                    CatchBall();
                    fBallCdControl = fBallCd;
                }
            }
        }
    }

    public void Dash()
    {
        fDashCdControl -= Time.deltaTime;

        if (fDashCdControl <= 0)
        {
            if (Input.GetAxis("Dash") != 0)
            {
                v3DashDirection = goBall.transform.position - transform.position;
                v3DashDirection.Normalize();
                bDashing = true;
                fDashCdControl = fDashCd;
            }
        }

        if (bDashing)
        {
            fDashTimeControl -= Time.deltaTime;
            rbPlayer.velocity = v3DashDirection * fDashForce;

            if (fDashTimeControl <= 0)
            {
                rbPlayer.velocity = rbPlayer.velocity * fDashEnd;
                fDashTimeControl = fDashTime;
                //bGravitySwap = true;
                bDashing = false;
            }
            
        }
        
    }

    void Attack()
    {
        if (fAttackControl <= fAttackTime)
        {
            fAttackControl += Time.deltaTime;
        }

        if (fAttackControl >= fAttackTime / 2)
        {
            hitEffect.SetActive(false);
        }

        if (fAttackControl >= fAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Vector3 v3HitDirection = tAttackPos.position - transform.position;
                v3HitDirection.Normalize();

                if (bBallOn)
                {
                    rbBall.bodyType = RigidbodyType2D.Dynamic;
                    goBall.GetComponent<CircleCollider2D>().enabled = true;
                    goBall.transform.parent = null;
                    rbBall.velocity = new Vector2(v3HitDirection.x, v3HitDirection.y) * fBallHitForce;
                    Knockback(v3HitDirection);
                    bBallOn = false;
                }
                else
                {
                    hitEffect.SetActive(true);
                    Collider2D[] playerAttackArea = Physics2D.OverlapCircleAll(tAttackPos.position, fAttackRangeX, layerPlayerAttack);

                    for (int i = 0; i < playerAttackArea.Length; i++)
                    {
                        if (playerAttackArea[i].gameObject.tag == "Spikes")
                        {
                            Knockback(v3HitDirection);
                        }
                        if (playerAttackArea[i].gameObject.tag == "Ball")
                        {
                            rbBall.velocity = new Vector2(v3HitDirection.x, v3HitDirection.y) * fBallHitForce;
                            Knockback(v3HitDirection);
                        }
                        if (playerAttackArea[i].gameObject.tag == "Enemy")
                        {
                            Knockback(v3HitDirection);
                        }
                    }
                    fAttackControl = 0;
                }
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
        if(bGravitySwap)
        {
            rbPlayer.gravityScale = fSuspensionGravity;
            fSuspensionTimeControl -= Time.deltaTime;
            if(fSuspensionTimeControl <= 0)
            {
                rbPlayer.gravityScale = fNormalGravity;
                fSuspensionTimeControl = fSuspensionTime;
                bGravitySwap = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(tAttackPos.position, fAttackRangeX);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(v2GroundedPositionControl, v2GroundedScaleControl);
    }
}

