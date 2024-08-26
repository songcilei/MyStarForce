using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using Random = System.Random;

namespace StarForce
{
    public static class TableUtility 
    {


        public static int GetRandomArrayID(Array array)
        {
            int random = Utility.Random.GetRandom(array.Length);
            return random;
        }

        public static int GetRandomListID(ArrayList list)
        {
            int random = Utility.Random.GetRandom(list.Count);
            return random;
        }

    }
}

