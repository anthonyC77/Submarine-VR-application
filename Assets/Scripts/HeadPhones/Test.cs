using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals(Names.MAILLOCHE))
        {
            var name = this.name;
            int nb = int.Parse(name.Replace(Names.HEADPHONE, string.Empty));
            CommandManager.Instance.Play(nb);
        }
    }
}
