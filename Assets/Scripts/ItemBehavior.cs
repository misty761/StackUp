using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public float speed = 1f;

    bool isBeforeFall;
    bool isRight;
    bool isStacked;
    bool bitExplode;
    bool bitVelocityZero;
    readonly float limitX = 2.2f;
    ButtonDropItem buttonDropItem;
    Rigidbody rb;
    ItemSpawner itemSpawner;
    Collider myCollider;
    Vector3 posCollision;
    float posOriginalY;
    float posOriginalZ;

    // Start is called before the first frame update
    void Start()
    {
        isBeforeFall = true;
        isStacked = false;
        bitExplode = false;
        bitVelocityZero = false;
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            isRight = false;
        }
        else
        {
            isRight = true;
        }

        buttonDropItem = FindObjectOfType<ButtonDropItem>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        itemSpawner = FindObjectOfType<ItemSpawner>();
        myCollider = FindObjectOfType<Collider>();
        posOriginalY = transform.position.y;
        posOriginalZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameState.Playing) return;

        // 떨어지기 전
        if (isBeforeFall)
        {
            // 오른족으로 움직임
            if (isRight)
            {
                if (transform.position.x <= limitX)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
                else
                {
                    isRight = !isRight;
                }
                
            }
            // 왼쪽으로 움직임
            else
            {
                if (transform.position.x >= -limitX)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
                else
                {
                    isRight = !isRight;
                }
            }

            // 화면 터치
            if (buttonDropItem.isButtonDown)
            {
                isBeforeFall = false;
                rb.velocity = Vector3.zero;
            }

            // 아이템 변경 버튼 눌림
            if (itemSpawner.bitChangeItem)
            {
                itemSpawner.bitChangeItem = false;
                DestroyItem(transform.position);
            }

            // 떨어지기 전 y/z 포지션이 변하면 다시 생성(폭탄으로 물건을 깨고 나서 나온 물건이 아래로 움직임
            if (transform.position.y != posOriginalY || transform.position.z != posOriginalZ)
            {
                myCollider.enabled = false;
                itemSpawner.SpawnItem();
                Destroy(gameObject);
            }
        }
        // 떨어진 후
        else
        {
            // 중력 적용
            rb.useGravity = true;  
        }
     
    }

    private void FixedUpdate()
    {
        // 폭탄에 맞았을 때
        if (bitExplode)
        {
            DestroyItem(posCollision);
        }

        // 물건위에 쌓인 순간 속도를 0으로(쌓기 쉽도록)
        if (isStacked && !bitVelocityZero)
        {
            bitVelocityZero = true;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            Destroy(collision.gameObject);
            bitExplode = true;
            posCollision = collision.contacts[0].point;
        }
        else
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Animal"))
            {
                SoundManager.instance.PlaySound(SoundManager.instance.audioThud, Vector3.zero, 1f);
                Instantiate(EffectManager.instance.smokePuff, collision.contacts[0].point, Quaternion.Euler(Vector3.zero));
            }
            else
            {
                if (collision.contacts[0].normal.y >= 0f)
                {
                    if (!isStacked && !isBeforeFall)
                    {
                        isStacked = true;
                        SoundManager.instance.PlaySound(SoundManager.instance.audioThud, Vector3.zero, 1f);
                        Instantiate(EffectManager.instance.smokePuff, collision.contacts[0].point, Quaternion.Euler(Vector3.zero));
                        itemSpawner.SpawnItem();
                        float height = transform.position.y;
                        if (height > GameManager.instance.stackedHeight)
                        {
                            GameManager.instance.stackedHeight = height;
                        }
                        GameManager.instance.AddScore();
                    }
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //print(collision.contacts[0].normal.y);
        //if (collision.contacts[0].normal.y > 0.98f && isStacked)
        //{
            //rb.velocity = Vector3.zero;
        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (isStacked) rb.velocity = Vector3.zero;
    }

    void DestroyItem(Vector3 pos)
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioExplosion, Vector3.zero, 1f);
        Instantiate(EffectManager.instance.explosion, pos, Quaternion.Euler(Vector3.zero));
        myCollider.enabled = false;
        itemSpawner.SpawnItem();
        Destroy(gameObject);
    }

}
