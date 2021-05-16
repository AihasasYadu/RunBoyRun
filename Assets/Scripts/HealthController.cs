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
        bool lifeReduced = false;
        int count = 0;
        for(int i = 0; i < life.Count; i++)
        {
            if(!life[i].lifeUsed && !lifeReduced)
            {
                life[i].lifeUsed = true;
                life[i].heart.gameObject.SetActive(false);
                lifeReduced = true;
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
