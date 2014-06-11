using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace HiringAutomationTool
{
    static class ThemesCollection
    {
        public static ObservableCollection<Themes> themesList = new ObservableCollection<Themes>();

       public static Themes Contains(Themes t)
        {
            foreach (Themes x in themesList)
            {
                if (x.Equals(t))
                {
                    return x;
                }          
            }
            return null;        
        }
       public static Themes Contains(string t)
        {
            foreach (Themes x in themesList)
            {
                if (x.ThemeName.Equals(t))
                {
                    return x;
                }
            }
            return null;        

        }
       public static bool IsContains(Themes t)
       {
           foreach (Themes x in themesList)
           {
               if (x.Equals(t))
               {
                   return true;
               }
           }
           return false;
       }

       public static bool IsContains(string t)
       {
           foreach (Themes x in themesList)
           {
               if (x.ThemeName.Equals(t))
               {
                   return true;
               }
           }
           return false;

       }

       public static void InitializeList()
       {
           Themes theme = new Themes();
           theme.ThemeName = "Default";
           theme.BackColor = "Window";
           theme.TextColor = "Black";
           theme.TopColor = "Window";
           themesList.Add(theme);

           theme = new Themes();
           theme.ThemeName = "Dark";
           theme.BackColor = "ControlDarkDark";
           theme.TextColor = "Window";
           theme.TopColor = "Black";
           themesList.Add(theme);

           theme = new Themes();
           theme.ThemeName = "Blue";
           theme.TextColor = "Azure";
           theme.BackColor = "DarkCyan";
           theme.TopColor = "ActiveCaption";
           themesList.Add(theme);

           theme = new Themes();
           theme.ThemeName = "Red";
           theme.TextColor = "Red";
           theme.BackColor = "LightSalmon";
           theme.TopColor = "Tomato";
           themesList.Add(theme);

           theme = new Themes();
           theme.ThemeName = "Green";
           theme.TextColor = "DarkOliveGreen";
           theme.BackColor = "LightGreen";
           theme.TopColor = "SpringGreen";
           themesList.Add(theme);

           theme = new Themes();
           theme.ThemeName = "default";
           theme.TextColor = "ControlDarkDark";
           theme.BackColor = "Window";
           theme.TopColor = "ControlLight";
           themesList.Add(theme);

       }
    }
}