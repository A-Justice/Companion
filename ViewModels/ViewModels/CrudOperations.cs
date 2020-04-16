using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOops;


namespace ViewModels.ViewModels
{
    public static class CrudOperations
    {
        #region EnumerationDictionaries

        public static Dictionary<int, Program?> ProgramDictionary = new Dictionary<int, Program?>
        {
             { 0,IOops.Program.ComputerEngineering },
             { 1,IOops.Program.ElectricalEngineering },
             { 2,IOops.Program.MechanicalEngineering },
             { 3,IOops.Program.Mathematics },
             { 4,IOops.Program.GeomaticEngineering },
             { 5,IOops.Program.GeologicalEngineering },
             { 6,IOops.Program.PetroleumEngineering },
             { 7,IOops.Program.MineralEngineering },
             { 8,IOops.Program.MiningEngineering },
             { 9,IOops.Program.GeneralDrilling },
             { 10,IOops.Program.EnvironmentalEngineering }
        };


        public static Dictionary<int, Semester?> SemesterDictionary = new Dictionary<int, Semester?>
        {
            {0,Semester.S1 },
            {1,Semester.S2 }
        };


        public static Dictionary<int, Level?> LevelDictionary = new Dictionary<int, Level?>
        {
            {0,Level.L100 },
            {1,Level.L200 },
            {2,Level.L300 },
            { 3,Level.L400}

        };

        #endregion
    }
}
