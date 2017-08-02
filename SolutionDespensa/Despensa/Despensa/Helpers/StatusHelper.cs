﻿using System.Collections.Generic;

namespace Despensa.Helpers
{
    /// <summary>
    /// Helper que retorna os status dos produtos
    /// </summary>
    public static class StatusHelper
    {
        public static List<string> RecuperarStatus()
        {
            return new List<string>()
            {
                "Bastante","Acabando","Acabou"
            };
        }
    }
}