using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath1 : MonoBehaviour
{

    public enum MovementType
    {
        MoveTowards,
        LerpTowards
    }

    public MovementType Type = MovementType.MoveTowards;
    public MovementPath1 MyPath;
    public float Speed = 1;
    public float MaxDistanceToGoal = 0.1f;

    private IEnumerator<Transform> pointInPath;

    public float direction_x;
    public float direction_z;

    public TextMesh Text;


    public void Start()
    {
        MyPath.Start();
        if(MyPath == null)
        {
            Debug.LogError("Movmement path cannot be null, Path must exist", gameObject);
            return;
        }

        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();

        if(pointInPath.Current == null)
        {
            Debug.LogError("Path must contain point to be followed", gameObject);
            return;
        }

        transform.position = pointInPath.Current.position;
    }

    private IEnumerator WaitAndErase(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Text.text = "";   
    }

    public void Update()
    {
        
        if(pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        if(Type == MovementType.MoveTowards)
        {
           

            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
            
            //Debug.Log(pointInPath.Current.position);
            //Debug.Log(transform.position);
            if(Vector3.Distance(transform.position,  pointInPath.Current.position) < 0.1f)
            {
                //It is within ~0.1f range, do stuff
                if(pointInPath.Current.name[0] != 'd')
                    {
                        if(pointInPath.Current.name == "K" || pointInPath.Current.name == "K(1)")
                            Text.text = "You have reached the Exit";
                        else
                            Text.text = "Now in Room "+ pointInPath.Current.name;
                    } 

                StartCoroutine(WaitAndErase(2.0f));                   
            }
            


         /*   
        if(transform.position == pointInPath.Current.position)
        {
            Debug.Log(pointInPath.Current.position);
        }*/

            direction_x = -(transform.position.x - pointInPath.Current.position.x);
            direction_z = -(transform.position.z - pointInPath.Current.position.z);
            //var offset = 0.1f;
            /*
            if(direction_x > 0 && direction_y == 0)
            {
                Debug.Log("East");
            }
            else if(direction_x < 0 && direction_y == 0)
            {
                Debug.Log("West");
            }
            else if(direction_y > 0 && direction_x == 0)
            {
                Debug.Log("North");
            }
            else if(direction_y < 0 && direction_x == 0)
            {
                Debug.Log("South");
            }
            
            else if(direction_x > 0 && direction_y > 0)
            {
                Debug.Log("NorthEast");
            } 
            else if(direction_x > 0 && direction_y < 0)
            {
                Debug.Log("SouthEast");
            }
            else if(direction_x < 0 && direction_y < 0)
            {
                Debug.Log("SouthWest");
            }
            else if(direction_x < 0 && direction_y > 0)
            {
                Debug.Log("NorthWest");
            }
            */
               
        }

         
        
        

        else if(Type == MovementType.LerpTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
            
        }

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if(distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }

        

       
    }

    public void QuitButton()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
