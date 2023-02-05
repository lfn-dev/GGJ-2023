using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :  Singleton<GameManager>
{
    private GameObject player = null;
    public GameObject GetPlayer(){
        if (player == null){
            player = GameObject.FindGameObjectWithTag("Player");
        }
        return player;
    }


    public void Pause(){
        Time.timeScale = 0;
    }

    public void Resume(){
        Time.timeScale = 1;
    }


    //retorna a posição mais próxima de 'reference'
    Vector3 NearestPosition(Vector3 reference, Vector3[] list){
        int nearIndex = 0;

        if(list.Length > 0){
            float smallest = float.MaxValue;
            
            for (int i = 0; i < list.Length; i++)
            {
                float dist = (reference - list[i]).sqrMagnitude;
                if(dist < smallest){
                    nearIndex = i;
                    smallest = dist;
                }
            }
        }
        else{
            return reference;
        }

        return list[nearIndex];
    }

    public Transform NearestTransform(Transform reference, Transform[] list){
        int nearIndex = 0;

        if(list.Length > 0){
            float smallest = float.MaxValue;
            
            for (int i = 0; i < list.Length; i++)
            {
                float dist = (reference.position - list[i].position).sqrMagnitude;
                if(dist < smallest){
                    nearIndex = i;
                    smallest = dist;
                }
            }
        }
        else{
            return reference;
        }

        return list[nearIndex];
    }
}
