using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour
{
    public Text text; 
    public Transform character; 


    // Update is called once per frame
    void Update()
    {
        float minX = 84; 
        float maxX = Screen.width - minX; 

        float minY = 56; 
        float maxY = Screen.height - minY;

        Vector3 ScorePos = new Vector3(character.transform.position.x, character.transform.position.y + 4, character.transform.position.z); 

        Vector2 pos = Camera.main.WorldToScreenPoint(GetFrontVector(Camera.main, ScorePos));

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        text.transform.position = pos;
    }

    public Vector3 GetFrontVector(Camera camera, Vector3 position)
    {
        Vector4 vec = camera.worldToCameraMatrix * new Vector4(position.x, position.y, position.z, 1);
        if (vec.z < 0.0f)
        {
            return position;
        }
        else
        {
            Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1));
            return camera.cameraToWorldMatrix * m * vec;
        }
    }
}