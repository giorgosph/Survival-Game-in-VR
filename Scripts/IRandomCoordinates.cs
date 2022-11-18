using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RandomCoordinates
{
    public class RandomCoords 
    {
        public static Vector3 RandomCoordinates(float minRangeX, float maxRangeX, float minRangeZ, float maxRangeZ)
        {
            Vector3 randCoords;
            float randX, randZ;

            randX = Random.Range(minRangeX, maxRangeX);
            randZ = Random.Range(minRangeZ, maxRangeZ);

            randCoords = new Vector3(randX, 0, randZ);
            randCoords.y = Terrain.activeTerrain.SampleHeight(randCoords) + 1f;

            return randCoords;
        }
    }
}
