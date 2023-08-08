using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FunctionsAndConstants
{
    public static Vector2 GetVector2FromVector3(Vector3 posV3)
    {
        return new Vector2(posV3.x, posV3.y);
    }

    public static Vector3 GetVector3FromVector2(Vector2 posV2)
    {
        return new Vector3(posV2.x, posV2.y, 0f);
    }
}