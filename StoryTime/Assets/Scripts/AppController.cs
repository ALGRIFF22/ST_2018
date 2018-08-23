using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour {


	public List<GameObject> ObjectsNotActiveOnStart;
	public List<GameObject> ObjectsActiveOnStart;

	// Use this for initialization
	void Start ()
	{
		foreach(GameObject i in ObjectsNotActiveOnStart)
		{
			i.SetActive(false);
		}
		foreach (GameObject c in ObjectsActiveOnStart)
		{
			c.SetActive(true);
		}
	}

}
