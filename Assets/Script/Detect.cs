using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
	public GameObject zombie;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			//other.gameObject.SetActive(true);
			zombie.SetActive(true);
		}
	}
}
