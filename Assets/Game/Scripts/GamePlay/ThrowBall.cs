using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class ThrowBall : MonoBehaviour
{

    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos;
    private Vector2 endPos;

    public float MinSwipDist = 0;
    private float BallVelocity = 0;
    private float BallSpeed = 0;
    public float MaxBallSpeed = 350;
    private Vector3 angle;

    private bool thrown, holding;
    private Vector3 newPosition, resetPos;
    Rigidbody rb;

    private Renderer renderer;

    public Color myColor;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        OnColorChanges();

         rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
        ResetBall();
    }

    private void OnMouseDown()
    {
        startTime = Time.time;
        startPos = Input.mousePosition;
        holding = true;
    }

    private void OnMouseDrag()
    {
        PickupBall();
    }

    private void OnMouseUp()
    {
        endTime = Time.time;
        endPos = Input.mousePosition;
        swipeDistance = (endPos - startPos).magnitude;
        swipeTime = endTime - startTime;

        if (swipeTime < 0.5f && swipeDistance > 30f)
        {
            //throw ball
            CalculateSpeed();
            CalculateAngle();
            rb.AddForce(new Vector3((angle.x * BallSpeed), (angle.y * BallSpeed / 3), (angle.z * BallSpeed) * 2));
            rb.useGravity = true;
            holding = false;
            thrown = true;
            Invoke("ResetBall", 4f);
        }
        else
            ResetBall();
    }

    void ResetBall()
    {

        OnColorChanges();
        GameManager.instance.GenrateObjects();
        angle = Vector3.zero;
        endPos = Vector2.zero;
        startPos = Vector2.zero;
        BallSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;
        thrown = holding = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = resetPos;
    }

    void PickupBall()
    {
        rb.isKinematic = false;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 5f;
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 80f * Time.deltaTime);
    }


    private void CalculateAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, (Camera.main.nearClipPlane + 5)));

    }

    void CalculateSpeed()
    {
        if (swipeTime > 0)
            BallVelocity = swipeDistance / (swipeDistance - swipeTime);

        BallSpeed = BallVelocity * 40;

        if (BallSpeed <= MaxBallSpeed)
        {
            BallSpeed = MaxBallSpeed;
        }
        swipeTime = 0;
    }

    public void OnColorChanges()
    {
        myColor = GameManager.instance.listColors[Random.Range(0, GameManager.instance.listColors.Count)];
        if (myColor != GameManager.instance.color)
        {
            renderer.material.color = GameManager.instance.listColors[Random.Range(0, GameManager.instance.listColors.Count)];
        }
    }
   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shape"))
        {
            Debug.Log(other.gameObject.name);
            GameManager.instance.GenrateObjects();
            Destroy(this.gameObject);
        }
    }*/
}