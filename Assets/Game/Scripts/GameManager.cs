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

    public void GenrateObjects()
    {
        Goal goal = Instantiate(listShapesPrefabs[Random.Range(0, listShapesPrefabs.Count)]);

        Material Mat = new Material(goal.renderer.materials[0]);
        
        goal.renderer.materials[0] = Mat;
        goal.renderer.materials[0].color = listColors[Random.Range(0, listColors.Count)];


        float posX = Random.Range(-13, 13);
        float posY = Random.Range(1.5f, 4.5f);
        float posZ = Random.Range(12, 32);

        goal.transform.position = new Vector3(posX, posY, posZ);


    }
}
