using UnityEngine;

public class SwordBossHealth : BossHealthCode
{

    private SwordBossCode bossMov;

    private void Awake()
    {
        bossMov = GetComponent<SwordBossCode>();
    }
    void Update()
    {
        UpdateMethod();
        healthMonitor();
    }


    public override void StunnedState()
    {
        bossMov.stunned();
        Debug.Log("stunned");
    }

    public override void healthMonitor()
    {
        if (health <= 0)
        {
            Debug.Log("killed");
            die();
        }
    }
}
