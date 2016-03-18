using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Services
{
    public static class SortingService
    {
        static GameObject tempObj = null;
        static float temp = 0;
        static int i = 0;
        static int recursionDepth = 0;
        static SortingService()
        {
            _boxes = GameObject.FindGameObjectsWithTag("cube")
                                               .OrderBy(cube => cube.transform.position.x)
                                               .ToArray();

            _sizes = _boxes.Select(cube => cube.transform.localScale.sqrMagnitude)
                           .ToArray();
        }

        static GameObject[] _boxes;
        static float[] _sizes;

        
        public static Tuple<GameObject, GameObject> GetNextBoxesToXchng()
        {
            if (i == _boxes.Length - 1)
            {
                i = 0;
            }
            if (_sizes[i] > _sizes[i + 1])
            {
                
                recursionDepth = 0;
                temp = _sizes[i];
                _sizes[i] = _sizes[i + 1];
                _sizes[i + 1] = temp;
                tempObj = _boxes[i];
                _boxes[i] = _boxes[i + 1];
                _boxes[i + 1] = tempObj;
                i++;
                return new Tuple<GameObject, GameObject>(_boxes[i-1], _boxes[i]);
            }
            else
            {
                i++;
                recursionDepth++;
                if (recursionDepth == _boxes.Length)
                {
                    return null;
                }

                return GetNextBoxesToXchng();
            }
        }

    }

    public class Tuple<T1, T2>
    {
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
        internal Tuple(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }
}
