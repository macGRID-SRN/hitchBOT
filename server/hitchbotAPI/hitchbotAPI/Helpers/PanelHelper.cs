using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using System.Data.Entity;

namespace hitchbotAPI.Helpers
{
    public class PanelHelper
    {

        double threshhold;
        Color[][] colorMatrix;
        List<byte> byteArrayOfRows;
        string name;
        string description;
        Models.Password user;
        bool approved;

        public PanelHelper(Bitmap bM, string name, string description, Models.Password user)
        {
            this.name = name;
            this.description = description;
            this.user = user;
            this.approved = false;
            getMatrixFromBitmap(bM);
        }

        public PanelHelper(List<byte> byteArray, string name, string description, Models.Password user)
        {
            this.byteArrayOfRows = byteArray;
            this.name = name;
            this.description = description;
            this.user = user;
            this.approved = true;
        }

        private void getMatrixFromBitmap(Bitmap bM)
        {
            int[,] imageRedMap = new int[16, 24];

            int maxRed = 0;
            int minRed = 255;
            bM = new Bitmap(bM, 24, 16);

            Color[][] colorMatrix = new Color[24][];
            for (int i = 0; i < 24; i++)
            {
                colorMatrix[i] = new Color[16];
                for (int j = 0; j < 16; j++)
                {
                    int pixel = (bM.GetPixel(i, j).A + bM.GetPixel(i, j).B + bM.GetPixel(i, j).G + bM.GetPixel(i, j).R) / 4;
                    colorMatrix[i][j] = new Color();
                    colorMatrix[i][j] = bM.GetPixel(i, j);
                    if (pixel < minRed)
                    {
                        minRed = pixel;
                    }
                    if (pixel > maxRed)
                    {
                        maxRed = pixel;
                    }
                    imageRedMap[j, i] = pixel;
                }
            }

            threshhold = (minRed + maxRed) / 2.0;

            this.colorMatrix = colorMatrix;
            this.byteArrayOfRows = makeByteArray(imageRedMap, this.threshhold);
        }

        private static List<byte> makeByteArray(int[,] imageRedMap, double threshhold)
        {
            List<byte> byteList = new List<byte>();
            List<bool> tempList = new List<bool>();
            for (int row = 0; row < imageRedMap.GetLength(0); row++)
            {
                for (int col = 0; col < imageRedMap.GetLength(1); col++)
                {

                    if (imageRedMap[row, col] < threshhold)
                    {
                        tempList.Add(false);
                    }
                    else
                    {
                        tempList.Add(true);
                    }
                    if (tempList.Count == 8)
                    {
                        byteList.Add(convertToByte(tempList.ToArray()));
                        tempList = new List<bool>();
                    }
                }


            }

            return byteList;
        }

        public static IEnumerable<bool> GetBits(byte b)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return (b & 0x80) != 0;
                b *= 2;
            }
        }

        public static string getArduinoArrayForFace()
        {
            string arduinoArray = "int[] {0} = {{ {1} }}";
            StringBuilder sb = new StringBuilder();
            List<Models.Face> faces = Controllers.LedPanelController.getAllFaces();
            List<string> arrayValues = new List<string>();
            foreach (var face in faces)
            {
                string test = getArrayValue(face);
                sb.AppendLine(string.Format(arduinoArray, face.Name, getArrayValue(face)));
            }
            return sb.ToString();
        }
        // int[] array = {0,1,1,1,1,1,1,1,.. etc..};
        public static string getArrayValue(Models.Face face)
        {
            List<Models.LedPanel> panels = face.Panels.ToList<Models.LedPanel>();
            StringBuilder sb = new StringBuilder();

            foreach (var panel in panels)
            {
                List<Models.Row> rows = panel.Rows.ToList<Models.Row>();
                foreach (var row in rows)
                {
                    List<bool> row1 = GetBits(row.ColSet0).ToList<bool>();
                    List<bool> row2 = row1.Concat(GetBits(row.ColSet1).ToList<bool>()).ToList<bool>();
                    List<bool> row3 = row2.Concat(GetBits(row.ColSet2).ToList<bool>()).ToList<bool>();
                    foreach (var entry in row3)
                    {
                        if (entry)
                        {
                            sb.Append("1,");
                        }
                        else
                        {
                            sb.Append("0,");
                        }
                    }
                }
            }
            return sb.ToString().Substring(0, sb.Length - 3);
        }

        public static byte convertToByte(bool[] arr)
        {
            byte val = 0;
            foreach (bool b in arr)
            {
                val <<= 1;
                if (b) val |= 1;
            }
            return val;
        }

        public Models.Face getFace()
        {
            Models.Face face = new Models.Face
            {
                Name = name,
                Description = description,
                Panels = getPanels(),
                Approved = this.approved,
                UserAccount = user,
                TimeAdded = DateTime.UtcNow
            };

            return face;
        }


        //This method must be refactored if multiple panels are to be added to a face!!!!
        private List<Models.LedPanel> getPanels()
        {
            List<Models.LedPanel> ledPanels = new List<Models.LedPanel>();
            Models.LedPanel ledPanel = new Models.LedPanel
            {
                Rows = getRows(),
                TimeAdded = DateTime.UtcNow
            };
            ledPanels.Add(ledPanel);
            return ledPanels;
        }

        private List<Models.Row> getRows()
        {
            List<Models.Row> rows = new List<Models.Row>();
            for (int i = 0; i < byteArrayOfRows.Count - 2; i = i + 3)
            {
                rows.Add(new Models.Row
                {
                    ColSet0 = byteArrayOfRows[i],
                    ColSet1 = byteArrayOfRows[i + 1],
                    ColSet2 = byteArrayOfRows[i + 2]
                });
            }
            return rows;
        }

    }
}