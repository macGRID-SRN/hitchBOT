using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbotAPI.Helpers
{
    public class PanelHelper
    {

        double threshhold;
        Color[][] colorMatrix;
        List<byte> byteArrayOfRows;
        string name;
        string description;

        public PanelHelper(Bitmap bM, string name, string description)
        {
            this.name = name;
            this.description = description;
            getMatrixFromBitmap(bM);
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
            this.byteArrayOfRows = makeByteArray(imageRedMap);
        }

        private List<byte> makeByteArray(int[,] imageRedMap)
        {
            List<byte> byteList = new List<byte>();
            List<bool> tempList = new List<bool>();
            BitArray bit;
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
                    if (row % 8 == 0)
                    {
                        bit = new BitArray(tempList.ToArray());
                        byteList.Add(convertToByte(bit));
                        tempList = new List<bool>();
                    }
                }

            }

            return byteList;
        }

        private byte convertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }

        public Models.Face getFace()
        {
            Models.Face face = new Models.Face
            {

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
            for (int i = 0; i < byteArrayOfRows.Count - 2; i++)
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