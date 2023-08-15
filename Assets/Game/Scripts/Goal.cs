using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public string GoalType="";
    public Renderer renderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            GameManager.instance.GenrateObjects();
            Destroy(this.gameObject);
        }
    }
}
