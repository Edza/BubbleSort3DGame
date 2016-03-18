using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Rotators : MonoBehaviour
{
    Rigidbody rb;
    public static GameObject smallestCube;
    static System.Random rnd = new System.Random();
    System.DateTime SecondCounter = System.DateTime.Now;
    public float? targetZ = null;
    public float? targetX = null;
    public float? time = null;
    public event Action<Rotators, Vector3, Vector3> ReachedPoint;
    public float StartPos;

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * (rnd.Next(1, 25));
        StartPos = gameObject.transform.position.x;
    }

    void FixedUpdate()
    {
        if (targetZ != null && time != null)
        {
            float speed = targetZ.Value / time.Value;
            var vect = gameObject.transform.position;
            vect.z -= Time.fixedDeltaTime * speed;
            gameObject.transform.position = vect;

            if (targetZ == 2 && vect.z <= -targetZ)
            {
                targetZ = null;
                time = null;
                Vector3 initial = gameObject.transform.position;
                initial.z += 2;
                ReachedPoint(this, initial, gameObject.transform.position);
            }
            else if(targetZ == -2 && vect.z >= -0.7)
            {
                targetZ = null;
                time = null;
                StartPos = gameObject.transform.position.x;
                ReachedPoint(this, new Vector3(), gameObject.transform.position);
            }
        }
        MoveOnX();
    }

    void MoveOnX()
    {
        if (targetX != null && time != null)
        {
            var vect = gameObject.transform.position;

            int sign = Math.Sign(targetX.Value);
            if (targetX.Value > 0 && StartPos > 0 && targetX.Value < StartPos)
                sign = -1;
            else if (targetX.Value < 0 && StartPos < 0 && targetX.Value > StartPos)
                sign = 1;
            // 100 ir liels skaitlis
            float speed = (((Math.Max(StartPos + 100, targetX.Value + 100) - Math.Min(StartPos + 100, targetX.Value + 100))) / time.Value) * sign;

            vect.x += Time.fixedDeltaTime * speed;
            gameObject.transform.position = vect;

            if (targetX < 0)
            {
                if (targetX.Value < 0 && StartPos < 0 && targetX.Value > StartPos)
                {
                    if (vect.x > targetX)
                    {
                        targetX = null;
                        time = null;
                        targetZ = -2;
                        time = 2;
                    }
                }
                else
                {
                    if (vect.x < targetX)
                    {
                        targetX = null;
                        time = null;
                        targetZ = -2;
                        time = 2;
                    }
                }
            }
            else
            {
                if (targetX.Value > 0 && StartPos > 0 && targetX.Value < StartPos)
                {
                    if (vect.x < targetX)
                    {
                        targetX = null;
                        time = null;
                        targetZ = -2;
                        time = 2;
                    }
                }
                else
                {
                    if (vect.x > targetX)
                    {
                        targetX = null;
                        time = null;
                        targetZ = -2;
                        time = 2;
                    }
                }
            }

        }
    }
    //if (smallestCube == gameObject)
    //{
    //    Vector3 muvments = new Vector3(0, 0, 1);
    //    rb.AddForce(muvments*1000*Speed);
    //}
    //if (gameObject.tag == "pckp" && PlayerComponent.startRotate)
    //{
    //    //transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);

    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");
    //    Vector3 movement = new Vector3(moveHorizontal*2, moveVertical*2, 0.0f);
    //    //transform.Translate(movement);
    //    if (System.DateTime.Now.Second != SecondCounter.Second)
    //    {
    //        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * (rnd.Next(1000, 5000) / 1000);
    //        SecondCounter = System.DateTime.Now;
    //    }

    //}


}
