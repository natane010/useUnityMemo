using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// What component to use(state) (最初は気にしないでいい範囲)
/// </summary>
public enum ProgramsUsedPlayer
{
    RigidBody,
    TransForm,
    CharactorController,
    NaviMesh,
}
/// <summary>
/// プレイヤーの状態、アニメーション管理するときに便利
/// </summary>
public enum PlayerState
{
    Idle = 0,
    Move = 1,
    Attack = 10,
    Jump = 2,
}

/// <summary>
/// プレイヤーの機能を管理するプログラム
/// ここでプレイヤーがどの機能を持っているのか決める
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// ほかで書き換えられるようにしてるだけ気にしないで
    /// トランスフォームのジャンプは一応ジャンプスクリプトに例を残しますが今回は使いません
    /// </summary>
    [Header("何で動かすか選択.TransFormで動かす場合はジャンプ方法がたくさんあるみたいなので使いやすいものでジャンプさせてください※NaviMeshは未追加"), SerializeField]
    private ProgramsUsedPlayer movePlayerState;

    public ProgramsUsedPlayer MovePlayerState
    {
        get { return movePlayerState; }
        set { movePlayerState = value; }
    }

    private PlayerState playerState;

    public PlayerState player
    {
        get { return playerState; }
        set { playerState = value; }
    }


    /// <summary>
    /// 移動するかどうか
    /// </summary>
    [Header("機能をつける")]
    [SerializeField] bool onMove = false;
    [SerializeField] bool onAnimator = false;
    /// <summary>
    /// ジャンプするか(Navimeshの場合ジャンプは難しいので、最初はできないと思ってください)
    /// </summary>
    [Header("navimeshの場合はジャンプできないと思ってください（方法はありますがここでは未対応）")]
    [SerializeField]
    bool onJump = false;
    /// <summary>
    /// 攻撃するか
    /// </summary>
    [SerializeField, Header("bulletのオブジェクトを入れないと何もしません")]
    bool onAttack = false;
    //------------------------------------------------------
    Move move = null;
    Jump jump = null;
    Attack attack = null;
    //------------------------------------------------------
    [Header("オブジェクトの設定"), SerializeField]
    GameObject bullet = null;


    public Rigidbody playerRb = null;

    [Header("移動スピードやジャンプ力の設定"), SerializeField, Range(0.0f, 1000.0f)]
    float moveSpeed;
    [Header("キャラクターコントローラーは移動速度でジャンプの管理もしています")]
    [SerializeField, Range(0.0f, 1000.0f)]
    float jumpPow;

    [Header("何を入力したら移動するか"), SerializeField]
    KeyCode up;
    [SerializeField]
    KeyCode down;
    [SerializeField]
    KeyCode right;
    [SerializeField]
    KeyCode left;
    [SerializeField]
    KeyCode jumpKey;

    //--------------------------------------
    Vector3 moveDir = Vector3.zero;

    public bool bGround;

    [Header("CharactorControllerで使う重力の大きさ"), SerializeField, Range(0.0f, 100.0f)]
    private float gravity = 20.0f;
    /// <summary>
    /// キャラクターコントローラーの宣言
    /// </summary>
    private CharacterController characterController;
    //--------------------------------------------
    //navimesh
    TerrainNaviMeshBake terrainNavi;
    /// <summary>
    /// 移動予定値に置くオブジェクト
    /// </summary>
    GameObject targetObj;
    Vector3 targetObjPosition;
    NavMeshSurface navMeshSurface;
    NavMeshAgent navMeshAgent;
    Animator navmeshAnimator;
    [Header("navimeshの数値")]
    [SerializeField, Range(0.1f, 100.0f)] float navMeshFloat;
    //--------------------------------------------

    /// <summary>
    /// あんまり使わないほうがいいので気にしないでください
    /// </summary>
    private void Awake()
    {
        if (movePlayerState == ProgramsUsedPlayer.NaviMesh)
        {
            terrainNavi = this.gameObject.AddComponent<TerrainNaviMeshBake>();
            navMeshAgent = this.gameObject.AddComponent<NavMeshAgent>();
            navMeshSurface = this.gameObject.AddComponent<NavMeshSurface>();
            terrainNavi.SetNavMeshComponent();
            if (onAnimator)
            {
                navmeshAnimator = this.gameObject.AddComponent<Animator>();
                navmeshAnimator.SetFloat("speed", 0f);
            }
        }
    }
    /// <summary>
    /// 最初に一回だけ行われる
    /// </summary>
    void Start()
    {
        playerRb = this.gameObject.AddComponent<Rigidbody>();
        if (onMove)
        {
            move = this.gameObject.AddComponent<Move>();
        }
        if (onJump)
        {
            jump = this.gameObject.AddComponent<Jump>();
        }
        if (onAttack)
        {
            attack = this.gameObject.AddComponent<Attack>();
        }
        if (movePlayerState == ProgramsUsedPlayer.CharactorController)
        {
            Destroy(playerRb);
            characterController = this.gameObject.AddComponent<CharacterController>();
        }
        if (movePlayerState == ProgramsUsedPlayer.NaviMesh)
        {
            terrainNavi.SetNavMesh();
        }
        
    }

    /// <summary>
    /// 難しく言うと毎フレーム行われる処理を書く場所 Unityでは基本的に計算をここでやる,
    /// ずっと繰り返し行う
    /// </summary>
    void Update()
    {
        if (onMove)
        {
            //毎フレーム入力を受け付けて方向を取得する
            if (moveDir != Vector3.zero)
            {
                moveDir = Vector3.zero;
            }
            if (Input.GetKey(up))
            {
                moveDir.z += 1;
            }
            if (Input.GetKey(down))
            {
                moveDir.z -= 1;
            }
            if (Input.GetKey(right))
            {
                moveDir.x += 1;
            }
            if (Input.GetKey(left))
            {
                moveDir.x -= 1;
            }
        }
        if (onAttack)
        {
            attack.NoAnimationAttack(this.gameObject, bullet);
        }
        if (movePlayerState == ProgramsUsedPlayer.NaviMesh)
        {
            targetObjPosition = (this.gameObject.transform.position + moveDir) * navMeshFloat;
        }
    }
    /// <summary>
    /// Updateの後に必ず来る処理 基本的にここで物理的な処理を行うことが多い
    /// </summary>
    void FixedUpdate()
    {
        if (Input.GetKeyDown(jumpKey) && onJump)
        {
            if (movePlayerState == ProgramsUsedPlayer.RigidBody)
            {
                jump.JumpRb(jumpPow, playerRb, bGround);
            }
            else if (movePlayerState == ProgramsUsedPlayer.CharactorController && characterController.isGrounded)
            {
                moveDir += jump.JumpCharacterController(characterController);
            }
            else
            {
                Debug.Log("エラー");
            }
        }
        if (onMove)
        {
            if (movePlayerState == ProgramsUsedPlayer.RigidBody)
            {
                move.MoveRigidBody(moveDir, moveSpeed, playerRb);
            }
            else if (movePlayerState == ProgramsUsedPlayer.TransForm)
            {
                move.MoveTransform(moveDir, this.gameObject, moveSpeed);
            }
            else if (movePlayerState == ProgramsUsedPlayer.CharactorController)
            {
                //CharacterControllerでは重力適応されないため重力をかけてあげなければならない
                if (!characterController.isGrounded)
                {
                    moveDir.y -= gravity * Time.deltaTime;
                }

                move.PlayerCharacterController(characterController, moveDir, moveSpeed);
            }
            else if (movePlayerState == ProgramsUsedPlayer.NaviMesh)
            {
                move.PlayerNavMesh(navMeshAgent, targetObjPosition, navmeshAnimator, moveSpeed);
            }
        }
    }

    /// <summary>
    /// updateの後に1回来るところ,
    /// あんまり使ってるところ見てないけど、カメラとかに使ってる人いるかも
    /// </summary>
    private void LateUpdate()
    {

    }

    /// <summary>
    /// 何かに当たった時に一回行われる処理
    /// </summary>
    /// <param name="collision">当たったものが格納される</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            bGround = true;
        }
    }
    /// <summary>
    /// 何かから離れたときに一回だけ呼ばれる処理
    /// </summary>
    /// <param name="collision">離れたものが格納される</param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            bGround = false;
        }
    }
}
