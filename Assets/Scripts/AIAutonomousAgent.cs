using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAutonomousAgent : AIAgent
{
    public AIPerception seekPerception = null;
    public AIPerception fleePerception = null;
    public AIPerception flockPerception = null;
	public float flockSepRadius;
    private void Update()
    {

        if (seekPerception != null )
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Seek(gameObjects[0]));
            }
        }

		if (fleePerception != null)
		{
			var gameObjects = fleePerception.GetGameObjects();
			if (gameObjects.Length > 0)
			{
				movement.ApplyForce(Flee(gameObjects[0]));
			}
		}

		if (flockPerception != null)
		{
			var gameObjects = flockPerception.GetGameObjects();
			if (gameObjects.Length > 0)
			{
				movement.ApplyForce(Cohesion(gameObjects));
				movement.ApplyForce(Alignment(gameObjects));
				movement.ApplyForce(Separation(gameObjects, flockSepRadius));
			}
		}

		//transform.position = Utilities.Wrap(transform.position, new Vector3(-55, -12, -53), new Vector3(55, 25, 53));
    }

	#region seek / flee
	private Vector3 Seek(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position; //go towards target
        return GetSteeringForce(direction);

    }

	private Vector3 Flee(GameObject target)
	{
		Vector3 direction = transform.position - target.transform.position; //go away from target
		return GetSteeringForce(direction);

	}
	#endregion

	#region flocking behaviors

	private Vector3 Alignment(GameObject[] neighbors)
	{
		Vector3 velocities = Vector3.zero;
		foreach (var neighbor in neighbors)
		{
			velocities += neighbor.GetComponent<AIAgent>().movement.Velocity;
		}

		Vector3 averageVelocity = velocities / neighbors.Length;
		return GetSteeringForce(averageVelocity);
	}

	private Vector3 Separation(GameObject[] neighbors, float radius)
	{
		Vector3 separation = Vector3.zero;

		foreach (var neighbor in neighbors)
		{
			Vector3 direction = (transform.position - neighbor.transform.position);
			if (direction.magnitude < radius)
			{
				separation += direction / direction.sqrMagnitude;
			}
		}

		return GetSteeringForce(separation);
	}

	private Vector3 Cohesion(GameObject[] neighbors)
	{
		Vector3 positions = Vector3.zero;
		foreach (var neighbor in neighbors)
		{
			positions += neighbor.transform.position;
		}
		Vector3 center = positions / neighbors.Length;
		Vector3 direction = center - transform.position;
		return GetSteeringForce(direction);
	}

	#endregion






	private Vector3 GetSteeringForce(Vector3 dir)
    {
        Vector3 desired = dir.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        return Vector3.ClampMagnitude(steer, movement.maxForce); //This is force
        
    }
}
