using UnityEngine;
using System.Collections;
using System;

public class SystemNameComparer{

	public static int compareGameObjectNames(MonoBehaviour c1, MonoBehaviour c2) {
        return c1.gameObject.name.CompareTo(c2.gameObject.name);
    }
}
