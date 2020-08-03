using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ProjectileType
{
    
    Bullet = 0,
    Laser = 1
}

[CanEditMultipleObjects]
[CustomEditor (typeof(Tower))]
public class TowerEditor : Editor
{
    float rangeProperty;
    //[Header("Projectile Type")]
    SerializedProperty projectileTypeProperty;
    //[Header("Projectile Type : Bullet")]
    float fireRateProperty;
    SerializedProperty bulletPrefabProperty;
    //[Header("Projectile Type : Laser")]
    int damageOverTimeProperty;
    float slowPercentProperty;
    SerializedProperty lineRendererProperty;
    SerializedProperty laserBeamImapctEffectProperty;
    SerializedProperty laserBeamImpactLightProperty;
    SerializedProperty buffElementsMoveSlowProperty;
    
    //[Header("Unity Setup fields")]
    string enemyTagProperty;
    SerializedProperty partToRotateProperty;
    SerializedProperty firePointProperty;
    float turnSpeedProperty;    

    private void OnEnable()
    {
        rangeProperty = serializedObject.FindProperty("range").floatValue;
        //[Header("Projectile Type : Bullet")]
        fireRateProperty = serializedObject.FindProperty("fireRate").floatValue;
        bulletPrefabProperty = serializedObject.FindProperty("bulletPrefab");// Q. TurretBullet casting 실패하면 ??
        //[Header("Projectile Type : Laser")]
        damageOverTimeProperty = serializedObject.FindProperty("damageOverTime").intValue;
        slowPercentProperty = serializedObject.FindProperty("slowPercent").floatValue;
        lineRendererProperty = serializedObject.FindProperty("lineRenderer");
        laserBeamImapctEffectProperty = serializedObject.FindProperty("laserBeamImapctEffect");
        laserBeamImpactLightProperty = serializedObject.FindProperty("laserBeamImpactLight");
        buffElementsMoveSlowProperty = serializedObject.FindProperty("buffElementsMoveSlow");
        //[Header("Unity Setup fields")]
        enemyTagProperty = serializedObject.FindProperty("enemyTag").stringValue;
        partToRotateProperty = serializedObject.FindProperty("partToRotate");
        firePointProperty = serializedObject.FindProperty("firePoint");
        turnSpeedProperty = serializedObject.FindProperty("turnSpeed").floatValue;

    }
    
    public override void OnInspectorGUI()
    {
        
        serializedObject.Update();
        
        EditorGUILayout.LabelField("[General]");
        EditorGUILayout.FloatField("Explosion Range by :", rangeProperty);

        EditorGUILayout.LabelField("[Projectile Type]");

        //*******************************using enum***********************************************************// 
        // Todo -> Selecting enum has a bug!! like when i select bullet, it returns laser
        //projectileType = (ProjectileType)EditorGUILayout.EnumFlagsField("Projectype Type", projectileType); //  이 방법은 enum drop box 에 everything 이라는 요소가 포함됨..
        projectileTypeProperty = serializedObject.FindProperty("projectileType");
        EditorGUILayout.PropertyField(projectileTypeProperty);
        ProjectileType projectileType = (ProjectileType)projectileTypeProperty.enumValueIndex;
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        switch (projectileType)
        {
            // projectile Bullet 
            case ProjectileType.Bullet:
                // tower using Bullet 
                EditorGUILayout.LabelField("[Projectile : Bullet]");

                EditorGUILayout.FloatField("Fire rate (Speed) by :", fireRateProperty);
                if (bulletPrefabProperty != null)
                    EditorGUILayout.ObjectField(bulletPrefabProperty);

                break;
            // projectile Laser
            case ProjectileType.Laser:
                // tower using laser
                EditorGUILayout.LabelField("[Projectile : Laser]");
                EditorGUILayout.LabelField("Damage Over Time by: ");
                EditorGUILayout.IntField(" ", damageOverTimeProperty); // Q. 줄 넘어갈때 이렇게 하는게 맞는지?
                EditorGUILayout.FloatField("Slow down by: ", slowPercentProperty);
                if (lineRendererProperty != null)
                    EditorGUILayout.ObjectField(lineRendererProperty);
                if (laserBeamImapctEffectProperty != null)
                    EditorGUILayout.ObjectField(laserBeamImapctEffectProperty);
                if (laserBeamImpactLightProperty != null)
                    EditorGUILayout.ObjectField(laserBeamImpactLightProperty);
                if (buffElementsMoveSlowProperty != null)
                    EditorGUILayout.ObjectField(buffElementsMoveSlowProperty);  
                break;
        }
        EditorGUILayout.TextField("Target tag string", enemyTagProperty);
        EditorGUILayout.ObjectField(partToRotateProperty);
        EditorGUILayout.ObjectField(firePointProperty);
        EditorGUILayout.FloatField("Turn speed by: ", turnSpeedProperty);

        serializedObject.ApplyModifiedProperties();
        
    }
}
