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
            
            tokens += Time.deltaTime * tokensPerSecond;

            float totalTokens = tokens;

            foreach (ObjToken obj in ObjList){
                if ((currentLevel >= obj.minLevel) && (currentLevel <= obj.maxLevel)){
                    if(totalTokens > obj.spawnCost){
                        int spawnQt = (int)(totalTokens / (int)obj.spawnCost);
                        totalTokens -= spawnQt * (int)obj.spawnCost;

                        Vector3 spawnPos = NearestSpawn(gameManager.GetPlayer().transform.position);

                        if (spawnPos != null){
                            CreateObj(spawnPos, obj.prefab, spawnQt);
                        }
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
            float dist = (reference - spawnPositions[0].position).sqrMagnitude;
            
            for (int i = 1; i < spawnPositions.Length; i++)
            {
                if((reference - spawnPositions[i].position).sqrMagnitude < dist){
                    nearIndex = i;
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
            GameObject.Instantiate(obj, pos, Quaternion.identity);
        }
    }


     //DEBUG
    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        //reachable area
        Gizmos.color = Color.blue;
        foreach (Transform pos in spawnPositions){
            Gizmos.DrawWireSphere(pos.position, 1);
        }
    }
#endif
}
