using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imagenary
{
    [Serializable()]
    class ApplicationData
    {

        static ApplicationData Dataobj = new ApplicationData();

        static public ApplicationData Create
        {
            get { return Dataobj; }
            set { Dataobj = value; }
        }

        #region Input

        private int xHide;

        public int XHide
        {
            get { return xHide; }
            set { xHide = value; }
        }

        private int yHide;

        public int YHide
        {
            get { return yHide; }
            set { yHide = value; }
        }
        
        private int xCoordinate;

        public int XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }

        private int yCoordinate;

        public int YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }


        private string mainFileAdress;

        public string MainFileAdress
        {
            get { return mainFileAdress; }
            set { mainFileAdress = value; }
        }


        private string pafFileAdress;

        public string PafFileAdress
        {
            get { return pafFileAdress; }
            set { pafFileAdress = value; }
        }


        private string sphFileAdress;

        public string SPHFileAdress
        {
            get { return sphFileAdress; }
            set { sphFileAdress = value; }
        }


        #endregion
        #region Output
        private string imageOutputDirectory;

        public string ImageOutputDirectory
        {
            get { return imageOutputDirectory; }
            set { imageOutputDirectory = value; }
        }


        #endregion

        public bool IsSettingsDone()
        {
            bool Result = false;
            if (String.IsNullOrEmpty(this.ImageOutputDirectory) && String.IsNullOrEmpty(this.sphFileAdress) && String.IsNullOrEmpty(this.pafFileAdress) && xCoordinate == 0 && this.yCoordinate == 0)
            {
                Result = false;

            }
            else {

                Result = true;
            }
            return Result;
        }



    }
}
