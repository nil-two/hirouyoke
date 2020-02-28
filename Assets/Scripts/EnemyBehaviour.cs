using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed            = 2f;
    public float speedAccelerated = 4f;
    public float speedDelayed     = 0.5f;
    public GameObject target;

    private float fowardSpeed;
    private Vector3 fowardVector;
    private Vector3 screenMinPos;
    private Vector3 screenMaxPos;
    
    void Start()
    {
        fowardSpeed  = speed;
        fowardVector = (target.transform.position - transform.position).normalized;
        screenMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        screenMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
    }

    void Update()
    {
        transform.position += fowardVector * fowardSpeed * Time.deltaTime;

        if ((transform.position.x < screenMinPos.x) ||
            (transform.position.x > screenMaxPos.x) ||
            (transform.position.y < screenMinPos.y) ||
            (transform.position.y > screenMaxPos.y))
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Accelerate()
    {
        fowardSpeed  = speedAccelerated;
        fowardVector = (target.transform.position - transform.position).normalized;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0.8f, 0f, 0f, 1f);
    }

    public void Delay()
    {
        fowardSpeed  = speedDelayed;
        fowardVector = (target.transform.position - transform.position).normalized;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0f, 0f, 0.8f, 1f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target)
        {
            GameManager.instance.OnHitEvent();
            Destroy(gameObject);
            return;
        }
    }
}
