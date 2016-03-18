using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

interface ILitterBox
{
    // padod iekšā to objektu kurām pieskārās
    // atgriež to kas tika izmests ārā vai null
    GameObject Triggered(GameObject element);
}
