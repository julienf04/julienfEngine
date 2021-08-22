using julienfEngine1;
using System;

namespace julienfEngine1
{
    class OptionsMenu : GameObject
    {
        #region ATRIBUTES

        public static readonly Figure[] RO_FiguresMenuOptions = new Figure[9]
        {
            new Figure(new string[8]
                      {
                          @" _____  _                _                _                           ",
                          @"/  ___|(_)              | |              | |                          ",
                          @"\ `--.  _  _ __    __ _ | |  ___   _ __  | |  __ _  _   _   ___  _ __ ",
                          @" `--. \| || '_ \  / _` || | / _ \ | '_ \ | | / _` || | | | / _ \| '__|",
                          @"/\__/ /| || | | || (_| || ||  __/ | |_) || || (_| || |_| ||  __/| |   ",
                          @"\____/ |_||_| |_| \__, ||_| \___| | .__/ |_| \__,_| \__, | \___||_|   ",
                          @"                   __/ |          | |                __/ |            ",
                          @"                  |___/           |_|               |___/             "
                      }, E_ForegroundColors.Green),

            new Figure(new string[8]
                      {
                          @"___  ___        _  _    _         _                           ",
                          @"|  \/  |       | || |  (_)       | |                          ",
                          @"| .  . | _   _ | || |_  _  _ __  | |  __ _  _   _   ___  _ __ ",
                          @"| |\/| || | | || || __|| || '_ \ | | / _` || | | | / _ \| '__|",
                          @"| |  | || |_| || || |_ | || |_) || || (_| || |_| ||  __/| |   ",
                          @"\_|  |_/ \__,_||_| \__||_|| .__/ |_| \__,_| \__, | \___||_|   ",
                          @"                          | |                __/ |            ",
                          @"                          |_|               |___/             "
                      }, E_ForegroundColors.Gray),

            new Figure(new string[6]
                      {
                          @" _____              _                      _            ",
                          @"/  __ \            | |                    (_)           ",
                          @"| /  \/ _   _  ___ | |_   ___   _ __ ___   _  ____  ___ ",
                          @"| |    | | | |/ __|| __| / _ \ | '_ ` _ \ | ||_  / / _ \",
                          @"| \__/\| |_| |\__ \| |_ | (_) || | | | | || | / / |  __/",
                          @" \____/ \__,_||___/ \__| \___/ |_| |_| |_||_|/___| \___|",
                      }, E_ForegroundColors.Gray),

            new Figure(new string[6]
                      {
                          @" _____        _  _   ",
                          @"|  ___|      (_)| |  ",
                          @"| |__  __  __ _ | |_ ",
                          @"|  __| \ \/ /| || __|",
                          @"| |___  >  < | || |_ ",
                          @"\____/ /_/\_\|_| \__|",
                      }, E_ForegroundColors.Gray),

            new Figure(new string[8]
                      {
                          @" _____                    ",
                          @"|  ___|                   ",
                          @"| |__    __ _  ___  _   _ ",
                          @"|  __|  / _` |/ __|| | | |",
                          @"| |___ | (_| |\__ \| |_| |",
                          @"\____/  \__,_||___/ \__, |",
                          @"                     __/ |",
                          @"                    |___/ ",
                      }),

            new Figure(new string[6]
                      {
                          @" _   _                                 _ ",
                          @"| \ | |                               | |",
                          @"|  \| |  ___   _ __  _ __ ___    __ _ | |",
                          @"| . ` | / _ \ | '__|| '_ ` _ \  / _` || |",
                          @"| |\  || (_) || |   | | | | | || (_| || |",
                          @"\_| \_/ \___/ |_|   |_| |_| |_| \__,_||_|",
                      }),

            new Figure(new string[6]
                      {
                          @" _   _                   _ ",
                          @"| | | |                 | |",
                          @"| |_| |  __ _  _ __   __| |",
                          @"|  _  | / _` || '__| / _` |",
                          @"| | | || (_| || |   | (_| |",
                          @"\_| |_/ \__,_||_|    \__,_|",
                      }),

            new Figure(new string[6]
                      {
                          @" _____                                 ",
                          @"|_   _|                                ",
                          @"  | |   _ __   ___   __ _  _ __    ___ ",
                          @"  | |  | '_ \ / __| / _` || '_ \  / _ \",
                          @" _| |_ | | | |\__ \| (_| || | | ||  __/",
                          @" \___/ |_| |_||___/ \__,_||_| |_| \___|",
                      }),

            new Figure(new string[8]
                      {
                          @" _____                           _                                     ",
                          @"/  ___|                         | |                                    ",
                          @"\ `--.   ___   __ _  _ __   ___ | |__     __ _   __ _  _ __ ___    ___ ",
                          @" `--. \ / _ \ / _` || '__| / __|| '_ \   / _` | / _` || '_ ` _ \  / _ \",
                          @"/\__/ /|  __/| (_| || |   | (__ | | | | | (_| || (_| || | | | | ||  __/",
                          @"\____/  \___| \__,_||_|    \___||_| |_|  \__, | \__,_||_| |_| |_| \___|",
                          @"                                          __/ |                        ",
                          @"                                         |___/                         ",
                      }),
        };

        #endregion

        #region CONSTRUCTORS

        public OptionsMenu(Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, myScene, baseFigure, visible, isUI, layer, posX, posY)
        {

        }

        #endregion

        #region METHODS

        public void OnSinglePlayerOption()
        {

        }

        public void OnMultiplayerOption()
        {

        }

        public void OnCustomizeOption()
        {

        }

        public void OnExitOption()
        {

        }


        public void OnSinglePlayerEasyOption()
        {

        }

        public void OnSinglePlayerNormalOption()
        {

        }

        public void OnSinglePlayerHardOption()
        {

        }

        public void OnSinglePlayerInsaneOption()
        {

        }


        public void OnMultiplayerSearchGameOption()
        {

        }

        #endregion

        #region PROPERTIES

        //public static Figure[] P_FiguresMenuOptions
        //{
        //    get
        //    {
        //        return _figures;
        //    }
        //}

        #endregion
    }
}