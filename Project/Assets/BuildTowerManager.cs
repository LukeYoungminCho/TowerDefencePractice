using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerManager : MonoBehaviour
{
    public static BuildTowerManager instance; 
    // Q 정확히 개념이 이해가 안되는듯 함..어떤 객체에 BuildTowerManager 스크립트를 붙이면 그 객체는 BuildTowerManager instance를 자동으로 
    // 생성하는것이 아닌가..? 
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log(this.ToString() + " is already existed in the scene!");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;

    private TowerBlueprint towerToBuild;

    //public bool CanBuildTower { get; }

    public bool IsTowerToBuildSelected { get { return towerToBuild != null; } }
    public bool IsAffordableToBuildTower {
        get
        {
            bool isAffordable = false;
            if (PlayerStats.money >= towerToBuild.cost)
            {
                isAffordable = true;
            }
            else
            {
                Debug.Log("Not enough money to build the tower!!");
            }
            return isAffordable;
        }
    }

    public void SellectTowerToBuild(TowerBlueprint towerBluePrint)
    {
        towerToBuild = towerBluePrint; 
    }

    public void BuildTowerOn(Node node)
    {
        if (!IsAffordableToBuildTower) return;

        PlayerStats.money -= towerToBuild.cost;

        GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, node.GetBuildTowerPosition(), Quaternion.identity);
        node.tower = tower;

        GameObject buildEffectInstance = (GameObject)Instantiate(buildEffect, node.GetBuildTowerPosition(), Quaternion.identity);
        Destroy(buildEffectInstance, 5f);

        Debug.Log("Tower built! money left:" + PlayerStats.money);
    }
}
