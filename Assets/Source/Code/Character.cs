﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DDDClasses
{
    public class Character
    {
        #region Props
        public Mana Mana { get; private set; }
        public HP HP { get; private set; }
        public AtkSpeed AtkSpeed { get; private set; }
        public Speed Speed { get; private set; }
        public Classes Class { get; private set; }
        #endregion
        
        #region Methods
        #endregion
    }
}
