using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRC16CCITT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalulate_Click(object sender, EventArgs e)
        {
            string[] hexValues = textBox1.Text.Split(' ');
            byte[] hexValuesByte = new byte[hexValues.Length];            

            for(int i=0;i<hexValues.Length;i++)
            {                
                hexValuesByte[i] = Convert.ToByte(Convert.ToInt32(hexValues[i], 16));
            }
            //byte[] x = hexValuesByte;
            
            result.Text = "CRC16-CCITT = 0x" + Compute_CRC16_Simple(hexValuesByte).ToString("X4");

            //byte[] datalength = new byte[] { 19, 0, 0 };
            //int a = Convert.ToInt32(datalength[0]);
            //int tmt = a + 1;

        }
        public ushort Compute_CRC16_Simple(byte[] bytes)
        {
            /*
            Reference : http://www.sunshine2k.de/articles/coding/crc/understanding_crc.html
            Algorithm : CRC-16-CCIT-FALSE
            Polynomial : 0x1021
            Initial Value : 0xFFFF
            Final XOR Value : 0x0 (Actually 0 has no impact)            
            */
            const ushort generator = 0x1021;    /* divisor is 16bit */
            ushort crc = 0xFFFF; /* CRC value is 16bit (Initial Value)*/

            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8); /* move byte into MSB of 16bit CRC */

                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0) /* test for MSB = bit 15 */
                    {
                        crc = (ushort)((crc << 1) ^ generator);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }

            return crc;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
