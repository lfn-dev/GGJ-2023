using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [System.Serializable]
    public class ObjToken{
        [Tooltip("Object to spawn.")]
        public GameObject prefab;

        [Tooltip("Min Level required to spawn")]
        public int minLevel;

        [Tooltip("Max Level permited to spawn")]
        public int maxLevel;

        [Tooltip("The cost determines how much of this objs will be spawned [Tokens / spawnCost = quantity]")]
        public float spawnCost;
    }

    [Header("Game Settings")]
    public float startTokens;
    public float tokensPerSecond;
    public float secondsPerWave;
    public float spawnRadius;
    
    [Space]
    public Transform[] spawnPositions;
    public ObjToken[] ObjList;

    public int currentLevel {get; private set;}


    private float startTime;
    private float endTime;
    private bool isCounting = false;

    private float tokens = 0;


    GameManager gameManager;

    void Start(){
        gameManager = GameManager.Instance;


        currentLevel = 0;
        tokens = startTokens;

        StartTimer();
    }


    //Start Level
    public void StartTimer(){
        startTime = Time.time;
        isCounting = true;

        StartCoroutine(Spawner());
    }

    public float StopTimer(){
        endTime = Time.time;
        isCounting = false;

        StopAllCoroutines();

        return CurrentLevelTime();
    }

    //Returns the time the level was active 
    public float CurrentLevelTime(){
        return isCounting ? (Time.time - startTime) : (endTime - startTime);
    }

    //
    IEnumerator Spawner(){
        while(isCounting){
            
            tokens = CurrentLevelTime() * tokensPerSecond;

            float totalTokens = tokens;

            foreach (ObjToken obj in ObjList){
                if ((currentLevel >= obj.minLevel) && (currentLevel <= obj.maxLevel)){
                    if(totalTokens > obj.spawnCost){
                        int spawnQt = (int)(totalTokens / (int)obj.spawnCost);
                        totalTokens -= spawnQt * (int)obj.spawnCost;

                        Vector3 spawnPos = NearestSpawn(gameManager.GetPlayer().transform.position);

                        CreateObj(spawnPos, obj.prefab, spawnQt);
                    }
                }
            }

            currentLevel++;
            yield return new WaitForSeconds(secondsPerWave);
        }
    }


    //retorna o spawn mais próximo de 'reference'
    Vector3 NearestSpawn(Vector3 reference){
        int nearIndex = 0;

        if(spawnPositions.Length > 0){
            float smallest = float.MaxValue;
            
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                float dist = (reference - spawnPositions[i].position).sqrMagnitude;
                if(dist < smallest){
                    nearIndex = i;
                    smallest = dist;
                }
            }
        }
        else{
            return reference;
        }

        return spawnPositions[nearIndex].position;
    }

    void CreateObj(Vector3 pos, GameObject obj, int qt){
        for (int i = 0; i < qt; i++)
        {
            Vector3 offset = Random.insideUnitCircle * spawnRadius;

            GameObject.Instantiate(obj, pos + offset, Quaternion.identity);
        }
    }


     //DEBUG
    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        //reachable area
        Gizmos.color = Color.blue;
        foreach (Transform pos in spawnPositions){
            Gizmos.DrawWireSphere(pos.position, spawnRadius);
        }
    }
#endif
}
