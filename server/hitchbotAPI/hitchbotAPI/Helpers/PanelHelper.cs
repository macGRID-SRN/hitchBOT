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

        public PanelHelper(Bitmap bM)
        {
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
            List<bool> tempList;
            BitArray bit;
            for (int row = 0; row < imageRedMap.GetLength(0); row++)
            {
                tempList = new List<bool>();
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
                }
                bit = new BitArray(tempList.ToArray());
                byteList.Add(convertToByte(bit));
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



    }
}