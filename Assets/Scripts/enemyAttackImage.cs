using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyAttackImage : MonoBehaviour
{
    public float time = 0.25f;
    private float timeToLive;

    Vector3 pos;
    private void OnEnable()
    {
        setScaleBack();
        timeToLive = time;

        pos = transform.position;
    }

    private void Update()
    {
        transform.position = pos;

        growScale();

        if (timeToLive <= 0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            timeToLive -= Time.deltaTime;
        }
    }

    private void growScale()
    {
        var growRate = Time.deltaTime;
        float x = transform.localScale.x + growRate;
        float y = transform.localScale.y + growRate;
        float z = transform.localScale.z + growRate;

        //x = Mathf.Clamp(x, 0.75f, 1.25f);
        //y = Mathf.Clamp(y, 0.75f, 1.25f);
        //z = Mathf.Clamp(z, 0.75f, 1.25f);

        Vector3 scale = new Vector3(x, y, z);
        transform.localScale = scale;
    }

    private void setScaleBack()
    {
        float basicScale = 0.9f;
        Vector3 scale = new Vector3(basicScale, basicScale, basicScale);
        transform.localScale = scale;
    }
}
