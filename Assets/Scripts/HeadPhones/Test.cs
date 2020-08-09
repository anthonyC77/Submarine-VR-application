using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals(TagNames.MAILLOCHE))
        {
            var name = this.name;
            int nb = int.Parse(name.Replace(TagNames.HEADPHONE, string.Empty));
            CommandManager.Instance.Play(nb);
        }
    }
}
