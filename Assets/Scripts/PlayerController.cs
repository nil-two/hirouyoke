using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private bool hasControl;
    private Vector3 screenMinPos;
    private Vector3 screenMaxPos;

    void Start()
    {
        hasControl = true;
        screenMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        screenMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
    }

    void Update()
    {
        if (!hasControl)
        {
            return;
        }

        var dx = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        var dy = Input.GetAxisRaw("Vertical")   * speed * Time.deltaTime;
        transform.Translate(dx, dy, 0f);

        transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, screenMinPos.x, screenMaxPos.x),
                Mathf.Clamp(transform.position.y, screenMinPos.y, screenMaxPos.y),
                0f);
    }

    public void RemoveControl()
    {
        if (!hasControl)
        {
            return;
        }

        hasControl = false;

        Rigidbody2D rigidbody2d = GetComponent<Rigidbody2D>();
        Destroy(rigidbody2d);
    }
}
