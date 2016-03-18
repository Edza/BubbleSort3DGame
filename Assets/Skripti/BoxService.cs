using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Services
{
    public static class BoxService
    {
        static Action<GameObject, GameObject> _callback;

        public static void XchngBoxes(GameObject box1, GameObject box2, Action<GameObject, GameObject> callback)
        {
            _callback = callback;

            var box1obj = box1.GetComponent<Rotators>();
            box1obj.targetZ = 2;
            box1obj.time = 2;
            box1obj.ReachedPoint += Obj_ReachedPoint;

            var box2obj = box2.GetComponent<Rotators>();
            box2obj.targetZ = 2;
            box2obj.time = 2;
            box2obj.ReachedPoint += Obj_ReachedPoint;
        }

        static bool oneReached;
        static Vector3 cube1PosCur;
        static Vector3 cube1PosInit;
        static Rotators rotate1;

        static void Obj_ReachedPoint(Rotators caller, Vector3 arg1, Vector3 arg2)
        {
            caller.ReachedPoint -= Obj_ReachedPoint;
             
            if (!oneReached)
            {
                cube1PosInit = arg1;
                cube1PosCur = arg2;
                rotate1 = caller;
                oneReached = true;
            }
            else
            {
                BothReached(rotate1, caller, cube1PosInit, cube1PosCur, arg1, arg2);
                oneReached = false;
            }
        }

        static bool secondTime = false;

        static void BothReached(Rotators rot1, Rotators rot2, Vector3 cube1intial, Vector3 cube1now, Vector3 cube2intial, Vector3 cube2now)
        {
            if(!secondTime)
            {
                rot1.ReachedPoint += Obj_ReachedPoint;
                rot2.ReachedPoint += Obj_ReachedPoint;
                rot1.targetX = cube2intial.x;
                rot1.time = 2;
                rot2.targetX = cube1intial.x;
                rot2.time = 2;
            }

            bool wasSecondTime = secondTime;
            
            secondTime = !secondTime;

            if (wasSecondTime)
                _callback(rot1.gameObject, rot2.gameObject);
        }
    }
}
