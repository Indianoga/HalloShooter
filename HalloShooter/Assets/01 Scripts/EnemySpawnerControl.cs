using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerControl : MonoBehaviour 
{
	[SerializeField]
	GameObject[] enemyPrefab;
	[SerializeField]
	Transform[]  spawners;

	int level;
	int count = 0;
	int enemyCount;
	int safeSpawner;
	[HideInInspector]
	public int spawnerControl;

	bool waves;

	// Use this for initialization
	void Start ()
	{

		StartCoroutine("Action");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator Action()
	{	while (true)
		{
			yield return null;
			if(Input.GetButtonDown("Fire1"))break;
		}
		
		while (true)
		{
			yield return null;
			Levels();
			if(!waves)
			{
				CreatEnemy();
				StopCoroutine("WavesControlSystem");
			}
			if(waves)
			{
				NextWave();
			}
			Debug.Log(level);
		}
	}

	void Levels()
	{
		if(level == 0)
		{
			enemyCount = 2;
			safeSpawner = 1;
		}
		if(level == 1)
		{
			enemyCount = 5;
			safeSpawner = 2;
		}
	}
	void CreatEnemy()
	{
		int randS = Random.Range(0, spawners.Length);
		int randE = Random.Range(0, enemyPrefab.Length);
		
		if( spawnerControl  < safeSpawner )
		{
			if(count <= enemyCount)
			{
				GameObject newEnemy = Instantiate(enemyPrefab[randE],spawners[randS].transform.position, spawners[randS].transform.rotation) as GameObject;
				count++;
				spawnerControl++;
			}
		}

		if(count >= enemyCount)
		{
			level++;
			count = 0;
			waves = true;
		}

	}
	void NextWave()
	{
		StartCoroutine("WavesControlSystem");
	}
	IEnumerator WavesControlSystem()
	{
		yield return new WaitForSeconds(6f);
		waves = false;

	}
}
