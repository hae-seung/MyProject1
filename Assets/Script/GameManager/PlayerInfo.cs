using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : Singleton<PlayerInfo>
{
   public int Diamond { get; set; }

   private void Awake()
   {
      Diamond = 0;
   }

}
