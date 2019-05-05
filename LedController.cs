namespace CSGO_Logitech_Integration
{
    // This class exposes methods to change LED's in specific ways associated to game events and state
    class LEDController
    {
        static ColorSchema ctColor = new ColorSchema(71, 83, 100);
        static ColorSchema ttColor = new ColorSchema(100, 47, 9);
        static ColorSchema idleColor = new ColorSchema(100, 100, 100);
        static ColorSchema currentSchema = idleColor;

        public LEDController()
        {
            LogitechGSDK.LogiLedInit();
            updateColorSchema(idleColor, idleColor);
        }

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
                LogitechGSDK.LogiLedPulseLighting(100, 0, 0, 40000, 500);
            else if (state == "defused")
                LogitechGSDK.LogiLedPulseLighting(0, 100, 0, 4000, 500);
            else
                updateColorSchema(currentSchema, currentSchema);
        }

        public void changedAlivePlayers(int aliveAllies, int aliveEnemies)
        {

        }

        public void updateColorSchema(ColorSchema keyBoardSchema, ColorSchema mouseSchema)
        {
            LogitechGSDK.LogiLedSetLighting(mouseSchema.Red, mouseSchema.Green, mouseSchema.Blue);
            LogitechGSDK.LogiLedSetLightingForTargetZone(CSGO_Logitech_Integration.DeviceType.Keyboard, 0, keyBoardSchema.Red, keyBoardSchema.Green, keyBoardSchema.Blue);
            //LogitechGSDK.LogiLedSetLightingForTargetZone(CSGO_Logitech_Integration.DeviceType.Keyboard, 1, 100, 100, 0);
        }


    }

    // Just a wrapper for save color schemas
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
