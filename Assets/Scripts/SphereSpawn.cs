using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour
{
    public float radius;

    public GameObject AgentToSpawn;
    public int amountToSpawn;

	private void Start()
	{
		amountToSpawn = Random.Range(amountToSpawn - 4, amountToSpawn + 4);
		for (int i = 0; i < amountToSpawn; i++)
		{
			Instantiate(original: AgentToSpawn, new Vector3(Random.Range(transform.position.x - radius, transform.position.x + radius), Random.Range(transform.position.y - radius, transform.position.y + radius), Random.Range(transform.position.z - radius, transform.position.z + radius)), transform.rotation);
		}
	}
}
