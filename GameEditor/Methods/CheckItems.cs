﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace GameEditor.Methods
{
    public class CheckItems
    {
        public static bool TeamNameAlreadyExists(string name)
        {
            bool result = false;
            using (ConnectDB context = new ConnectDB())
            {
                if (context.teams.Count() > 0) {
                    if (context.teams.FirstOrDefault(team => team.Name == name) != null)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}