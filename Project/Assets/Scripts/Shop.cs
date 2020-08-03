using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint turretBluePrint;
    public TowerBlueprint missileLauncherBluePrint;
    public TowerBlueprint laserBeamerBluePrint;
    
    BuildTowerManager buildTowerManager;
    
    private void Start()
    {
        buildTowerManager = BuildTowerManager.instance;
    }

    public void SelectTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildTowerManager.SellectTowerToBuild(turretBluePrint);
    }

    public void SellectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildTowerManager.SellectTowerToBuild(missileLauncherBluePrint);
    }

    public void SellectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selected");
        buildTowerManager.SellectTowerToBuild(laserBeamerBluePrint);
    }
}
