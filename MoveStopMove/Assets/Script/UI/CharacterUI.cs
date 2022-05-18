using TMPro;
using UnityEngine;
public class CharacterUI : MonoBehaviour
{
    //public Text text;
    public GameObject scoreUI;
    public Transform target;

    private Vector3 offset = new Vector3(0, 4, 0);

    private Transform scoreTf;


    private void Awake()
    {
        scoreTf = scoreUI.transform;
    }

    // Update is called once per frame
    private void Update()
    {

        float minX = 60;
        float maxX = Screen.width - minX;

        float minY = 28;
        float maxY = Screen.height - minY;

        Vector3 ScorePos = target.position + offset;

        Vector2 pos = Camera.main.WorldToScreenPoint(ScorePos);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        scoreTf.position = pos;
    }
}