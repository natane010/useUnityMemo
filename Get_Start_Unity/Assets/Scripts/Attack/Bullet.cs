using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("弾の設定"), SerializeField]
    float bulletSpeed;

    Vector3 moveBulletDir;
    // Start is called before the first frame update
    void Start()
    {
        moveBulletDir = new Vector3(0, 0, bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //とりあえずまっすぐ飛ぶ。
        transform.localPosition += moveBulletDir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this);
    }
}
