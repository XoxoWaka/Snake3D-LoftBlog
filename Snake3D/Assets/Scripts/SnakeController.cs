using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    public List<Transform> Tails;
    [Range(0,3)]
    public float BonesDistance;
    public GameObject BonesPrefabs;
    [Range(0,4)]
    public float Speed;
    private Transform _transform;
    public UnityEvent onEat;  
    public int boneCount;
       

    private void Start()
    {
        _transform = GetComponent<Transform>();  
        
        
                
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);
        float angel = Input.GetAxis("Horizontal") * 4;
        transform.Rotate(0, angel, 0);  
        Finish();      
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float sqrDistance = BonesDistance * BonesDistance;
        Vector3 previousPosition = _transform.position;

        foreach (var bone in Tails)
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqrDistance) 
            {
                var temp = bone.position;
                bone.position = previousPosition;
                previousPosition = temp;
            }else
            {
                break;
            }
        }

        _transform.position = newPosition;   
    }
    private void OnCollisionEnter(Collision other)
    
    {
        if (other.gameObject.tag == "Food")
        {
            Destroy(other.gameObject);
            

             var bone =  Instantiate(BonesPrefabs);
             Tails.Add(bone.transform);
             boneCount++;
             
             if (onEat != null)
             {
                 onEat.Invoke();
             }
        }
    }

    private void Finish(){
        if (boneCount == 10)
        {
            SceneManager.LoadScene(0);            
        }
    }

}
