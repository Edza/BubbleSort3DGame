using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Services;
using Game.Utillities;
using System;

public class PlayerComponent : MonoBehaviour
{
    static public bool startRotate = false;

    bool nop = true;
   
    #region Public Members
    public float Speed;
    #endregion

    #region Private Members
    System.Random rnd = new System.Random();
    Rigidbody rb;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * Speed);

       // rb.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider)
        {
            if (other.gameObject.tag == "sort")
            {
                List<GameObject> cubes = GameObject.FindGameObjectsWithTag("cube")
                                                    .ToList();

                var twoCubes = cubes.Where(cube => cube.GetComponent<Renderer>().material.color == new Color(256, 0, 0, 256)).ToList();

                if(twoCubes.Count == 2)
                {
                    BoxService.XchngBoxes(twoCubes[0], twoCubes[1], (box1, box2) => 
                    {
                        box1.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
                        box2.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
                    });
                }

                //List<GameObject> cubes = GameObject.FindGameObjectsWithTag("cube")
                //                                    .ToList();

                //positions = cubes.Select(cube => cube.transform.position)
                //                 .OrderBy(vector3 => vector3.x)
                //                 .ToList(); // kāds ir datu tips positions

                //cubes = cubes.OrderBy(cube => cube.transform.localScale.sqrMagnitude)
                //             .ToList();

                ////for (int i = 0; i <= cubes.Count(); i++)
                ////{
                ////    cubes[i].transform.position = positions[i];
                ////}

                //Queue<Vector3> rinda = new Queue<Vector3>(positions);

                //cubes.ForEach((cube) => cube.transform.position = rinda.Dequeue());
                //Rotators.smallestCube = cubes[0];
            }
            else if(other.gameObject.tag == "dosort")
            {
                Tuple<GameObject, GameObject> toXchng = SortingService.GetNextBoxesToXchng();

                BoxService.XchngBoxes(toXchng.First, toXchng.Second, callback);

            }
            else if (other.gameObject.tag == "Respawn")
            {
                List<GameObject> cubes = GameObject.FindGameObjectsWithTag("cube")
                                                    .ToList();
                cubes.ForEach((cube) => cube.transform.localScale =
                                            new Vector3(0.1f, 0.1f, 0.1f) * (rnd.Next(1, 25)));
            }
            else if (other.gameObject.tag == "cube")
            {
                ILitterBox Box = new LitterBox();
                other.gameObject.GetComponent<Renderer>().material.color = new Color(256, 0, 0, 256);
                GameObject Decolor = Box.Triggered(other.gameObject);
                if (Decolor != null)
                {
                    Decolor.gameObject.GetComponent<Renderer>().material.color = new Color(256, 256, 256, 0);
                }
                //other.gameObject.GetComponent<Renderer>().material.color = new Color(0, float.MaxValue, 0, 200);
                //selectedObjects.Enqueue(other.gameObject);
                //selectedCount++;
                //if (selectedCount > 2)
                //{
                //    selectedCount--;
                //    selectedObjects.Dequeue().gameObject.GetComponent<Renderer>().material.color = new Color(256,256,256,0);
                //}
            }


                nop = false;

        }

    }


    void callback(GameObject box1, GameObject box2)
    {
        Tuple<GameObject, GameObject> toXchng = SortingService.GetNextBoxesToXchng();

        BoxService.XchngBoxes(toXchng.First, toXchng.Second, callback);
    }



    void OnTriggerExit(Collider other)
    {
        if (other is BoxCollider)
        {
            
        }
    }
}
