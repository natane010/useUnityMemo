using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 今はただクローンをつくって攻撃するプログラム、あとでアニメーション設定して攻撃できるようにしときます。
/// </summary>
public class Attack : MonoBehaviour
{
    /// <summary>
    /// クローンを作るプログラム
    /// </summary>
    /// <param name="go">プレイヤーの位置</param>
    /// <param name="bullet">弾のプレハブ</param>
    public void NoAnimationAttack(GameObject player, GameObject bullet)
    {
        Instantiate(bullet, player.transform.position, Quaternion.identity);
    }
}
