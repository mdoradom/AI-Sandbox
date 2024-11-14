using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/isZombieChasing")]
[Help("Checks whether the cop is near the treasure.")]
public class isZombieChasing : ConditionBase
{
    public override bool Check()
    {
        GameObject cop = GameObject.Find("Zombie");
        GameObject treasure = GameObject.Find("Treasure");

        if (cop == null || treasure == null)
        {
            Debug.LogError("Cop or Treasure not found in the scene.");
            return false;
        }

        float distance = Vector3.Distance(cop.transform.position, treasure.transform.position);
        Debug.Log("Distance between cop and treasure: " + distance);

        // Usar un rango más amplio para probar
        return distance < 20f;
    }
}
