using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorControlPanel : MonoSingleton<ActorControlPanel>
{
    public ActorControlItem actorControlPrefab;
    public Transform spawnPoint;
    public void AddActorControlPrefab(Ally act)
    {
        ActorControlItem prfb = Instantiate(actorControlPrefab, spawnPoint);
        prfb.image.sprite = act.ClassSprite;
        prfb.Name.text = act.ActName.ToString();
        prfb.Healt.text = act.currentHealth.ToString();
        prfb.Hunger.text = act.Hunger.ToString();
        prfb.currentState.text = act.currentState.ToString();

    }
    public void RefreshActorControlPrefab(Ally act , ActorControlItem item)
    {
        item.image.sprite = act.ClassSprite;
        item.Name.text = act.ActName.ToString();
        item.Healt.text = act.currentHealth.ToString();
        item.Hunger.text = act.Hunger.ToString();
        item.currentState.text = act.currentState.ToString();
    }
}
