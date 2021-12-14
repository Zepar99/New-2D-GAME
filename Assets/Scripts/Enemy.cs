using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Vector3 velocity;
    public Transform transForm;
    public float distance = 5f;
    public float speed = 1f;
    Vector3 originalPosition;
    bool isGoingLeft = false;
    public float distFromStart;
    public void Start()
    {
        originalPosition = gameObject.transform.position;
        transForm = GetComponent<Transform>();
        velocity = new Vector3(speed, 0, 0);
        // _transform.Translate ( velocity.x*Time.deltaTime,0,0);

    }

    void Update()
    {
        distFromStart = transform.position.x - originalPosition.x;

        if (isGoingLeft)
        {

            if (distFromStart < -distance)
                SwitchDirection();

            transForm.Translate(velocity.x * Time.deltaTime, 0, 0);
            transform.Rotate(new Vector2(0.0f, 0.0f));
        }
        else
        {

            if (distFromStart > distance)
                SwitchDirection();

            transForm.Translate(velocity.x * Time.deltaTime, 0, 0);
        }

    }

    void SwitchDirection()
    {
        isGoingLeft = !isGoingLeft;
        transform.Rotate(new Vector2(0.0f, 180.0f));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<movement>() != null)
        {
            movement move = collision.gameObject.GetComponent<movement>();
            move.KillPLayer();
            Destroy(gameObject);

        }

    }
}


