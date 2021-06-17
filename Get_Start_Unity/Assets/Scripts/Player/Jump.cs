using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    /// <summary>
    /// Rigidbodyでジャンプするプログラム
    /// </summary>
    /// <param name="jumpPow">ジャンプする力</param>
    /// <param name="rb">プレイヤーのRigidbody</param>
    public void JumpRb(float jumpPow, Rigidbody rb, bool bGround)
    {
        if (bGround)
        {
            rb.AddForce(0, jumpPow, 0, ForceMode.Impulse);
        }
    }
    /// <summary>
    /// トランスフォームでの実装。デバックしてないので動かないかも
    /// どうしても使いたい！無理やり位置変えるやり方がしたいんだ！
    /// ってならない限り使わないと思う。
    /// ※無理やりやる場合も重要な部分なら組み合わせをする（RB＋TF）など
    /// しかし砲弾など攻撃では線形保管を使うこともあるので一応覚えておくと便利
    /// 球面線形保管
    /// </summary>
    /// <param name="startpos">起点</param>
    /// <param name="endpos">終点</param>
    /// <param name="player">プレイヤー</param>
    /// <param name="jumpPow">ジャンプ力</param>
    public void TransFormJump(Vector3 startpos, Vector3 endpos, GameObject player, float jumpPow)
    {
        float dis = Vector3.Distance(startpos, endpos);
        float nowPos = (Time.time * jumpPow) / dis;
        player.transform.position = Vector3.Slerp(startpos, endpos, nowPos);
    }
    /// <summary>
    /// ジャンプキャラクターコントローラー
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    public Vector3 JumpCharacterController(CharacterController controller)
    {
        Vector3 jumpMove;
        jumpMove = new Vector3(0, 1, 0);
        return jumpMove;
    }
}
