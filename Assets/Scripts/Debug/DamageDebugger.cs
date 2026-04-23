// Script de teste
using Core.Damage.Components;
using UnityEngine;

public class DamageDebugger : MonoBehaviour
{
    private void Start()
    {
        DamageReceiver receiver = GetComponent<DamageReceiver>();
        if (receiver != null)
        {
            receiver.OnDamageTaken += (damage) =>
                Debug.Log($"{gameObject.tag} tomou {damage.Value} de dano do tipo {damage.Type}!");

            receiver.OnDeath += () =>
                Debug.Log("Morri!");
        }
    }
}