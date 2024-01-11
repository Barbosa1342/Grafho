using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rig;
    [SerializeField] float speed = 4f;

    GameObject player;
    // Prefab cant reference scene object (in serialized field)

    [SerializeField] float damage = 1;
    [SerializeField] float cooldownTime = 2f;
    float timer;
    public float knockbackForce;

    healthSystem playerHealth;

    enemyAnimManager anim;
    Vector3 lastPos;
    int random;

    SpriteRenderer sprite;

    void Start()
    {
        anim = gameObject.GetComponent<enemyAnimManager>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerHealth = player.GetComponent<healthSystem>();
        }

        timer = 0f;
        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        var fullSpeed = speed * Time.deltaTime;
        var halfSpeed = fullSpeed / 2;
        var rotateSpeed = fullSpeed * 10;

        if (player != null)
        {
            if (player.activeSelf && !anim.getDying() && !animManag.anim.getDying())
            {
                if (timer <= 0f && playerHealth.IsDamageble)
                {
                    FollowPlayer(fullSpeed);
                }
                else
                {
                    var distance = Vector2.Distance(transform.position, player.transform.position);

                    if (distance <= 2f)
                    {
                        FollowPlayer(-halfSpeed);

                        // When enemy goes away from player it will act different
                        random = Random.Range(1, 3);
                    }
                    else if (distance >= 4f)
                    {
                        FollowPlayer(halfSpeed);
                    }
                    else
                    {
                        rig.velocity = Vector2.Lerp(rig.velocity, Vector2.zero, 0.75f);

                        if (random == 1)
                        {
                            transform.RotateAround(player.transform.position, Vector3.forward, rotateSpeed);
                            transform.rotation = Quaternion.identity;
                        }
                    }
                    timer -= Time.deltaTime;
                }
            }
            else
            {
                rig.velocity = Vector2.zero;
            }
        }
        anim.setMove(enemyMoved());
    }

    void FollowPlayer(float speed)
    {
        Vector2 targetPos = Vector2.MoveTowards(transform.position, player.transform.position, speed);
        rig.MovePosition(targetPos);
    }

    bool enemyMoved()
    {
        Vector3 move = transform.position - lastPos;
        lastPos = transform.position;

        if (move.x > 0)
        {
            sprite.flipX = false;
        }
        else if (move.x < 0)
        {
            sprite.flipX = true;
        }

        if (move.magnitude > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Vector2 dir = (transform.position - collision.transform.position).normalized;
            rig.AddForce(dir * knockbackForce, ForceMode2D.Impulse);

            if (playerHealth.IsDamageble)
            {
                healthSystem healthComponent = collision.collider.GetComponent<healthSystem>();

                if (healthComponent)
                {
                    healthComponent.ChangeHealth(-damage);
                }

                timer = cooldownTime;
            }
        }
    }
}
