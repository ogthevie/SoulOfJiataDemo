using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBehavior : MonoBehaviour
{
  public float timetoDestroy;
    void LateUpdate()
    {
      Destroy(this.gameObject, timetoDestroy);
    }
}
