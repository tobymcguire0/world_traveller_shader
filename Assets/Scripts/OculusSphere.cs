using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusSphere : MonoBehaviour
{
    List<GameObject> objects;
    MaterialPropertyBlock propBlock;

    public GameObject visionSphere;
    [Range(0f, 1f)]
    public float speed = .1f;
    Vector3 smallSize = new Vector3(0, 0, 0);
    Vector3 bigSize = new Vector3(10, 10, 10);
    bool changingSize = false;
    bool isLargest = false;
    bool inOverworld = true;

    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        objects = new List<GameObject>();
    }
    public void addMaterial(GameObject obj)
    {
        if (!objects.Contains(obj))
        {
            objects.Add(obj);
            Debug.Log("+"+obj.name);
        }
        
    }
    public void removeMaterial(GameObject obj)
    {
        if (objects.Contains(obj))
        {
            objects.Remove(obj);
            Debug.Log("-" + obj.name);
        }
        else //Should never happen
        {
            Debug.Log("Object Not In List: " + obj.name);
        }
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (changingSize && !isLargest)
            {
                isLargest = true;
                inOverworld = true;
                gameObject.layer = 9;
            }
            else if (changingSize && isLargest)
            {
                isLargest = false;
                inOverworld = false;
                gameObject.layer = 10;
            }
            else
            {
                changingSize = true;
                if (inOverworld)
                {
                    gameObject.layer = 10;
                    inOverworld = false;
                }
                else
                {
                    gameObject.layer = 9;
                    inOverworld = true;
                }

            }
        }
        if (changingSize)
        {
            if (!isLargest)
            {
                visionSphere.transform.localScale = Vector3.Lerp(visionSphere.transform.localScale, bigSize, speed);
                if (bigSize.magnitude - visionSphere.transform.localScale.magnitude < .1f)
                {
                    visionSphere.transform.localScale = bigSize;
                    isLargest = true;
                    changingSize = false;
                }
            }
            else
            {
                visionSphere.transform.localScale = Vector3.Lerp(visionSphere.transform.localScale, smallSize, speed);
                if (visionSphere.transform.localScale.magnitude - smallSize.magnitude < .1f)
                {
                    visionSphere.transform.localScale = smallSize;
                    isLargest = false;
                    changingSize = false;
                }
            }

        }
        //Updating Shader
        for (int i = 0; i < objects.Count; i++)
        {
            Renderer render = objects[i].GetComponent<MeshRenderer>();
            if(objects[i].layer == 9)
            {                
                render.sharedMaterial.SetVector("Vector4_AA7DE966", visionSphere.transform.position);
                render.sharedMaterial.SetFloat("Vector1_5E30E5C6", visionSphere.transform.localScale.x / 2);
            } else
            {
                render.sharedMaterial.SetVector("Vector3_F782CD06", visionSphere.transform.position);
                render.sharedMaterial.SetFloat("Vector1_845F7A60", visionSphere.transform.localScale.x / 2);
            }
            
        }
    }
    private void OnApplicationQuit()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Renderer render = objects[i].GetComponent<MeshRenderer>();
            if (objects[i].layer == 9)
            {
                render.sharedMaterial.SetVector("Vector4_AA7DE966", new Vector4(0,0,0,0));
                render.sharedMaterial.SetFloat("Vector1_5E30E5C6", 0);
            }
            else
            {
                render.sharedMaterial.SetVector("Vector3_F782CD06", new Vector4(0, 0, 0, 0));
                render.sharedMaterial.SetFloat("Vector1_845F7A60", 0);
            }

        }
    }

}

//if (inOverworld)
//{
//    overworldRender.material.SetInt("_Ref", 1);
//    reverseRender.material.SetInt("_Ref", 1);
//}
//else
//{
//    overworldRender.material.SetInt("_Ref", 0);
//    reverseRender.material.SetInt("_Ref", 0);
//}