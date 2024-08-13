using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    void Move(Vector2 dir);
    void Jump();
}
