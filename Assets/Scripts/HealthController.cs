using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [System.Serializable]
    public class Health
    {
        public RawImage heart;
        public bool lifeUsed;
    }
    [SerializeField] private List<Health> life;

    private void Start()
    {
        EventsManager.Collided += ReduceLife;
    }

    private void ReduceLife()
    {
        int count = 0;
        for(int i = 0; i < life.Count; i++)
        {
            if(!life[i].lifeUsed)
            {
                life[i].lifeUsed = true;
                Color color = life[i].heart.material.color;
                color.a = 0;
                life[i].heart.material.color = color;
                break;
            }
        }

        for (int i = 0; i < life.Count; i++)
        {
            if(life[i].lifeUsed)
            {
                count++;
            }
        }

        if (count == life.Count)
        {
            EventsManager.Instance.PlayerDeathEvent();
        }
    }

    private void OnDestroy()
    {
        EventsManager.Collided -= ReduceLife;
    }
}
