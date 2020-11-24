using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV3 : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float fJumpForce = 5;
    [SerializeField] float fGroundRangeX;
    [SerializeField] float fGroundRangeY;
    [SerializeField] float fJumpSecure = 0.2f;
    [SerializeField] float fGroundedSecure = 0.25f;
    [SerializeField] [Range(0, 1)] float fJumpCap = 0.5f;
    [SerializeField] float fMaxFallSpeed;
    [SerializeField] LayerMask layerGround;
    [SerializeField] bool bGROUNDJump;
    [Space(10)]
    [SerializeField] float fSuspensionRange;
    [SerializeField] float fSuspensionTime;
    [SerializeField] float fSuspensionGravity;
    [SerializeField] float fNormalGravity;
    [Space(10)]
    [SerializeField] float fAirJumpForce;
    [SerializeField] float fAirJumpMaxTime;
    Vector2 v2GroundedPositionControl;
    Vector2 v2GroundedScaleControl;
    bool bGrounded;
    bool bAirJumpDone = false;
    bool bAirJumpRDY = false;
    bool bGravitySwap = false;
    float fJumpSecureControl = 0;
    float fGroundedSecureControl = 0;
    float fSuspensionTimeControl = 0;
    float fAirJumpMinTimeControl;

    [Header("WallJump")]
    [SerializeField] float fWallJumpforce;
    [SerializeField] float fWallRangeX;
    [SerializeField] float fWallRangeY;
    [SerializeField] float fWallJumpCapMoveTime;
    [SerializeField] Vector2 v2WallJumpdir;
    [SerializeField] LayerMask layerWall;
    Vector2 v2WallDetectScale;
    bool bWallJumpDone = false;
    float fWallJumpCapMoveTimeControl = 0;
    

    [Header("Movement")]
    [SerializeField] float fSpeedDrag = 10;
    [SerializeField] [Range(0, 1)] float fMoveControl = 0.5f;
    [SerializeField] [Range(0, 1)] float fStopControl = 0.5f;
    [SerializeField] [Range(0, 1)] float fTurnControl = 0.5f;
    float fHorizontalVelocity;
    bool facingRight = true;

    [Header("Shoot&Reload")]
    [SerializeField] float fBallShootForce;
    [SerializeField] float fPlayerShootForce;
    [SerializeField] float fShootDistance;
    [SerializeField] float fShootCapMoveTime;
    [SerializeField] float fChargeShootMinTime;
    [SerializeField] float fChargeShootMaxTime;
    [SerializeField] float fShootExploitTime;
    [SerializeField] float fChargeTimeScale;
    [Space(10)]
    [SerializeField] float fBallReloadForce;
    [SerializeField] float fPlayerReloadForce;
    [SerializeField] bool bCOLLIDEOnReload;
    bool bBallOn = false;
    bool bShootDone = false;
    bool bShootRDY = false;
    bool bShootExploit = false;
    bool bReloading = false;
    bool bChargingShoot = false;
    float fShootCapMoveTimeControl = 0;
    float fChargeMinTimeControl = 0;
    float fChargeMaxTimeControl = 0;
    float fShootExploitTimeControl = 0;

    [Header("Dash")]
    [SerializeField] float fDashForce;
    [SerializeField] float fDashDistance;
    [SerializeField] float fDashCapMoveTime;
    [SerializeField] float fDashGravity;
    [SerializeField] float fDashExploitTime;
    [SerializeField] float fDashBeforeShootCd;
    bool bDashDone = false;
    bool bDashRDY = false;
    bool bDashExploit = false;
    bool bDashBeforeShootCd = false;
    float fDashCapMoveTimeControl = 0;
    float fDashExploitTimeControl = 0;
    float fDashBeforeShootCdControl = 0;
    /*
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
    */

    [Header("Ball")]
    GameObject goBall;
    GameObject goLimit;
    GameObject goBallArrow;
    Transform tBallAttackPivot;
    Ball scriptBall;
    Rigidbody2D rbBall;
    bool bBallDetecion = false;


    [Header("General")]
    [SerializeField] GameObject goPlayerArrow;
    [SerializeField] Transform tModel;
    [SerializeField] Transform tPlayerAttackPos;
    [SerializeField] Transform tPlayerAttackPivot;
    [SerializeField] LayerMask layerBall;
    [SerializeField] LayerMask layerPlatformMove;
    [SerializeField]  LayerMask layerTrajectoryHit;
    [SerializeField]  float fDistanceTrajectory = 25f;

    [HideInInspector]public  LineRenderer lineRenderer;


    //GameObject goPlatformMove;
    Rigidbody2D rbPlayer;
    Vector2 v2PlayerToBall;
    Vector2 v2BallToPlayer;
    float fPlayerBallDistance;
    float fBugTest = .5f;
    [HideInInspector] public bool bCollecting = false;
    GameObject goCurrentCollectable;
    public bool bBugTest;


    private void Awake()
    {
        
    }

    void Start()
    {
        //BallElements
        goBall = GameManager.Instance.GoBall.gameObject;
        scriptBall = goBall.GetComponent<Ball>();
        goLimit = scriptBall.goLimit;
        goBallArrow = scriptBall.goBallArrow;
        tBallAttackPivot = scriptBall.tBallAttackPivot;

        //Rigidbodies
        rbPlayer = GetComponent<Rigidbody2D>();
        rbBall = goBall.GetComponent<Rigidbody2D>();

        rbPlayer.gravityScale = fNormalGravity;


        goLimit.SetActive(false);
        goPlayerArrow.SetActive(false);
        goBallArrow.SetActive(false);

        

        //CatchBall();
        
        v2WallJumpdir.Normalize();

        fChargeShootMaxTime = (fChargeShootMaxTime * fChargeTimeScale) + fChargeShootMinTime;

        //Timers Control Setup
        fSuspensionTimeControl = fSuspensionTime;
        fWallJumpCapMoveTimeControl = fWallJumpCapMoveTime;
        fShootCapMoveTimeControl = fShootCapMoveTime;
        fChargeMinTimeControl = fChargeShootMinTime;
        fChargeMaxTimeControl = fChargeShootMaxTime;
        fDashCapMoveTimeControl = fDashCapMoveTime;
        fAirJumpMinTimeControl = fAirJumpMaxTime;
        fShootExploitTimeControl = fShootExploitTime;
        fDashExploitTimeControl = fDashExploitTime;
        fDashBeforeShootCdControl = fDashBeforeShootCd;
    }

    void Update()
    {
        if(bBugTest)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                print(rbPlayer.velocity.y);
            }

            fBugTest -= Time.deltaTime;
            if (fBugTest <= 0)
            {
                //print(bDashRDY);

                fBugTest = .5f;
            }
        }
        if (Input.GetAxis("Hold") == 0)
        {
            
        }

        WallJump();
        Jump();
        UpdatePlayerAndBallState();
        BallDetection();
        Collecting();

        if (GameManager.Instance.BSkill[0])
        {
            Shoot();
            Reload();
        }   
        if (GameManager.Instance.BSkill[2])
        {
            Dash();
        }

        //Slingshot();

        if (!facingRight && fHorizontalVelocity > 0)
        {
            FlipX();
        }
        else if (facingRight && fHorizontalVelocity < 0)
        {
            FlipX();
        }

    }

    private void FixedUpdate()
    {

        if(!bWallJumpDone && !bShootDone && !bDashDone)
        {
            Movement();
        }

        v2GroundedPositionControl = (Vector2)transform.position + new Vector2(0, -0.1f);
        v2GroundedScaleControl = new Vector2(fGroundRangeX, fGroundRangeY);
        bGrounded = Physics2D.OverlapBox(v2GroundedPositionControl, v2GroundedScaleControl, 0, layerGround);
        v2WallDetectScale = new Vector2(fWallRangeX, fWallRangeY);

        

        if (bGrounded)
        {
            bShootRDY = true;
            
            bAirJumpRDY = true;
        }
        if (bGrounded && !bDashDone)
        {
            bDashRDY = true;
        }
    }

    private void FlipX()
    {
        facingRight = !facingRight;
        Vector3 scaler = tModel.transform.localScale;
        scaler.x *= -1;
        tModel.transform.localScale = scaler;
    }
    public void SetCollectable(GameObject collectable)
    {
        goCurrentCollectable = collectable;
    }

    void Collecting()
    {
        if(bCollecting)
        {
            if(bGrounded)
            {
                goCurrentCollectable.SetActive(false);
                GameManager.Instance.GetCollectable();
                bCollecting = false;
            }
        }
    }

    public void CatchBall()
    {
        bBallOn = true;
        bBallDetecion = false;
        rbBall.velocity = Vector2.zero;
        rbBall.bodyType = RigidbodyType2D.Kinematic;
        goBall.GetComponent<CircleCollider2D>().enabled = false;
        if (!bCOLLIDEOnReload) { goBall.GetComponent<CircleCollider2D>().isTrigger = false; }
        goBall.transform.parent = transform;
        goBall.transform.position = transform.position;

        if (bReloading && !bGrounded && GameManager.Instance.BSkill[1])
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, fPlayerReloadForce);
            //rbPlayer.velocity = Vector2.up * fPlayerReloadForce;
            bReloading = false;
        }
        else if (bReloading)
        {
            bReloading = false;
        }
    }

    void UpdatePlayerAndBallState()
    {
        float fHorizontalStick = Input.GetAxis("HorizontalRightStick");
        float fVerticalStick = Input.GetAxis("VerticalRightStick");
        tPlayerAttackPivot.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(-fHorizontalStick, -fVerticalStick) * 180 / Mathf.PI);
        tBallAttackPivot.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(-fHorizontalStick, -fVerticalStick) * 180 / Mathf.PI);



        fPlayerBallDistance = Vector2.Distance(goBall.transform.position, transform.position);
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

    void Dash()
    {
        if (bDashExploit)
        {
            fDashExploitTimeControl -= Time.deltaTime;
            if (fDashExploitTimeControl <= Time.deltaTime)
            {
                fDashExploitTimeControl = fDashExploitTime;
                bDashExploit = false;
            }
        }

        if (bDashDone)
        {
            rbPlayer.gravityScale = fDashGravity;
            fDashCapMoveTimeControl -= Time.deltaTime;
            if (fDashCapMoveTimeControl <= 0)
            {
                rbPlayer.gravityScale = fNormalGravity;
                rbPlayer.velocity = Vector2.zero;
                fDashCapMoveTimeControl = fDashCapMoveTime;
                bDashDone = false;
            }
        }

        if (fPlayerBallDistance <= fDashDistance && !bBallOn)
        {
            if(!bReloading)
            {
                goLimit.SetActive(true);
            }

            if (Input.GetAxis("Dash") != 0 && !bBallOn && bDashRDY && !bReloading && !bDashExploit && !bDashBeforeShootCd)
            {
                bDashDone = true;
                bDashRDY = false;
                bDashExploit = true;
                rbBall.velocity = Vector3.zero;

                v2PlayerToBall = goBall.transform.position - transform.position;
                v2PlayerToBall.Normalize();

                float fForceToApply = fPlayerBallDistance * fDashForce;
                if(fForceToApply < 40)
                {
                    fForceToApply = 40;
                }
                rbPlayer.velocity = v2PlayerToBall * fForceToApply;
            }
        }
        else
        {
            goLimit.SetActive(false);
        }
    }

    void Shoot()
    {
        if(bShootDone)
        {
            fShootCapMoveTimeControl -= Time.deltaTime;

            if(fShootCapMoveTimeControl <= 0)
            {
                fShootCapMoveTimeControl = fShootCapMoveTime;
                bShootDone = false;
            }
        }

        if (bShootExploit)
        {
            fShootExploitTimeControl -= Time.deltaTime;
            if (fShootExploitTimeControl <= 0)
            {
                fShootExploitTimeControl = fShootExploitTime;
                bShootExploit = false;
            }
        }

        if(bDashBeforeShootCd)
        {
            fDashBeforeShootCdControl -= Time.deltaTime;

            if(fDashBeforeShootCdControl <= 0)
            {
                fDashBeforeShootCdControl = fDashBeforeShootCd;
                bDashBeforeShootCd = false;
            }
        }

        if (bChargingShoot)
        {
            fChargeMinTimeControl -= Time.deltaTime;
            fChargeMaxTimeControl -= Time.deltaTime;

            DrawTrajectory();

            if (fChargeMinTimeControl <= 0)
            {
                Time.timeScale = fChargeTimeScale;
            }
            
            if(fChargeMaxTimeControl <= 0)
            {
                goPlayerArrow.SetActive(false);
                goBallArrow.SetActive(false);
                bChargingShoot = false;
                fChargeMinTimeControl = fChargeShootMinTime;
                fChargeMaxTimeControl = fChargeShootMaxTime;
                Time.timeScale = 1;

                Vector3 v3HitDirection = tPlayerAttackPos.position - transform.position;
                v3HitDirection.Normalize();
               
                bDashBeforeShootCd = true;
                bShootDone = true;
                rbBall.bodyType = RigidbodyType2D.Dynamic;
                goBall.GetComponent<CircleCollider2D>().enabled = true;
                goBall.transform.parent = null;

                rbBall.velocity = new Vector2(v3HitDirection.x, v3HitDirection.y) * fBallShootForce;

                rbPlayer.velocity = new Vector2(-v3HitDirection.x * fPlayerShootForce, -v3HitDirection.y * fPlayerShootForce);

                if (bBallOn)
                {
                    bBallOn = false;
                }

                if (bDashDone)
                {
                    rbPlayer.gravityScale = fNormalGravity;
                    fDashCapMoveTimeControl = fDashCapMoveTime;
                    bDashDone = false;
                }
            }
        }

        if (Input.GetButtonDown("Shoot") && bShootRDY && !bShootExploit || Input.GetButton("Shoot") && bShootRDY && !bShootExploit)
        {
            if (fPlayerBallDistance <= fShootDistance)
            {
                goPlayerArrow.SetActive(true);
                goBallArrow.SetActive(true);
                bChargingShoot = true;
                bShootExploit = true;
            }
        }

        if (Input.GetButtonUp("Shoot"))
        {   
            lineRenderer.positionCount = 0;

            if (bChargingShoot)
            {
                goPlayerArrow.SetActive(false);
                goBallArrow.SetActive(false);
                bChargingShoot = false;
                fChargeMinTimeControl = fChargeShootMinTime;
                fChargeMaxTimeControl = fChargeShootMaxTime;
                Time.timeScale = 1;

                Vector3 v3HitDirection = tPlayerAttackPos.position - transform.position;
                v3HitDirection.Normalize();

                

                bDashBeforeShootCd = true;
                bShootDone = true;
                rbBall.bodyType = RigidbodyType2D.Dynamic;
                goBall.GetComponent<CircleCollider2D>().enabled = true;
                goBall.transform.parent = null;

                rbBall.velocity = new Vector2(v3HitDirection.x, v3HitDirection.y) * fBallShootForce;

                rbPlayer.velocity = new Vector2(-v3HitDirection.x * fPlayerShootForce, -v3HitDirection.y * fPlayerShootForce);

                if (bBallOn)
                {
                    bBallOn = false;
                }

                if (bDashDone)
                {
                    rbPlayer.gravityScale = fNormalGravity;
                    fDashCapMoveTimeControl = fDashCapMoveTime;
                    bDashDone = false;
                }
            }
        }
    }

    public void Reload()
    {
        if (!bBallOn)
        {
            /*
            if(bChargingReload)
            {
                fChargeReloadTimeControl -= Time.deltaTime;
                if (fChargeReloadTimeControl <= 0)
                {
                    fChargeReloadTimeControl = fChargeReloadMinTime;
                    bShootRDY = false;
                    bBallDetecion = true;
                    bReloading = true;
                    bChargingReload = false;
                }
            }
            */

            if(bReloading)
            {
                if (!bCOLLIDEOnReload) { goBall.GetComponent<CircleCollider2D>().isTrigger = true; }
                v2BallToPlayer = transform.position - goBall.transform.position;
                v2BallToPlayer.Normalize();

                rbBall.velocity = v2BallToPlayer * fBallReloadForce;
            }

            if (Input.GetButtonDown("Ball"))
            {
                //bChargingReload = true;
                bReloading = true;
                bShootRDY = false;
                bBallDetecion = true;
            }

            /*
            if (Input.GetButtonUp("Ball"))
            {
                if(fChargeReloadTimeControl > 0 && bShootRDY)
                {
                    fChargeReloadTimeControl = fChargeReloadMinTime;
                    bChargingReload = false;
                    rbBall.velocity = Vector3.zero;
                }
            }
            */
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
    }

    private void WallJump()
    {
        if(!bGrounded && Input.GetButtonDown("Jump"))
        {
            Collider2D[] wallDetect = Physics2D.OverlapBoxAll(transform.position, v2WallDetectScale, 0, layerWall);

            for (int i = 0; i < wallDetect.Length; i++)
            {
                if (wallDetect[i].GetComponent<BoxCollider2D>() == wallDetect[i].gameObject.GetComponentInParent<WallJump>().colRight)
                {
                    bWallJumpDone = true;
                    rbPlayer.velocity = new Vector2(v2WallJumpdir.x, v2WallJumpdir.y) * fWallJumpforce;
                }
                else
                {
                    bWallJumpDone = true;
                    rbPlayer.velocity = new Vector2(-v2WallJumpdir.x, v2WallJumpdir.y) * fWallJumpforce;
                }
            }
        }

        if(bWallJumpDone)
        {
            fWallJumpCapMoveTimeControl -= Time.deltaTime;

            if (fWallJumpCapMoveTimeControl <= 0)
            {
                fWallJumpCapMoveTimeControl = fWallJumpCapMoveTime;
                bWallJumpDone = false;
            }
        }
    }

    private void Jump()
    {
        if(bGROUNDJump)
        {
            fGroundedSecureControl -= Time.deltaTime;
            if (bGrounded)
            {
                fGroundedSecureControl = fGroundedSecure;
            }

            fJumpSecureControl -= Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
            {
                fJumpSecureControl = fJumpSecure;
            }

            if (Input.GetButtonUp("Jump"))
            {
                if (rbPlayer.velocity.y > 0)
                {
                    rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * fJumpCap);
                }
            }

            if ((fJumpSecureControl > 0) && (fGroundedSecureControl > 0))
            {
                fJumpSecureControl = 0;
                fGroundedSecureControl = 0;
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, fJumpForce);
            }
        }
        else
        {
            if (bAirJumpDone)
            {
                fAirJumpMinTimeControl -= Time.deltaTime;
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, fAirJumpForce);

                if (fAirJumpMinTimeControl <= 0)
                {
                    //rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, fAirJumpForce * fAirJumpCap);
                    fAirJumpMinTimeControl = fAirJumpMaxTime;
                    bAirJumpDone = false;
                }
            }
            if (Input.GetButtonDown("Jump") && bAirJumpRDY && !bWallJumpDone && !bGrounded)
            {
                bAirJumpDone = true;
                bAirJumpRDY = false;
            }

            if (Input.GetButtonUp("Jump"))
            {
                if(bAirJumpDone)
                {
                    fAirJumpMinTimeControl = fAirJumpMaxTime;
                    bAirJumpDone = false;
                }
            }
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
        
        //FallingCap
        /*
        if(rbPlayer.velocity.y <= fMaxFallSpeed)
        {
            rbPlayer.velocity = new Vector2( rbPlayer.velocity.x , fMaxFallSpeed);
        }
        */
    }

    private void DrawTrajectory(){
        //LOGICA DOTTED LINE BOLA


            if (lineRenderer != null){
                Vector3 joaquin = tPlayerAttackPos.position - transform.position;
                joaquin.Normalize();

                RaycastHit2D hit = Physics2D.Raycast(goBall.transform.position, joaquin, fDistanceTrajectory, layerTrajectoryHit);
                
                float fOldDistance = fDistanceTrajectory;

                if (hit.collider){

                    //float fDistanceBallToHit =Vector2.Distance(goBall.transform.position, hit.point);
                    //float fNewDistance = fOldDistance - fDistanceBallToHit;

                    Vector2 francisco = (new Vector2(joaquin.x, joaquin.y) - (2 * (joaquin * hit.normal.normalized)*hit.normal.normalized));

                    //RaycastHit2D hiti = Physics2D.Raycast(hit.point, francisco, 1, layerTrajectoryHit);
                    
                    lineRenderer.positionCount = 3;

                    lineRenderer.SetPosition(0, goBall.transform.position);

                    lineRenderer.SetPosition(1, hit.point);

                    lineRenderer.SetPosition(2, (francisco + francisco*1) + (hit.point) );

                                    
                    /*if (hiti.collider){

                         print(hiti.collider.name);

                        lineRenderer.positionCount = 3;

                        lineRenderer.SetPosition(0, goBall.transform.position);

                        lineRenderer.SetPosition(1, hit.point);

                        lineRenderer.SetPosition(2, hiti.point);

                    }else{
                        lineRenderer.positionCount = 3;

                        lineRenderer.SetPosition(0, goBall.transform.position);

                        lineRenderer.SetPosition(1, hit.point);

                        lineRenderer.SetPosition(2, (francisco + francisco*fNewDistance) + (hit.point) );
                    }*/
                    


                }else{
                    lineRenderer.positionCount = 2;

                    lineRenderer.SetPosition(0, goBall.transform.position);

                    lineRenderer.SetPosition(1, (joaquin + joaquin*fDistanceTrajectory) + (goBall.transform.position));
                    
                }
            }
               


                

                //Hasta AQui Mi lógica amigos
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

    

    /*
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
    */
}

