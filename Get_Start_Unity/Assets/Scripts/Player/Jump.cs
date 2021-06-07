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
    public void JumpRb(float jumpPow, Rigidbody rb)
    {
        rb.AddForce(0, jumpPow, 0, ForceMode.Impulse);
    }

    /// <summary>
    /// プレイヤーの位置を無理やり変えてジャンプするプログラム
    /// </summary>
    /// <param name="jumpPow">ジャンプ力</param>
    /// <param name="rb">プレイヤーのRigidbody</param>
    /// <param name="player">プレイヤーのオブジェクト</param>
    public void JumpTransForm(float jumpPow, Rigidbody rb, GameObject player)
    {
        Vector3 holdingSpeed = rb.velocity;
        Vector3 jumpVector = Vector3.up * jumpPow;
        Vector3 moveDir = (jumpVector + holdingSpeed);
        player.transform.position += moveDir * Time.deltaTime;
    }
}
