using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace readDataFile{


    public class ScriptCommunication: MonoBehaviour
    {
        
        public static int SetOnData = 0;
        
        public static int sizeOfWord0 = 0;

        public static bool activatePrefabRedStar = false;

        public static int sizeTextField = 0;


        public static bool flagPaintHighlight_OneLanguage = false;

        public static bool flagPaintHighlight_TwoLanguage = false;


        public static bool flagUnPaintHighlight_OneLanguage = false;

        public static bool flagUnPaintHighlight_TwoLanguage = false;


        public static int positionInt_OneLanguage = 0;

        public static int positionInt_TwoLanguage = 0;


        public static List<List<int>> sizeOfWords_communication_OneLanguage = new List<List<int>>();

        public static List<List<int>> sizeOfWords_communication_TwoLanguage = new List<List<int>>();

    }


}