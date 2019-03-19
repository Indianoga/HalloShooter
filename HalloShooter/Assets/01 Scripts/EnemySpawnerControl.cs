using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerControl : MonoBehaviour 
{
	[SerializeField]
	GameObject[] enemyPrefab;
	[SerializeField]
	Transform[]  spawners;
	[SerializeField]
	Text score;
	[SerializeField]
	GameObject infoButton;
	[SerializeField]
	GameObject comandsPrefab;
	int level;
	int count = 0;
	int enemyCount;
	int safeSpawner;
	[HideInInspector]
	public int spawnerControl;
	[HideInInspector]
	public int points;
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
			if(Input.GetKeyDown(KeyCode.Space))break;
		}
		infoButton.SetActive(false);
		StartCoroutine("Comand");
		while (true)
		{
			yield return null;
		
			score.text = string.Format("Score: {0} ", points.ToString("0") );
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
			
		}
	}
	IEnumerator Comand()
	{
		comandsPrefab.SetActive(true);
		yield return new WaitForSeconds(2f);
		comandsPrefab.SetActive(false);
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
