using UnityEngine;
using System.Collections;
 
public class Wander : MonoBehaviour 
{ 
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;
 
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWarking = false;
  
    void Start()
    {

    }
    // Update is called once per frame
    void Update () 
    {
        if(isWandering == false)
        {
            StartCoroutine(WanderAI());            
        }
        if(isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }  
        if(isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }  
        if(isWarking == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }    
    }
 
    IEnumerator WanderAI() 
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);
        
        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWarking = true;
        yield return new WaitForSeconds(walkTime);
        isWarking = false;
        yield return new WaitForSeconds(rotateWait);
        if(rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if(rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }
}