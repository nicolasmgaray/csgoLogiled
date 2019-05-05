using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_Logitech_Integration
{

  
    class GameController
    {
        ColorSchema ctColor = new ColorSchema(71, 83, 100);
        ColorSchema ttColor = new ColorSchema(100, 47, 9);
        ColorSchema idleColor = new ColorSchema(100, 100, 100);
        ColorSchema currentSchema = new ColorSchema(100, 100, 100);

        public void changedTeam(string team)
        {
            if (team == "CT")           
                currentSchema = ctColor;
            else if (team == "T")            
                currentSchema = ttColor;            
            else if (team != "")            
                currentSchema = idleColor;            

            updateColorSchema(currentSchema, currentSchema);

        }

        public void changedBombState(string state)
        {
            if (state == "planted")           
                LogitechGSDK.LogiLedPulseLighting(100,0,0,40000,500);            
            else if (state == "defused")            
               LogitechGSDK.LogiLedPulseLighting(0, 100, 0, 4000, 500);            
            else            
                changedTeam("");             
        }

        public void changedAlivePlayers (int aliveAllies, int aliveEnemies)
        {

        }

        public void updateColorSchema(ColorSchema keyBoardSchema, ColorSchema mouseSchema)
        {
            LogitechGSDK.LogiLedSetLighting(mouseSchema.Red, mouseSchema.Green, mouseSchema.Blue);
            LogitechGSDK.LogiLedSetLightingForTargetZone(CSGO_Logitech_Integration.DeviceType.Keyboard, 0, keyBoardSchema.Red, keyBoardSchema.Green, keyBoardSchema.Blue);
            //LogitechGSDK.LogiLedSetLightingForTargetZone(CSGO_Logitech_Integration.DeviceType.Keyboard, 1, 100, 100, 0);
        }


    }

    class GameState
    {
        GameController gameController = new GameController();

        public GameState()
        {
            LogitechGSDK.LogiLedInit();
            gameController.changedTeam("Start");
        }

       
        public void updateState(string newJSONState)
        {

            JObject newState = JObject.Parse(newJSONState);
            var player = JObject.Parse(newState["player"].ToString());

            JObject round =null;
            if (newState["round"] != null)
            {
                round = JObject.Parse(newState["round"].ToString());
                if (round["bomb"] != null)
                    BombState = round["bomb"].ToString();
                else
                    BombState = "inactive";
            }
     
            if (player["team"] != null)               
                Team = player["team"].ToString();
        }

        private string _team = "";
        public string Team
        {
            get { return this._team; }
            set
            {
                if (this._team != value)
                {
                    gameController.changedTeam(value);
                    this._team = value;
                }
            }
        }

        private string _bombState ="";
        public string BombState
        {
            get { return this._bombState; }
            set
            {
                if (this._bombState != value)
                {
                    gameController.changedBombState(value);
                    this._bombState = value;
                }
            }

        }

    }


    class ColorSchema
    {
        public ColorSchema(int red, int green, int blue)
        {
            this._red = red;
            this._green = green;
            this._blue = blue;
        }

        private int _red;
        public int Red
        {
            get { return _red; }
            set
            {
                _red = Red;
            }
        }

        private int _green;
        public int Green
        {
            get { return _green; }
            set
            {
                _green = Green;
            }
        }

        private int _blue;
        public int Blue
        {
            get { return _blue; }
            set
            {
                _blue = Blue;
            }
        }

    }
}
