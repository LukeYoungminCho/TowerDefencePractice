using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color impossibleToBuildColor = Color.red;
    public Vector3 towerPositionOffset;

    [Header("Optional")]
    public GameObject tower;

    private Renderer rend; // keyward renderer 와 Renderer rend 로 정의하는것의 차이
    private Color startColor;
    private BuildTowerManager buildTowerManager;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildTowerManager = BuildTowerManager.instance;
    }

    // Check Building tower is enable, 
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // to avoid node is available even shop button is position on the node
            return;

        if (!buildTowerManager.IsTowerToBuildSelected)
            return;

        buildTowerManager.BuildTowerOn(this);

    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // to avoid node is available even shop button is position on the node
            return;

        if (!buildTowerManager.IsTowerToBuildSelected)
            return;

        if (!buildTowerManager.IsAffordableToBuildTower)
        {
            rend.material.color = impossibleToBuildColor;
            return;
        }
        
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public Vector3 GetBuildTowerPosition()
    {
        return transform.position + towerPositionOffset;
    }
}
