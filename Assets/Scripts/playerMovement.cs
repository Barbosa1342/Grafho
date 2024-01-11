using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D rig;

    [SerializeField] float speed = 5;
    float dirX, dirY;
    float diagonalLimit = 0.7f;
    // Diagonal movement is a sum of vectors, so we need to limit it
    public float knockbackForce;
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
 

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        animManag.anim.setMovement(dirX, dirY);
    }

    void FixedUpdate()
    {
        if (!animManag.anim.getAttacking())
        {
            if (dirX != 0 && dirY != 0)
            {
                dirX *= diagonalLimit;
                dirY *= diagonalLimit;
            }

            Vector2 newVelocity = new Vector2(dirX, dirY) * speed;
            rig.velocity = Vector2.Lerp(rig.velocity, newVelocity, 0.75f);
        }
        else
        {
            rig.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Vector2 dir = (transform.position - collision.transform.position).normalized;
            rig.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
