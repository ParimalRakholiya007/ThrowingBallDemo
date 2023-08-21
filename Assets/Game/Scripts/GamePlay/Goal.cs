using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public string GoalType="";
    public Renderer renderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log(other.gameObject.name);
            for (int i = 0; i <renderer.materials.Length; i++)
            {
               // Debug.Log("Other Color =" + other.GetComponent<Renderer>().material.GetColor("_Color"));
                renderer.materials[i].SetColor("_Color", other.GetComponent<Renderer>().material.GetColor("_Color"));
                //renderer.materials[i].SetColor() = other.GetComponent<Material>().GetColor("_Color");
            }

            StartCoroutine(ObjectDestroyDeley());
           
           
           
        }
    }

    IEnumerator ObjectDestroyDeley()
    {
        yield return new WaitForSeconds(1f);

        //GameManager.instance.GenrateObjects();
        Destroy(gameObject);

    }
        
}
