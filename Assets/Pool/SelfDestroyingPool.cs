using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelfDestroyingPool<T> : Pool<T> where T : PoolObject
{

}
