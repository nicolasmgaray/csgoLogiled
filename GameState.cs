using Newtonsoft.Json.Linq;

namespace CSGO_Logitech_Integration
{
    // This class contains the GameState and when some property of it changes, it executes a method in the Led Controller
    class GameState
    {
        LEDController ledController;

        public GameState()
        {
            ledController = new LEDController();

        }

        // METHODS
        public void updateState(string newJSONState)
        {

            JObject newState = JObject.Parse(newJSONState);
            JObject player = JObject.Parse(newState["player"].ToString());
            JObject round = newState["round"] != null ? JObject.Parse(newState["round"].ToString()) : null;

            if (round != null)
            {
                if (round["bomb"] != null)
                    BombState = round["bomb"].ToString();
                else
                    BombState = "inactive";
            }

            if (player["team"] != null)
                Team = player["team"].ToString();
        }

        // PROPERTIES

        private string _team = "";
        public string Team
        {
            get { return this._team; }
            set
            {
                if (this._team != value)
                {
                    ledController.changedTeam(value);
                    this._team = value;
                }
            }
        }

        private string _bombState = "";
        public string BombState
        {
            get { return this._bombState; }
            set
            {
                if (this._bombState != value)
                {
                    ledController.changedBombState(value);
                    this._bombState = value;
                }
            }

        }

    }

    

}
