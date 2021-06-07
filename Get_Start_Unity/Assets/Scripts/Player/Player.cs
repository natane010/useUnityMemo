using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 最初は気にしないでいい範囲
/// </summary>
public enum PlayerToUseToMove
{
    RigidBody,
    TransForm,
    CharactorController,
    NaviMesh,
}

/// <summary>
/// プレイヤーの機能を管理するプログラム
/// ここでプレイヤーがどの機能を持っているのか決める
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// ほかで書き換えられるようにしてるだけ気にしないで
    /// </summary>
    [Header("何で動かすか選択※まだCharactorControllerとNaviMeshは未追加"), SerializeField]
    private PlayerToUseToMove movePlayerState;

    public PlayerToUseToMove MovePlayerState
    {
        get { return movePlayerState; }
        set { movePlayerState = value; }
    }

    /// <summary>
    /// 移動するかどうか
    /// </summary>
    [Header("機能をつける"), SerializeField]
    bool onMove = false;
    /// <summary>
    /// ジャンプするか
    /// </summary>
    [SerializeField]
    bool onJump = false;
    /// <summary>
    /// 攻撃するか
    /// </summary>
    [SerializeField]
    bool onAttack = false;
    //------------------------------------------------------
    Move move = null;
    Jump jump = null;
    Attack attack = null;
    //------------------------------------------------------
    [Header("オブジェクトやRigidbodyの設定"), SerializeField]
    GameObject player = null;
    [SerializeField]
    GameObject bullet = null;
    [SerializeField]
    Rigidbody playerRb = null;
    [Header("移動スピードやジャンプ力の設定"), SerializeField, Range(0.0f, 1000.0f)]
    float moveSpeed;
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

    // Start is called before the first frame update
    void Start()
    {
        if (onMove)
        {
            move = GetComponent<Move>();
        }
        if (onJump)
        {
            jump = GetComponent<Jump>();
        }
        if (onAttack)
        {
            attack = GetComponent<Attack>();
        }
    }

    /// <summary>
    /// 難しく言うと毎フレーム行われる処理を書く場所Unityでは基本的に計算をここでやる
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
            attack.NoAnimationAttack(player, bullet);
        }
    }
    /// <summary>
    /// Updateの後に必ず来る処理基本的にここで物理的な処理を行うことが多い
    /// </summary>
    void FixedUpdate()
    {
        if (Input.GetKeyDown(jumpKey) && onJump)
        {
            if (movePlayerState == PlayerToUseToMove.RigidBody)
            {
                jump.JumpRb(jumpPow, playerRb);
            }
            else if (movePlayerState == PlayerToUseToMove.TransForm)
            {
                jump.JumpTransForm(jumpPow, playerRb, player);
            }
        }
        if (onMove)
        {
            if (movePlayerState == PlayerToUseToMove.RigidBody)
            {
                move.MoveRigidBody(moveDir, moveSpeed, playerRb);
            }
            else if (movePlayerState == PlayerToUseToMove.TransForm)
            {
                move.MoveTransform(moveDir, player, moveSpeed);
            }
        }
    }
    /// <summary>
    /// 何かに当たった時に一回行われる処理
    /// </summary>
    /// <param name="collision">当たったものが格納される</param>
    private void OnCollisionEnter(Collision collision)
    {

    }
}
