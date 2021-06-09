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

    public Vector3 JumpCharacterController(CharacterController controller, float jumpPow)
    {
        Vector3 jumpMove;
        jumpMove = new Vector3(0, jumpPow, 0);
        return jumpMove;
    }
}
