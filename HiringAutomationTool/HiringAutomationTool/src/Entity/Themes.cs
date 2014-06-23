using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringAutomationTool
{
    class Themes
    {
        public Themes(string ThemeName)
        {
            if (ThemeName.Equals("Default"))
            {
                this.SetDefaultTheme();
            }
            else if (ThemeName.Equals("Green"))
            {
                this.SetGreenTheme();
            }
            else if (ThemeName.Equals("Red"))
            {
                this.SetRedTheme();
            }
            else if (ThemeName.Equals("Dark"))
            {
                this.SetDarkTheme();
            }
            else if (ThemeName.Equals("Blue"))
            {
                this.SetBlueTheme();
            }
        }
        public Themes()
        {
            themeName = "Default";
            textColor = "ControlDarkDark";
            BackColor = "Window";
        }

        public void SetDarkTheme()
        {

            themeName = "Dark";
            BackColor = "ControlDarkDark";
            textColor = "Window";
            TopColor = "Black";
           

        }
        public void SetBlueTheme()
        {
            themeName = "Blue";
            textColor = "Azure";
            BackColor = "DarkCyan";
            TopColor = "ActiveCaption";
        }
        public void SetRedTheme()
        {
            themeName = "Red";
            textColor = "Red";
            BackColor = "LightSalmon";
            TopColor = "Tomato";
        }
        public void SetGreenTheme()
        {
            themeName = "Green";
            textColor = "DarkOliveGreen";
            BackColor = "LightGreen";
            TopColor = "SpringGreen";
        }
        public void SetDefaultTheme()
        {
            themeName = "default";
            textColor = "ControlDarkDark";
            BackColor = "Window";
            TopColor = "ControlLight";
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        
        private string themeName;

        public string ThemeName
        {
            get { return themeName; }
            set { themeName = value; }
        }

        private string backColor;

        public string BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        private string textColor;

        public string TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }


        private string topColor;

        public string TopColor
        {
            get { return topColor; }
            set { topColor = value; }
        }
        

    }
}
