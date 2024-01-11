using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField] Collider2D coll;
    [SerializeField] GameObject player;

    ContactFilter2D filter2D;
    Vector3 mousePos;

    bool attack;
    [SerializeField] float damage = 1;
    [SerializeField] float cooldownTime = 0.9f;
    float timer;
    public float knockbackForce;

    [SerializeField] healthSystem healthScript;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        filter2D = new ContactFilter2D
        {
            useTriggers = true,
            useLayerMask = true,
        };
        filter2D.SetLayerMask(LayerMask.GetMask("Enemy"));

        attack = false;
    }
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 0.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (timer <= 0f)
        {
            if (coll.OverlapPoint(mousePos))
            {
                cursorTexture.cursor.setAttack();

                if (Input.GetMouseButtonDown(0))
                {
                    attack = true;
                }
            }
            else
            {
                cursorTexture.cursor.setDefault();
            }
        }
        else
        {
            cursorTexture.cursor.setDefault();
            timer -= Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (attack == true)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            var pos = player.transform.position;
            Vector2 dir = mousePos - pos;

            healthScript.setDamageble(false);

            int totalHits = Physics2D.Raycast(pos, dir, filter2D, hits);

            for (int i = 0; i < totalHits; i++)
            {
                RaycastHit2D hit = hits[i];

                Rigidbody2D rig = hit.rigidbody;
                healthSystem healthComponent = hit.collider.gameObject.GetComponent<healthSystem>();

                Vector2 enemyDir = (hit.transform.position - mousePos).normalized;
                rig.AddForce(enemyDir * knockbackForce, ForceMode2D.Impulse);

                if (healthComponent)
                {
                    healthComponent.ChangeHealth(-damage);
                }
            }
            
            player.transform.position = (Vector2)mousePos;
            animManag.anim.isAttackRight(pos.x, mousePos.x);
            animManag.anim.setAttackTrue();

            timer = cooldownTime;
            attack = false;
        }
    }
}
