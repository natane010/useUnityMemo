using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 動かすプログラム（ナビメッシュ、キャラコンなどは暇があったら追加しときます。）
/// </summary>
public class Move : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの位置を無理やり変えて移動するやり方
    /// </summary>
    /// <param name="moveDir">移動方向</param>
    /// <param name="player">プレイヤーのオブジェクト</param>
    /// <param name="movePow">移動力</param>
    public void MoveTransform(Vector3 moveDir, GameObject player, float movePow)
    {
        if (moveDir != Vector3.zero)//移動入力があったら行う処理
        {
            //プレイヤーの位置を移動入力された値だけ足して動かす。Time.deltaTimeはごつごつ動かないためのおまじない
            player.transform.position += moveDir * Time.deltaTime;
        }
    }
    /// <summary>
    /// プレイヤーのRigidbodyを押して移動する方法
    /// </summary>
    /// <param name="moveDir">移動方向</param>
    /// <param name="movePow">移動力</param>
    /// <param name="playerRb">プレイヤーのRigidbody</param>
    public void MoveRigidBody(Vector3 moveDir, float movePow, Rigidbody playerRb)
    {
        if (moveDir != Vector3.zero)//移動入力があったら行う処理
        {
            //物体を押して動かす。Time.deltaTimeはごつごつ動かないためのおまじない
            Vector3 movePos = moveDir * movePow * Time.deltaTime;
            playerRb.AddForce(movePos, ForceMode.Force);
        }
    }
}
