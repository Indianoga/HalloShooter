using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {
	[SerializeField]
	GameObject camera;
	[SerializeField]
	GameObject target;
	public EnemyInfo enemyInfo;
	

	[HideInInspector]
	bool damageControl;
	PlayerControl player;
	NavMeshAgent agent;
	
	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag ("Player");
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		player = camera.GetComponent<PlayerControl>();
		agent = GetComponent<NavMeshAgent>();
		StartCoroutine("Action");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator Action()
	{
		while (true)
		{
			yield return null;
			Walk();
		}
	}

	void Walk()
	{
		agent.SetDestination(target.transform.position);
	}

	private void OnTriggerStay(Collider other) 
	{
		if(other.gameObject.CompareTag("MainCamera"))
		{

			
			if(!damageControl)
			{
				StartCoroutine("Attack");
			}
			
		}			
	}
	
	IEnumerator Attack()
	{
		player.life--;
		damageControl = true;
		yield return new WaitForSeconds(2f);
		damageControl = false;
	}
}

[System.Serializable]
public class EnemyInfo
{
	public string type;
	public int life;
	public int strenght;
}
