using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirectionScript : MonoBehaviour {

	public bool useRelativeRotation = true;

    private Quaternion relativeRotation;

	void Start () {
        relativeRotation = transform.parent.localRotation;
	}
	
	void Update () {
        if (useRelativeRotation)
            transform.rotation = relativeRotation;

	}
}
