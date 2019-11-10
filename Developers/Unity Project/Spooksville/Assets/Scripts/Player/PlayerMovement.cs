using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string[] maskedLayers;
    public float speed = 1.0f;

    private BoxCollider2D boxCollider;
    private Vector3 direction;
    private RaycastHit2D hit;

    void Start()
    {
        direction = Vector3.zero;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        direction = new Vector3(x, y, 0f);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0f, new Vector2(direction.x, 0f), Mathf.Abs(direction.x * speed * Time.deltaTime), LayerMask.GetMask(maskedLayers));
        if (hit.collider == null)
        {
            transform.Translate(new Vector3(direction.x, 0f) * speed * Time.deltaTime);
        }
        //what's going on?//
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0f, new Vector2(0f, direction.y), Mathf.Abs(direction.y * speed * Time.deltaTime), LayerMask.GetMask(maskedLayers));
        if (hit.collider == null)
        {
            transform.Translate(new Vector3(0f, direction.y) * speed * Time.deltaTime);
        }
    }//big poopy
}
