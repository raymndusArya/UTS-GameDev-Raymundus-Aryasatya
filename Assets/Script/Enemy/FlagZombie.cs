using UnityEngine;

public class FlagZombie : Enemy
{
    public bool flag = true;

    public override void Serang()
    {
        Debug.Log("FlagZombie menyerang!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
