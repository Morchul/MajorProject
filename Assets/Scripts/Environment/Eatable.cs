using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : EntityComponent
{
    public float Amount;

    public override int ID => ComponentIDs.EATABLE;
}
