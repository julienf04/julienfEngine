using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace julienfEngine1
{
    struct GameData
    {
        public int P_PosY { get; set; }
        public bool P_Shoot { get; set; }

        public static string SerializeData(GameData toSerialize)
        {
            string dataResult = toSerialize.P_PosY.ToString() + " " + toSerialize.P_Shoot;
            return dataResult;
        }

        public static GameData DeserializeData(string toDeserialize)
        {
            GameData dataResult = new GameData();

            string[] separateData = toDeserialize.Split(' ');
            dataResult.P_PosY = int.Parse(separateData[0]);
            dataResult.P_Shoot = bool.Parse(separateData[1]);
            return dataResult;
        }
    }
}
