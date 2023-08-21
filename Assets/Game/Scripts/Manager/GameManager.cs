using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextAsset textJsonFile;
    public List<Color> listColors;
    public List<Goal> listShapesPrefabs;

    public List<Goal> listAllGoalsPrefabs;

    public int radius;


    private void Awake()
    {
        instance = this;
    }




    private void Start()
    {

        JSONObject data = new JSONObject(textJsonFile.text);
        print(data);

        JSONObject colorData = new JSONObject(data.GetField("ColorDatas").ToString().Trim('"'));
        JSONObject ShapeData = new JSONObject(data.GetField("ShapeDatas").ToString().Trim('"'));

        for (int i = 0; i < colorData.Count; i++)
        {
            Color color;
            ColorUtility.TryParseHtmlString(colorData[i].ToString().Trim('"'), out color);

            listColors.Add(color);
        }
        for (int i = 0; i < ShapeData.Count; i++)
        {
            for (int j = 0; j < listAllGoalsPrefabs.Count; j++)
            {
                if(listAllGoalsPrefabs[j].GoalType == ShapeData[i].ToString().Trim('"'))
                {
                    listShapesPrefabs.Add(listAllGoalsPrefabs[j]);
                }
            }
        }
        GenrateObjects();
    }

    public Color color;
    public Goal goal;
    public void GenrateObjects()
    {
        if (goal == null)
        {
            Debug.Log("GenrateObjects");
            float radius = 10f;
            Vector3 randomPos = Random.insideUnitSphere * radius;
            randomPos += transform.position;
            randomPos.y = 0f;

            Vector3 direction = randomPos - transform.position;
            direction.Normalize();

            float dotProduct = Vector3.Dot(transform.forward, direction);
            float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

            randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
            randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.z;

            var shape = listShapesPrefabs[Random.Range(0, listShapesPrefabs.Count)];
            goal = Instantiate(shape, randomPos, Quaternion.identity);
            goal.transform.position = randomPos;

            goal.gameObject.SetActive(true);


            /// color on mat
            /// 
            Color color = listColors[Random.Range(0, listColors.Count)];
            for (int i = 0; i < goal.renderer.materials.Length; i++)
            {

                //Material Mat = new Material(goal.renderer.materials[i]);
                //Debug.Log("goal Mat=" + Mat.name);
                //goal.renderer.materials[i] = Mat;
                goal.renderer.materials[i].color = color;
            }
        }
    }
      
}
