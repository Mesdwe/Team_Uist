using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : GenericSingletonClass<Player>
{
    public int rp;
    public void UpdateRP()
    {
        rp++;
    }

}
