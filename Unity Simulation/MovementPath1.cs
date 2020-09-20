using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementPath1 : MonoBehaviour
{
    //public enum PathTypes
    //{
     //   linear,
     //   loop
   // }

    //public PathTypes PathType;
    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] PathSequence;
    List<GameObject> Seq;

    public GameObject[] Rooms;
    public GameObject[] Doors;

    
    
    public void Start()
    {
        //string sentence = "J#K";
        string sentence = PlayerPrefs.GetString ("path_seq");
        char[] letters = sentence.ToCharArray();
        int x = 0;
        PathSequence = new Transform[letters.Length];
        for(var i = 0; i < letters.Length; i++)
        {
            x = (int)letters[i] - 65;
            //Debug.Log(x);
            if(i%2 == 0)
            {
                PathSequence[i] = Rooms[x].GetComponent<Transform>();
            }
            else
            {
                if((letters[i-1] == 'A' && letters[i+1] == 'C') || (letters[i-1] == 'C' && letters[i+1] == 'A'))
                    PathSequence[i] = Doors[0].GetComponent<Transform>();

                else  if((letters[i-1] == 'A' && letters[i+1] == 'E') || (letters[i-1] == 'E' && letters[i+1] == 'A'))
                    PathSequence[i] = Doors[1].GetComponent<Transform>();
                
                else  if((letters[i-1] == 'B' && letters[i+1] == 'C') || (letters[i-1] == 'C' && letters[i+1] == 'B'))
                    PathSequence[i] = Doors[2].GetComponent<Transform>();
                
                else  if((letters[i-1] == 'C' && letters[i+1] == 'H') || (letters[i-1] == 'H' && letters[i+1] == 'C'))
                    PathSequence[i] = Doors[3].GetComponent<Transform>();
                
                else  if((letters[i-1] == 'G' && letters[i+1] == 'H') || (letters[i-1] == 'H' && letters[i+1] == 'G'))
                    PathSequence[i] = Doors[4].GetComponent<Transform>();
                
                else  if((letters[i-1] == 'F' && letters[i+1] == 'G') || (letters[i-1] == 'G' && letters[i+1] == 'F'))
                    PathSequence[i] = Doors[5].GetComponent<Transform>();
                
                else  if((letters[i-1] == 'H' && letters[i+1] == 'I') || (letters[i-1] == 'I' && letters[i+1] == 'H'))
                    PathSequence[i] = Doors[6].GetComponent<Transform>();

                else  if((letters[i-1] == 'I' && letters[i+1] == 'J') || (letters[i-1] == 'J' && letters[i+1] == 'I'))
                    PathSequence[i] = Doors[7].GetComponent<Transform>();
                
                else  if((letters[i-1] == 'E' && letters[i+1] == 'J') || (letters[i-1] == 'J' && letters[i+1] == 'E'))
                    PathSequence[i] = Doors[8].GetComponent<Transform>();
                
                else  if((letters[i-1] == 'J' && letters[i+1] == 'K') || (letters[i-1] == 'K' && letters[i+1] == 'J'))
                    PathSequence[i] = Doors[9].GetComponent<Transform>();
                
                else  if((letters[i-1] == 'G' && letters[i+1] == 'K') || (letters[i-1] == 'K' && letters[i+1] == 'G'))
                    PathSequence[i] = Doors[10].GetComponent<Transform>();
                
            }

        }
    }

    public void OnDrawGizmos()
    {
        if(PathSequence == null || PathSequence.Length <2)
        {
            return; //no points
        }
        
        for(var i = 1; i < PathSequence.Length; i++)
        {
            Gizmos.DrawLine(PathSequence[i-1].position, PathSequence[i].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if(PathSequence == null || PathSequence.Length <1)
        {
            yield break;
        }

        while(true)
        {
            yield return PathSequence[movingTo];
            if(PathSequence.Length == 1)
            {
                continue;
            }
            
            if(movingTo <= 0)
            {
                movementDirection = 1;
            }
            
            else if(movingTo >= PathSequence.Length - 1)
            {
                movementDirection = 0;
                Time.timeScale = 0;
            }
            movingTo = movingTo + movementDirection;
        }
    }
}
