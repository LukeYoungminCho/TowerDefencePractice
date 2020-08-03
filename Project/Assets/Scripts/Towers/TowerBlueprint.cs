using System;
using UnityEngine;

[Serializable] // 이 객체를 참조할때 (public) 으로, 이 객체를 참조한 객체의 inspector 창에 public 변수 인자를 보여주고싶으면 사용해야함.
public class TowerBlueprint
{
    public GameObject prefab;
    public int cost;
}
