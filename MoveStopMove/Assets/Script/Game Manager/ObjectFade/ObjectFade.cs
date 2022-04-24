using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private GameObject player;

    private Material material;
    private Color fadeColor;
    private bool isFade;

    private void Start()
    {
        isFade = false;
        material = _renderer.material;
        fadeColor = material.color;
    }

    private void Update()
    {
        if (isFade)
        {
            MaterialObjectFade.MakeFade(material);
            fadeColor.a = 0.5f;
        }
        else
        {
            MaterialObjectFade.MakeOpaque(material);
            fadeColor.a = 1f;
        }

        material.color = fadeColor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isFade = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isFade = false;
        }
    }
}
