using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera _camera;
    public Transform player;

    private Vector3 menuPosition;
    public static Vector3 ingamePosition;

    private Quaternion menuRotation;
    private Quaternion ingameRotation;
    private Quaternion shopRotation;

    void Start()
    {
        menuPosition = new Vector3(0f, 0.5f, -10f);
        ingamePosition = new Vector3(0f, 20f, -10f);

        menuRotation = Quaternion.Euler(0f, 0f, 0f);
        ingameRotation = Quaternion.Euler(60f, 0f, 0f);
        shopRotation = Quaternion.Euler(12f, 0f, 0f);

        _camera.transform.position = player.position + menuPosition;
        _camera.transform.rotation = menuRotation;

    }

    public void ChangeToShopCamera()
    {
        _camera.transform.rotation = shopRotation;
    }

    public void ChangeToMenuCamera()
    {
        _camera.transform.rotation = menuRotation;
    }

    public void LerpCamera()
    {
        StartCoroutine(Lerp(menuPosition, ingamePosition, menuRotation, ingameRotation, 1f));
    }    

    void Update()
    {
        if (GameFlowManager.Instance.gameState == GameFlowManager.GameState.gameStart)
        {
            _camera.transform.position = player.position + ingamePosition;
        }
    }

    IEnumerator Lerp(Vector3 pos1, Vector3 pos2, Quaternion rot1, Quaternion rot2, float time)
    {
        for (float t = 0f; t < time; t += Time.deltaTime)
        {
            _camera.transform.position = player.position + Vector3.Lerp(pos1, pos2, t / time);
            _camera.transform.rotation = Quaternion.Slerp(rot1, rot2, t / time);
            yield return 0;
        }
        transform.position = pos2;
        transform.rotation = rot2;
    }
}
